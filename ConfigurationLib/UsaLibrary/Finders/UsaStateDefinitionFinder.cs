namespace MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary.Finders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary.Finders.Interfaces;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary.Retrievers.Interfaces;

    public class UsaStateDefinitionFinder : IUsaStateDefinitionFinder
    {
        public const string ErrorMessageUsaStateDefinitionConfigurationRetrieverIsNull = "UsaStateDefinitionConfigurationRetriever is null";

        private const string ErrorMessageMoreThanOneMatch = "More than item was found with the selection criteria. ({0})";

        private readonly IUsaStateDefinitionConfigurationRetriever usaStateDefinitionConfigurationRetriever;

        public UsaStateDefinitionFinder(IUsaStateDefinitionConfigurationRetriever retriever)
        {
            this.usaStateDefinitionConfigurationRetriever = retriever ?? throw new ArgumentNullException(ErrorMessageUsaStateDefinitionConfigurationRetrieverIsNull);
        }

        public UsaStateObject FindUsaStateObject(UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter settings, string usaStateFullName)
        {
            UsaStateObject returnItem = null;

            if (null != settings && null != settings.UsaStateDefinitions)
            {
                ICollection<UsaStateObject> matchingFarmItems;
                matchingFarmItems = settings.UsaStateDefinitions.Where(ele => usaStateFullName.Equals(ele.UsaStateFullName, StringComparison.OrdinalIgnoreCase)).ToList();

                if (matchingFarmItems.Count > 1)
                {
                    string errorDetails = this.BuildErrorDetails(matchingFarmItems);
                    throw new IndexOutOfRangeException(string.Format(ErrorMessageMoreThanOneMatch, errorDetails));
                }

                returnItem = matchingFarmItems.FirstOrDefault();
            }

            return returnItem;
        }

        public UsaStateObject FindUsaStateObject(string usaStateFullName)
        {
            UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter settings = this.usaStateDefinitionConfigurationRetriever.GetUsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter();
            return this.FindUsaStateObject(settings, usaStateFullName);
        }

        public UsaStateObject FindUsaStateObjectByUniqueId(int id)
        {
            UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter settings = this.usaStateDefinitionConfigurationRetriever.GetUsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter();
            return this.FindUsaStateObjectByUniqueId(settings, id);
        }

        public UsaStateObject FindUsaStateObjectByUniqueId(UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter settings, int id)
        {
            UsaStateObject returnItem = null;

            if (null != settings && null != settings.UsaStateDefinitions)
            {
                ICollection<UsaStateObject> matchingFarmItems;
                matchingFarmItems = settings.UsaStateDefinitions.Where(ele => id == ele.UsaStateDefinitionUniqueIdentifier).ToList();

                if (matchingFarmItems.Count > 1)
                {
                    string errorDetails = this.BuildErrorDetails(matchingFarmItems);
                    throw new IndexOutOfRangeException(string.Format(ErrorMessageMoreThanOneMatch, errorDetails));
                }

                returnItem = matchingFarmItems.FirstOrDefault();
            }

            return returnItem;
        }

        private string BuildErrorDetails(ICollection<UsaStateObject> items)
        {
            string returnValue;

            StringBuilder sb = new StringBuilder();

            if (null != items)
            {
                foreach (UsaStateObject item in items)
                {
                    sb.Append(string.Format("UsaStateFullName='{0}'.", item.UsaStateFullName));
                }
            }

            returnValue = sb.ToString();

            return returnValue;
        }
    }
}
