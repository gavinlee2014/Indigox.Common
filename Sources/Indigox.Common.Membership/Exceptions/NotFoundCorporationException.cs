using System;

namespace Indigox.Common.Membership.Exceptions
{
    [Serializable]
    public class NotFoundCorporationException : Exception
    {
        private const string DefaultMessage = "找不到集团根节点。";

        public NotFoundCorporationException()
            : base( DefaultMessage )
        {
        }
    }
}