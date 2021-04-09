namespace BeMyAngel.Persistance
{
    public class AutoUpdaterSettings
    {
        public bool Enabled { get; set; } = false;
        public string ScriptsDirectory { get; set; }
    }

    public class PersistanceSettings
    {
        public string Host { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public AutoUpdaterSettings AutoUpdater { get; set; }

    }

}
