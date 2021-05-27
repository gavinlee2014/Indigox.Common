using System;
using System.Collections.Generic;
using Indigox.Common.Data.Logging.MessageFormaters;

namespace Indigox.Common.Data.Logging
{
    internal class ObjectMessageFormater
    {
        private static IList<IMessageFormater> formaters = new List<IMessageFormater>();

        static ObjectMessageFormater()
        {
            formaters.Add( new CommandMessageFormater() );
            formaters.Add( new SystemCommandMessageFormater() );
        }

        public static string Format( object msg )
        {
            if ( msg == null )
            {
                return "";
            }

            foreach ( IMessageFormater formater in formaters )
            {
                if ( formater.IsMatch( msg ) )
                {
                    return formater.Format( msg );
                }
            }

            return msg.ToString();
        }
    }
}