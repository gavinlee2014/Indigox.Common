using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Indigox.Common.StateContainer
{
    internal class ApplicationInfo
    {
        public static bool IsWebApplication
        {
            get
            {
                return ( HttpContext.Current != null );
            }
        }
    }
}