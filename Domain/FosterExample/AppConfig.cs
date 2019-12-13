namespace MyCompany.MyExamples.CustomConfiguration.Domain.FosterExample
{
    public class AppConfig
    {
        public ConnectionStringsConfig ConnectionStringsX { get; set; }

        public ApiSettingsConfig ApiSettings { get; set; }

        public class ConnectionStringsConfig
        {
            public string MyDb { get; set; }
        }

        public class ApiSettingsConfig
        {
            public string Url { get; set; }

            public string ApiKey { get; set; }

            public bool UseCache { get; set; }
        }
    }
}