using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Session
{
    interface ICurrentSessionBinder
    {
        ISession Session { get; set; }
    }
}