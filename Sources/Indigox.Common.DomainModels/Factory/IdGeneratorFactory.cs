using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.DomainModels.Interface.Generator;
using Indigox.Common.DomainModels.Generator;

namespace Indigox.Common.DomainModels.Factory
{
    public class IdGeneratorFactory
    {
        private static IIdGenerator _dbIdGenerator;
        private static string _generatorType;

        private static string DefaultGeneratorType = "DbIdGenerator";

        public static IIdGenerator GetIdGenerator()
        {
            if (_dbIdGenerator == null)
            {
                if (GetGeneratorType() == DefaultGeneratorType)
                {
                    _dbIdGenerator = new DbIdGenerator();
                }
                else
                {
                    _dbIdGenerator = new DefaultIdGenerator();
                }
            }
            return _dbIdGenerator;
        }

        private static string GetGeneratorType()
        {
            if (string.IsNullOrEmpty(_generatorType))
            {
                //TODO 从配置文件中读取GeneratorType
                _generatorType = DefaultGeneratorType;
            }
            return _generatorType;
        }
    }
}
