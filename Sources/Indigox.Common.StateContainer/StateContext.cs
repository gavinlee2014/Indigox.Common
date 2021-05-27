using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using Indigox.Common.Utilities;

namespace Indigox.Common.StateContainer
{
    public class StateContext
    {
        private static IStateContext current = CurrentSessionContextFactory.Create();

        public static IStateContext Current
        {
            get { return current; }
        }
    }
}