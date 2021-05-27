using System;
using System.Collections.Generic;
using System.Text;

namespace Indigox.Common.Data.SqlBuilder
{
    /// <summary>
    /// Common Table Expression
    /// </summary>
    public class SqlCte
    {
        private string name;
        private List<string> fields = new List<string>();
        private SqlSelectCommand selectCommand;
        private SqlSelectCommand selectCommand2;
        private int maxRecrusive;
        private SqlCommand doCommand;

        public SqlCte( string name )
        {
            this.name = name;
        }

        public SqlCte Fields( params string[] fields )
        {
            this.fields.AddRange( fields );
            return this;
        }

        public SqlCte SelectFrom( SqlSelectCommand selectCommand )
        {
            this.selectCommand = selectCommand;
            return this;
        }

        public SqlCte UnionAll( SqlSelectCommand selectCommand )
        {
            this.selectCommand2 = selectCommand;
            return this;
        }

        public SqlCte Do( SqlCommand command )
        {
            this.doCommand = command;
            return this;
        }

        public SqlCte MaxRecrusive( int maxRecrusive )
        {
            this.maxRecrusive = maxRecrusive;
            return this;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append( "with " + name + " (" + string.Join( ", ", fields.ToArray() ) + ") as (" );
            builder.Append( selectCommand.ToString() );
            if ( selectCommand2 != null )
            {
                builder.Append( " union all " );
                builder.Append( selectCommand2.ToString() );
            }
            builder.Append( ") " );
            builder.Append( doCommand.ToString() );
            
            return builder.ToString();
        }
    }
}