namespace MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary
{
    [System.Diagnostics.DebuggerDisplay("UsaStateFullName='{UsaStateFullName}', UsaStateAbbreviation='{UsaStateAbbreviation}', CountyLabelName='{CountyLabelName}', CountyCount='{UsaCounties.Count}'")]
    public class UsaStateObject
    {
        public string UsaStateFullName { get; set; }

        public string UsaStateAbbreviation { get; set; }

        public int UsaStateDefinitionUniqueIdentifier { get; set; }

        public bool IsContenential { get; set; }

        public UsaCountyLabelEnum CountyLabelName { get; set; }

        /* the variable name (UsaCounties) matches the json element name */
        public UsaCountyObjectCollection UsaCounties { get; set; }
    }
}
