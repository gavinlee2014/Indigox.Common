namespace Indigox.Common.Configuration.Test.Configs
{
    public class OrganizationConfigurator : Configurator<OrganizationConfigSection>
    {
        private static readonly string defaultKey = typeof( OrganizationConfigSection ).Name;

        public OrganizationConfigSection Organization()
        {
            if ( !this.Contains( defaultKey ) )
            {
                this.Add( new OrganizationConfigSection() );
            }
            return this[ defaultKey ];
        }

        public override void Configure()
        {
            OrganizationConfigurations.Instance.Register( this[ defaultKey ] );
        }

        protected override string GetKeyForItem( OrganizationConfigSection item )
        {
            return typeof( OrganizationConfigSection ).Name;
        }
    }
}