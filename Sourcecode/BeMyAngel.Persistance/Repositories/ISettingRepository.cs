using BeMyAngel.Persistance.Models;

namespace BeMyAngel.Persistance.Repositories
{
    public interface ISettingRepository
    {
        SettingDto GetByIdentifier(string Identifier);

        void Update(string Identifier, string Value);

        int Insert(string Identifier, string Value);
    }
}
