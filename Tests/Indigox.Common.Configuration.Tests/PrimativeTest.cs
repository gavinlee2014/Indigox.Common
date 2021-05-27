using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Indigox.Common.Configuration.Test
{
    [TestFixture]
    internal class PrimativeTest
    {
        [SetUp]
        public void OnSetUp()
        {
            datas = new Dictionary<Type, object>();
            datas.Add( typeof( int ), (int)10 );
            datas.Add( typeof( short ), (short)10 );
            datas.Add( typeof( long ), (long)10 );
            datas.Add( typeof( uint ), (uint)10 );
            datas.Add( typeof( ushort ), (ushort)10 );
            datas.Add( typeof( ulong ), (ulong)10 );
            datas.Add( typeof( float ), (float)10.1F );
            datas.Add( typeof( double ), (double)10.1D );
            datas.Add( typeof( decimal ), (decimal)10.1 );
            datas.Add( typeof( bool ), true );
            datas.Add( typeof( DateTime ), new DateTime( 1900, 10, 10 ) );
            datas.Add( typeof( byte ), (byte)0xCC );
            datas.Add( typeof( byte[] ), new byte[] { 0x0, 0xD, 0xA } );
            datas.Add( typeof( Guid ), new Guid( "{40e0180c-9a05-44f5-99de-3f10d63025c8}" ) );
            datas.Add( typeof( char ), 'c' );
            datas.Add( typeof( string ), "hello, world!" );
            datas.Add( typeof( State ), State.Opened );
            datas.Add( typeof( int? ), (int)10 );
            datas.Add( typeof( short? ), (short)10 );
            datas.Add( typeof( long? ), (long)10 );
            datas.Add( typeof( uint? ), (uint)10 );
            datas.Add( typeof( ushort? ), (ushort)10 );
            datas.Add( typeof( ulong? ), (ulong)10 );
            datas.Add( typeof( float? ), (float)10.1F );
            datas.Add( typeof( double? ), (double)10.1D );
            datas.Add( typeof( decimal? ), (decimal)10.1 );
            datas.Add( typeof( bool? ), true );
            datas.Add( typeof( DateTime? ), new DateTime( 1900, 10, 10 ) );
            datas.Add( typeof( byte? ), (byte)0xCC );
            datas.Add( typeof( Guid? ), new Guid( "{40e0180c-9a05-44f5-99de-3f10d63025c8}" ) );
            datas.Add( typeof( char? ), 'c' );
            datas.Add( typeof( State? ), null );

            texts = new Dictionary<Type, string>();
            texts.Add( typeof( int ), "10" );
            texts.Add( typeof( short ), "10" );
            texts.Add( typeof( long ), "10" );
            texts.Add( typeof( uint ), "10" );
            texts.Add( typeof( ushort ), "10" );
            texts.Add( typeof( ulong ), "10" );
            texts.Add( typeof( float ), "10.1" );
            texts.Add( typeof( double ), "10.1" );
            texts.Add( typeof( decimal ), "10.1" );
            texts.Add( typeof( bool ), "True" );
            texts.Add( typeof( DateTime ), new DateTime( 1900, 10, 10 ).ToString( "yyyy/MM/dd HH:mm:ss" ) );
            texts.Add( typeof( byte ), "0xCC" );
            texts.Add( typeof( byte[] ), "0x000D0A" );
            texts.Add( typeof( Guid ), "{40e0180c-9a05-44f5-99de-3f10d63025c8}" );
            texts.Add( typeof( char ), "c" );
            texts.Add( typeof( string ), "hello, world!" );
            texts.Add( typeof( State ), "Opened" );
            texts.Add( typeof( int? ), "10" );
            texts.Add( typeof( short? ), "10" );
            texts.Add( typeof( long? ), "10" );
            texts.Add( typeof( uint? ), "10" );
            texts.Add( typeof( ushort? ), "10" );
            texts.Add( typeof( ulong? ), "10" );
            texts.Add( typeof( float? ), "10.1" );
            texts.Add( typeof( double? ), "10.1" );
            texts.Add( typeof( decimal? ), "10.1" );
            texts.Add( typeof( bool? ), "True" );
            texts.Add( typeof( DateTime? ), new DateTime( 1900, 10, 10 ).ToString( "yyyy/MM/dd HH:mm:ss" ) );
            texts.Add( typeof( byte? ), "0xCC" );
            texts.Add( typeof( Guid? ), "{40e0180c-9a05-44f5-99de-3f10d63025c8}" );
            texts.Add( typeof( char? ), "c" );
            texts.Add( typeof( State? ), null );
        }

        private Dictionary<Type, object> datas;
        private Dictionary<Type, string> texts;

        private enum State
        {
            Opened,
            Closed,
        }

        [Test]
        public void TestConvertToString()
        {
            foreach ( var item in datas )
            {
                string text = PrimativeTypes.ConvertToString( item.Value );
                Console.WriteLine( "{0} = \"{1}\"", ClassHelper.GetTypeName( item.Key ), text );
                Assert.AreEqual( texts[ item.Key ], text );
            }
        }

        [Test]
        public void TestConvertFromString()
        {
            foreach ( var item in texts )
            {
                object value = PrimativeTypes.ConvertFromString( item.Key, item.Value );
                Console.WriteLine( "{0} = {1}", ClassHelper.GetTypeName( item.Key ), value );
                Assert.AreEqual( datas[ item.Key ], value );
            }
        }
    }
}