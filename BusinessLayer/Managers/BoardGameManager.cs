namespace MyCompany.MyExamples.CustomConfigurationExample.BusinessLayer.Managers
{
    using System;
    using System.Collections.Generic;
    using Common.Logging;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary.Finders.Interfaces;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary.Retrievers.Interfaces;
    using MyCompany.MyExamples.CustomConfigurationExample.BusinessLayer.Managers.Interfaces;

    public class BoardGameManager : IBoardGameManager
    {
        private readonly ILog logger;
        private readonly IUsaStateDefinitionConfigurationRetriever usaStateDefinitionConfigurationRetriever;
        private readonly IUsaStateDefinitionFinder usaStateDefinitionFinder;

        public BoardGameManager(ILog lgr, IUsaStateDefinitionConfigurationRetriever usaStateDefinitionConfigurationRetriever, IUsaStateDefinitionFinder usaStateDefinitionFinder)
        {
            this.logger = lgr ?? throw new ArgumentNullException("ILog is null");
            this.usaStateDefinitionConfigurationRetriever = usaStateDefinitionConfigurationRetriever ?? throw new ArgumentNullException("IUsaStateDefinitionConfigurationRetriever is null");
            this.usaStateDefinitionFinder = usaStateDefinitionFinder ?? throw new ArgumentNullException("IUsaStateDefinitionFinder is null");
        }

        public void DemonstrateIUsaStateDefinitionConfigurationRetriever()
        {
            UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter wrapper = this.usaStateDefinitionConfigurationRetriever.GetUsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter();
            if (null != wrapper)
            {
                this.ShowUsaStateObjects("From.DemonstrateIUsaStateDefinitionConfigurationRetriever", wrapper.UsaStateDefinitions);
            }
        }

        public void DemonstrateIUsaStateDefinitionFinder()
        {
            UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter wrapper = this.usaStateDefinitionConfigurationRetriever.GetUsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter();
            if (null != wrapper)
            {
                UsaStateObject foundItem = null;

                foundItem = this.usaStateDefinitionFinder.FindUsaStateObject(wrapper, "Virginia");
                this.ShowUsaStateObject("With.Wrapper", foundItem);

                foundItem = this.usaStateDefinitionFinder.FindUsaStateObject("Alaska");
                this.ShowUsaStateObject("Without.Wrapper", foundItem);
            }
        }

        private void ShowUsaStateObjects(string label, ICollection<UsaStateObject> items)
        {
            if (null != items)
            {
                foreach (UsaStateObject item in items)
                {
                    this.ShowUsaStateObject(label, item);
                }
            }
        }

        private void ShowUsaStateObject(string label, UsaStateObject item)
        {
            if (null != item)
            {
                Console.WriteLine(string.Format("Label='{0}', UsaStateAbbreviation='{1}', UsaStateFullName='{2}', IsContenential='{3}', CountyLabelName(Enum)='{4}'", label, item.UsaStateAbbreviation, item.UsaStateFullName, item.IsContenential, item.CountyLabelName));
                if (null != item.UsaCounties)
                {
                    foreach (UsaCountyObject currentCounty in item.UsaCounties)
                    {
                        Console.WriteLine(string.Format("....UsaCountyValue='{0}'", currentCounty.UsaCountyValue));
                    }
                }
            }
        }
    }
}
