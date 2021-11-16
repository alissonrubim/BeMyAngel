using BeMyAngel.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BeMyAngel.Persistance.Helpers.DatabaseUpdater
{
    internal class DatabaseUpdater: IDatabaseUpdater
    {
        private readonly IDatabase _database;
        private readonly ISettingRepository _settingRepository;
        private readonly IDatabaseSchemaRepostory _databaseSchemaRepostory;
        private readonly PersistanceSettings _settings;

        public DatabaseUpdater(PersistanceSettings settings,
            IDatabase database, 
            ISettingRepository settingRepository, 
            IDatabaseSchemaRepostory databaseSchemaRepostory)
        {
            _settings = settings;
            _database = database;
            _settingRepository = settingRepository;
            _databaseSchemaRepostory = databaseSchemaRepostory;
        }

        private void RunScript(string script)
        {
            foreach (var scriptPiece in script.Split("GO\r\n"))
                if (!string.IsNullOrEmpty(scriptPiece))
                    _database.Execute(scriptPiece);
        }

        private void RunFile(Version version)
        {
            var script = File.ReadAllText(Path.Combine(_settings.AutoUpdater.ScriptsDirectory, $"{version}.sql"));
            RunScript(script);

            if (_settingRepository.GetByIdentifier("DatabaseVersion") == null)
                _settingRepository.Insert("DatabaseVersion", version.ToString());
            else
                _settingRepository.Update("DatabaseVersion", version.ToString());
        }
        
        private void UpdateFrom(Version currentDatabaseVersion)
        {
            var updateFiles = Directory.GetFiles(_settings.AutoUpdater.ScriptsDirectory, "*.sql").Where(x => Regex.Match(x, @"\d+.\d+.\d+.\d+", RegexOptions.IgnoreCase).Success);
            var updateVersions = new List<Version>();
            foreach(var updateFile in updateFiles)
                updateVersions.Add(new Version(Path.GetFileNameWithoutExtension(updateFile)));

            if (currentDatabaseVersion != null)
            {
                //Check if the database has a newers version than the code
                var lastUpdateVersion = updateVersions.Max(x => x);
                if (currentDatabaseVersion > lastUpdateVersion)
                    throw new Exception($"Database is using a newer version than the code requires (DatabaseVersion `{currentDatabaseVersion}`, LastScriptFile `{lastUpdateVersion}`). Please, delete your database or update your branch.");
                
            }

            var hadUpdate = false;
            foreach (var updateVersion in updateVersions.OrderBy(x => x))
            {
                if (currentDatabaseVersion == null || updateVersion > currentDatabaseVersion) 
                {
                    RunFile(updateVersion);
                    hadUpdate = true;
                    currentDatabaseVersion = updateVersion;
                }
            }

            if (hadUpdate)
            {
                RunScript(File.ReadAllText(Path.Combine(_settings.AutoUpdater.ScriptsDirectory, $"data.sql")));
                RunScript(File.ReadAllText(Path.Combine(_settings.AutoUpdater.ScriptsDirectory, $"data.test.sql")));
            }
        }

        public void Update()
        {
            if (_settings.AutoUpdater.Enabled)
            {
                if (_databaseSchemaRepostory.DatabaseExists(_settings.Database))
                {
                    _databaseSchemaRepostory.CreateDatabase(_settings.Database);
                    UpdateFrom(null);
                }
                else
                {
                    var databaseVersionSetting = _settingRepository.GetByIdentifier("DatabaseVersion");
                    if (databaseVersionSetting == null)
                        throw new Exception("Something went wrong: the database doesnt have the DatabaseVersion setting");

                    UpdateFrom(new Version(databaseVersionSetting.Value));
                }
            }
        }
    }
}
