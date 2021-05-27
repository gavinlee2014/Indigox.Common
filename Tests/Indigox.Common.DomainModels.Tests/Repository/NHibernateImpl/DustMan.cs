using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.DomainModels.Test.Repository.NHibernateImpl
{
    class DustMan
    {
        public DustMan()
        {
            this.guid = Guid.NewGuid();
        }

        private Guid guid;
        private string name;

        public Guid GUID
        {
            get { return guid; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
