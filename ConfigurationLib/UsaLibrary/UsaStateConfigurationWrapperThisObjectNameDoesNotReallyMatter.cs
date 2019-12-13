namespace MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary
{
    public class UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter
    {
        public UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter()
        {
            this.UsaStateDefinitions = new UsaStateObjectCollection();
        }

        /* the variable name (UsaStateDefinitions) matches the json element name */
        public UsaStateObjectCollection UsaStateDefinitions { get; set; }
    }
}
