using System;

namespace Indigox.Common.ADAccessor.ObjectModel
{
    public abstract class Entry
    {
        public virtual Guid ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string DisplayName { get; set; }
        public Guid Parent { get; set; }
    }
}
