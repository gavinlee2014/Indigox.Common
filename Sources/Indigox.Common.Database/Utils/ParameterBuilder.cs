using System;
using System.Collections.Generic;
using Indigox.Common.Data.Interface;

namespace Indigox.Common.Data.Utils
{
    internal class ParameterBuilder
    {
        public static IList<IParameter> GetParameters( IDatabase db, string paramDefinitions )
        {
            IList<IParameter> parameters = new List<IParameter>();
            string[] paramDefinitionArray = new string[ 0 ];

            if ( !string.IsNullOrEmpty( paramDefinitions ) )
            {
                paramDefinitionArray = paramDefinitions.Split( ',' );
            }

            for ( int i = 0; i < paramDefinitionArray.Length; i++ )
            {
                string paramDefinition = paramDefinitionArray[ i ];
                IParameter parameter = GetParameter( db, paramDefinition );
                parameters.Add( parameter );
            }

            return parameters;
        }

        public static IList<IParameter> GetParameters( IDatabase db, string paramDefinitions, object[] paramValues )
        {
            IList<IParameter> parameters = new List<IParameter>();
            string[] paramDefinitionArray = new string[ 0 ];

            if ( !string.IsNullOrEmpty( paramDefinitions ) )
            {
                paramDefinitionArray = paramDefinitions.Split( ',' );
            }

            if ( paramDefinitionArray.Length != paramValues.Length )
            {
                throw new Exception( "参数定义与实际参数数量不一致。" );
            }

            for ( int i = 0; i < paramDefinitionArray.Length; i++ )
            {
                string paramDefinition = paramDefinitionArray[ i ];
                IParameter parameter = GetParameter( db, paramDefinition );
                parameter.Value = paramValues[ i ];
                parameters.Add( parameter );
            }

            return parameters;
        }

        public static IParameter GetParameter( IDatabase db, string paramDefinition )
        {
            string[] paramDefinitionTokens = paramDefinition.Split( new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries );

            if ( paramDefinitionTokens.Length > 3 || paramDefinitionTokens.Length < 1 )
            {
                throw new Exception( "参数定义错误：" + paramDefinition );
            }

            string name = paramDefinitionTokens[ 0 ];
            string dbType = ( paramDefinitionTokens.Length > 1 ) ? paramDefinitionTokens[ 1 ] : "varchar";
            string maxLength = "";
            if ( dbType.Contains( "(" ) )
            {
                string[] temp = dbType.Split( new char[] { ' ', '\t', '\n', '\r', '(', ')' }, StringSplitOptions.RemoveEmptyEntries );
                dbType = temp[ 0 ];
                maxLength = temp[ 1 ];
            }

            IParameter parameter = db.CreateParameter();
            parameter.Name = name;
            parameter.DbType = dbType;
            if ( maxLength != "" )
            {
                if ( string.Equals( maxLength, "max", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    parameter.MaxLength = -1;
                }
                else
                {
                    parameter.MaxLength = int.Parse( maxLength );
                }
            }

            if ( paramDefinitionTokens.Length > 2 )
            {
                string direction = paramDefinitionTokens[ 2 ];
                if ( string.Equals( direction, "output", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    parameter.Direction = ParameterDirection.Output;
                }
                else
                {
                    parameter.Direction = ParameterDirection.Input;
                }
            }
            return parameter;
        }
    }
}