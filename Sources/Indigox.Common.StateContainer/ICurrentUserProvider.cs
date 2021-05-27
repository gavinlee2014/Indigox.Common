using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.StateContainer
{
    public interface ICurrentUserProvider
    {
        string GetCurrentUser();
    }
}
