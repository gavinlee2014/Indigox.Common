using System;
using System.Collections.Generic;

namespace Indigox.Common.DomainModels.Repository
{
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false, Inherited = true )]
    public class DomainObjectAttribute : Attribute
    {
        public DomainObjectAttribute( string key )
        {
            this.keys = new string[] { key };
        }

        public DomainObjectAttribute( string[] keys )
        {
            this.keys = keys;
        }

        private string[] keys;

        public string[] Keys
        {
            get { return keys; }
        }
    }
}
