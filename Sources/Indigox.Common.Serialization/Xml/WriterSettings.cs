using System;
using System.Collections;
using System.Collections.Generic;

namespace Indigox.Common.Serialization.Xml
{
    internal class WriterSettings
    {
        IObjectWriter defaultWriter;
        SimpleClasses simpleClasses;
        ComplexClasses complexClasses;
        TypeAlias typeAlias;
        TypeNameConverter typeNameConverter;
        bool writeType;

        public WriterSettings()
        {
            this.defaultWriter = new ObjectWriter( this );
            this.simpleClasses = new SimpleClasses();
            this.complexClasses = new ComplexClasses();
            this.typeAlias = new TypeAlias();
            this.typeNameConverter = new TypeAliasNameConverter( typeAlias );
            this.writeType = true;

            RegisterDefaultTypeAlias( this.typeAlias );
            RegisterDefaultSimpleClasses( this, this.simpleClasses );
            RegisterDefaultComplexClasses( this, this.complexClasses );
        }

        public IObjectWriter DefaultWriter
        {
            get { return defaultWriter; }
            set { defaultWriter = value; }
        }

        public SimpleClasses SimpleClasses
        {
            get { return simpleClasses; }
        }

        public ComplexClasses ComplexClasses
        {
            get { return complexClasses; }
        }

        public TypeAlias TypeAlias
        {
            get { return typeAlias; }
        }

        public TypeNameConverter TypeNameConverter
        {
            get { return typeNameConverter; }
            set { this.typeNameConverter = value; }
        }

        public IObjectWriter GetWriter( Type type )
        {
            IObjectWriter writer = this.SimpleClasses.GetWriter( type );
            if ( writer != null )
            {
                return writer;
            }

            writer = this.ComplexClasses.GetWriter( type );
            if ( writer != null )
            {
                return writer;
            }

            return this.DefaultWriter;
        }

        public bool WriteType
        {
            get { return writeType; }
            set { writeType = value; }
        }

        private static void RegisterDefaultTypeAlias( TypeAlias typeAlias )
        {
            typeAlias.RegistType( typeof( int ), "int" );
            typeAlias.RegistType( typeof( bool ), "bool" );
            typeAlias.RegistType( typeof( double ), "double" );
            typeAlias.RegistType( typeof( float ), "float" );
            typeAlias.RegistType( typeof( short ), "short" );
            typeAlias.RegistType( typeof( string ), "string" );
            typeAlias.RegistType( typeof( DateTime ), "datetime" );
            typeAlias.RegistType( typeof( Guid ), "guid" );
        }

        private static void RegisterDefaultSimpleClasses( WriterSettings settings, SimpleClasses simpleClasses )
        {
            simpleClasses.RegistType( typeof( int ), new SimpleValueWriter( settings ) );
            simpleClasses.RegistType( typeof( bool ), new SimpleValueWriter( settings ) );
            simpleClasses.RegistType( typeof( double ), new SimpleValueWriter( settings ) );
            simpleClasses.RegistType( typeof( float ), new SimpleValueWriter( settings ) );
            simpleClasses.RegistType( typeof( short ), new SimpleValueWriter( settings ) );
            simpleClasses.RegistType( typeof( string ), new SimpleValueWriter( settings ) );
            simpleClasses.RegistType( typeof( DateTime ), new SimpleValueWriter( settings ) );
            simpleClasses.RegistType( typeof( Guid ), new SimpleValueWriter( settings ) );
        }

        private static void RegisterDefaultComplexClasses( WriterSettings settings, ComplexClasses complexClasses )
        {
            complexClasses.RegistType( typeof( IDictionary<,> ), new GenericDictionaryWriter( settings ) );
            complexClasses.RegistType( typeof( IDictionary ), new DictionaryWriter( settings ) );
            complexClasses.RegistType( typeof( ICollection<> ), new CollectionWriter( settings ) );
            complexClasses.RegistType( typeof( ICollection ), new CollectionWriter( settings ) );
            complexClasses.RegistType( typeof( Object ), new ObjectWriter( settings ) );
        }
    }
}