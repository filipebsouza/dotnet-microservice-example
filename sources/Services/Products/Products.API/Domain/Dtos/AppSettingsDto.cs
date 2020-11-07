using System.Collections.Generic;

namespace Base.Domain.Dtos
{
    public class AppSettingsDto
    {
        public List<string> LocalizationOptions { get; set; }
        public DevelopmentEnvDto DevelopmentEnv {get;set;}
    }

    public class DevelopmentEnvDto
    {
        public string MemoryDatabase { get; set; }
        public string MemoryDatabaseForUnitTests { get; set; }
    }  
}