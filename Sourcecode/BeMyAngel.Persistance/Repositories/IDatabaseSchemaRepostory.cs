using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Persistance.Repositories
{
    public interface IDatabaseSchemaRepostory
    {
        void CreateDatabase(string DatabaseName);

        bool DatabaseExists(string DatabaseName);
    }
}
