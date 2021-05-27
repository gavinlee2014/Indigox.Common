using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.DomainModels.Test.Repository.NHibernateImpl
{
    class Street
    {
        public Street()
        {
            this.guid = Guid.NewGuid();
        }

        private Guid guid;
        private string name;
        private IList<DustMan> assignTo = new List<DustMan>();

        public Guid GUID
        {
            get { return guid; }
        }
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public IList<DustMan> AssignTo
        {
            get { return assignTo; }
        }

        public void AddDustMan(DustMan dustman)
        {
            this.assignTo.Add(dustman);
        }

        public void RemoveDustMan(DustMan dustman)
        {
            this.assignTo.Remove(dustman);
        }
        

    }
}
