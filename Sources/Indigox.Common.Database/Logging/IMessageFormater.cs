using System;

namespace Indigox.Common.Data.Logging
{
    internal interface IMessageFormater
    {
        bool IsMatch( object msg );

        string Format( object msg );
    }
}