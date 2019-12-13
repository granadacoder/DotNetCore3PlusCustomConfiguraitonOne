namespace MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary
{
    [System.Diagnostics.DebuggerDisplay("UsaCountyValue='{UsaCountyValue}'")]
    public class UsaCountyObject
    {
        public UsaCountyObject()
        {
        }

        public UsaCountyObject(string usaCountyValue)
        {
            this.UsaCountyValue = usaCountyValue;
        }

        public string UsaCountyValue { get; set; }
    }
}
