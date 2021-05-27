using System;
using System.Collections.Generic;
using System.Text;
using Indigox.Common.DomainModels.Interface.Generator;

namespace Indigox.Common.DomainModels.Generator
{
    public class DefaultIdGenerator : IIdGenerator
    {
        protected static Dictionary<string, long> seeds = new Dictionary<string, long>(StringComparer.CurrentCultureIgnoreCase);

        public T GetNextID<T>(string name)
        {
            object nextId = null;
            if (!seeds.ContainsKey(name))
            {
                seeds[name] = 1;
                nextId = 1;
            }
            else
            {
                long seed = (long)seeds[name];
                seed++;
                seeds[name] = seed;
                nextId = seed;
            }
            return (T)nextId;
        }
    }
}
