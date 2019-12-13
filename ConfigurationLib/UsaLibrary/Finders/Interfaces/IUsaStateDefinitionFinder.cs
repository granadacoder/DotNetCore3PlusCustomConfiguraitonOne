namespace MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary.Finders.Interfaces
{
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary;

    public interface IUsaStateDefinitionFinder
    {
        UsaStateObject FindUsaStateObject(UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter settings, string usaStateFullName);

        UsaStateObject FindUsaStateObject(string usaStateFullName);

        UsaStateObject FindUsaStateObjectByUniqueId(int id);

        UsaStateObject FindUsaStateObjectByUniqueId(UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter settings, int id);
    }
}
