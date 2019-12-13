namespace MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary.Retrievers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary.Retrievers.Interfaces;

    public class UsaStateDefinitionConfigurationRetriever : IUsaStateDefinitionConfigurationRetriever
    {
        private readonly IConfigurationRoot configuration;

        public UsaStateDefinitionConfigurationRetriever(IConfigurationRoot configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException("IConfigurationRoot is null");
        }

        public UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter GetUsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter()
        {
            UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter returnItem = this.configuration.Get<UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter>();

            if (returnItem != null)
            {
                IEnumerable<int> duplicatesIdentifiers = returnItem.UsaStateDefinitions.GroupBy(i => i.UsaStateDefinitionUniqueIdentifier)
                  .Where(g => g.Count() > 1)
                  .Select(g => g.Key);

                if (duplicatesIdentifiers.Count() > 1)
                {
                    throw new ArgumentOutOfRangeException("Duplicate UsaStateDefinitionUniqueIdentifier values.", Convert.ToString(duplicatesIdentifiers.First()));
                }

                return returnItem;
            }

            return null;
        }
    }
}
