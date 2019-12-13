namespace MyCompany.MyExamples.CustomConfigurationExample.ConsoleOne
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Common.Logging;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary.Finders;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary.Finders.Interfaces;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary.Retrievers;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary.Retrievers.Interfaces;
    using MyCompany.MyExamples.CustomConfigurationExample.BusinessLayer.Managers;
    using MyCompany.MyExamples.CustomConfigurationExample.BusinessLayer.Managers.Interfaces;

    public static class Program
    {
        public static int Main(string[] args)
        {
            ILog lgr = LogManager.GetLogger(typeof(Program));

            try
            {
                /* look at the Project-Properties/Debug(Tab) for this environment variable */
                string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                Console.WriteLine(string.Format("ASPNETCORE_ENVIRONMENT='{0}'", environmentName));
                Console.WriteLine(string.Empty);

                IConfigurationBuilder builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                        .AddEnvironmentVariables();

                IConfigurationRoot configuration = builder.Build();

                string defaultConnectionStringValue = configuration.GetConnectionString("DefaultConnection");
                Console.WriteLine(string.Format("defaultConnectionStringValue='{0}'", defaultConnectionStringValue));
                Console.WriteLine(string.Empty);

                ////setup our DI
                IServiceCollection servColl = new ServiceCollection()
                    .AddSingleton(lgr)
                    .AddLogging()
                    .AddSingleton<IBoardGameManager, BoardGameManager>()
                .AddSingleton<IUsaStateDefinitionConfigurationRetriever, UsaStateDefinitionConfigurationRetriever>()
                .AddSingleton<IUsaStateDefinitionFinder, UsaStateDefinitionFinder>()
                .AddSingleton(configuration);

                ServiceProvider servProv = servColl.BuildServiceProvider();

                CustomConfiguration.Domain.FosterExample.AppConfig my7AppConfig = configuration.Get<CustomConfiguration.Domain.FosterExample.AppConfig>();
                ////UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter myUsaStateDefinitionConfigurationSectionName = configuration.Get<UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter>();

                IUsaStateDefinitionConfigurationRetriever customConfigRetriever = servProv.GetService<IUsaStateDefinitionConfigurationRetriever>();

                UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter myUsaStateDefinitionConfigurationSectionName = customConfigRetriever.GetUsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter();

                if (null != myUsaStateDefinitionConfigurationSectionName)
                {
                    ShowUsaStateObjects("configuration.Get", myUsaStateDefinitionConfigurationSectionName.UsaStateDefinitions);
                }

                Console.WriteLine(string.Empty);

                IUsaStateDefinitionFinder finder = servProv.GetService<IUsaStateDefinitionFinder>();
                UsaStateObject foundUsaStateObject = finder.FindUsaStateObject("Virginia");
                ShowUsaStateObject("FindUsaStateObject:(ByVirginia)", foundUsaStateObject);
                Console.WriteLine(string.Empty);

                IBoardGameManager boardGameManager = servProv.GetService<IBoardGameManager>();
                boardGameManager.DemonstrateIUsaStateDefinitionConfigurationRetriever();
                Console.WriteLine(string.Empty);

                boardGameManager.DemonstrateIUsaStateDefinitionFinder();
                Console.WriteLine(string.Empty);
            }
            catch (Exception ex)
            {
                string flattenMsg = GenerateFullFlatMessage(ex, true);
                Console.WriteLine(flattenMsg);
            }

            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();

            return 0;
        }

        private static void ShowUsaStateObjects(string label, ICollection<UsaStateObject> items)
        {
            if (null != items)
            {
                foreach (UsaStateObject item in items)
                {
                    ShowUsaStateObject(label, item);
                }
            }
        }

        private static void ShowUsaStateObject(string label, UsaStateObject item)
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

        private static string GenerateFullFlatMessage(Exception ex)
        {
            return GenerateFullFlatMessage(ex, false);
        }

        private static string GenerateFullFlatMessage(Exception ex, bool showStackTrace)
        {
            string returnValue;

            StringBuilder sb = new StringBuilder();
            Exception nestedEx = ex;

            while (nestedEx != null)
            {
                if (!string.IsNullOrEmpty(nestedEx.Message))
                {
                    sb.Append(nestedEx.Message + System.Environment.NewLine);
                }

                if (showStackTrace && !string.IsNullOrEmpty(nestedEx.StackTrace))
                {
                    sb.Append(nestedEx.StackTrace + System.Environment.NewLine);
                }

                if (ex is AggregateException)
                {
                    AggregateException ae = ex as AggregateException;

                    foreach (Exception flatEx in ae.Flatten().InnerExceptions)
                    {
                        if (!string.IsNullOrEmpty(flatEx.Message))
                        {
                            sb.Append(flatEx.Message + System.Environment.NewLine);
                        }

                        if (showStackTrace && !string.IsNullOrEmpty(flatEx.StackTrace))
                        {
                            sb.Append(flatEx.StackTrace + System.Environment.NewLine);
                        }
                    }
                }

                nestedEx = nestedEx.InnerException;
            }

            returnValue = sb.ToString();

            return returnValue;
        }
    }
}