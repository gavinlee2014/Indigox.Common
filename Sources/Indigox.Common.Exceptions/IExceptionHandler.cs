using System;

namespace Indigox.Common.Exceptions
{
    interface IExceptionHandler
    {
        void Handle(Exception ex);
    }
}
