using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Indigox.Common.Data.Interface;
using Indigox.Common.Data;

namespace Indigox.Common.DomainModels.Repository.NHibernateImpl.UserTypes
{
    public class ClassNameFactory
    {
        private const int NullID = -1;
        
        private static Dictionary<int, string> classIDCache = new Dictionary<int, string>();
        private static Dictionary<string, int> classNameCache = new Dictionary<string, int>();
        private static Dictionary<string, IDatabase> databaseCache = new Dictionary<string, IDatabase>();

        private static IDatabase GetDatabase(string connectionName)
        {
            if (databaseCache.ContainsKey(connectionName))
            {
                return databaseCache[connectionName];
            }
            else
            {
                IDatabase db = new DatabaseFactory().CreateDatabase(connectionName);
                databaseCache.Add(connectionName, db);
                return db;
            }
        }

        public static int GetClassID(string className, string connectionName)
        {
            if (classNameCache.ContainsKey(className))
            {
                return classNameCache[className];
            }

            string querySql = "select ClassID from Classes where ClassName = '" + className + "'";
            string insertSql = "insert into Classes (ClassName) values ('" + className + "')";

            int classID = NullID;

            IDatabase db = GetDatabase(connectionName);

            IRecordSet recordset = db.QueryText(querySql);
            if (recordset.Records.Count > 0)
            {
                classID = recordset.Records[0].GetInt("ClassID");
            }

            if (classID == NullID)
            {
                db.ExecuteText(insertSql);

                recordset = db.QueryText(querySql);
                if (recordset.Records.Count > 0)
                {
                    classID = recordset.Records[0].GetInt("ClassID");
                    classIDCache.Add(classID, className);
                    classNameCache.Add(className, classID);
                }
                else
                {
                    throw new Exception("添加 ClassName 失败");
                }
            }
            
            return classID;
        }

        public static int GetClassID(Type type, string connectionName)
        {
            return GetClassID(GetTypeName(type), connectionName);
        }

        /// <summary>
        /// 通过 ClassID 获取 ClassName
        /// </summary>
        public static string GetClassName(int classID, string connectionName)
        {
            if (classIDCache.ContainsKey(classID))
            {
                return classIDCache[classID];
            }

            string className = "";
            string querySql = "select ClassName from Classes where ClassID = '" + classID + "'";

            IDatabase db = GetDatabase(connectionName);
            IRecordSet recordset = db.QueryText(querySql);
            if (recordset.Records.Count > 0)
            {
                className = recordset.Records[0].GetString("ClassName");
            }

            return className;
        }

        /// <summary>
        /// 获取 C# 类型的名称
        /// </summary>
        private static string GetTypeName(Type type)
        {
            return type.FullName + ", " + type.Assembly.GetName().Name;
        }
    }
}
