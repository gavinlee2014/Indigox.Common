using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.DomainModels.Interface.Generator;
using Indigox.Common.Data;
using Indigox.Common.Data.Interface;
using Indigox.Common.DomainModels.Configuration.Generator;

namespace Indigox.Common.DomainModels.Generator
{
    public class DbIdGenerator : IIdGenerator
    {
        public T GetNextID<T>(string name)
        {
            DBIdGenertatorElement dbIdGenertatorElement = DBIdGenertatorConfigurations.Instance.GetConfiguration(name);
            DatabaseFactory factory = new DatabaseFactory();
            IDatabase db = factory.CreateDatabase(dbIdGenertatorElement.DatabaseName);

            string sql =
                "if exists (select 1 from serial where serial_name = @name) " +
                "  update serial set serial_seed = (isnull(serial_seed,0) + 1) where serial_name = @name; " +
                "else " +
                "  insert into serial (serial_name, serial_seed) values (@name, 1); " +
                "select serial_seed from serial where serial_name = @name;";

            object nextId = db.ScalarText(
                sql,
                "@name varchar",
                name
            );

            return (T)Convert.ChangeType(nextId, typeof(T));
        }
    }
}
