using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Indigox.Common.Expression
{
    [Serializable]
    public class ExpressionParseException : Exception
    {

        private int _startPos = -1;
        private int _endPos = -1;

        public int StartPos
        {
            get { return _startPos; }
        }

        public int EndPos
        {
            get { return _endPos; }
        }

        public ExpressionParseException() : base() { }
        public ExpressionParseException( string message ) : base( message, null ) { }
        public ExpressionParseException( string message, Exception inner ) : base( message, inner ) { }
        protected ExpressionParseException( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
            _startPos = (int)info.GetValue( "startPos", typeof( int ) );
            _endPos = (int)info.GetValue( "endPos", typeof( int ) );
        }

        public ExpressionParseException( string message, int pos )
            : base( message, null )
        {
            _startPos = pos;
            _endPos = -1;
        }

        public ExpressionParseException( string message, int startPos, int endPos )
            : base( message, null )
        {
            _startPos = startPos;
            _endPos = endPos;
        }

        public ExpressionParseException( string message, int startPos, int endPos, Exception inner )
            : base( message, inner )
        {
            _startPos = startPos;
            _endPos = endPos;
        }

        public override void GetObjectData( SerializationInfo info, StreamingContext context )
        {
            info.AddValue( "startPos", _startPos );
            info.AddValue( "endPos", _endPos );

            base.GetObjectData( info, context );
        }
    }
}
