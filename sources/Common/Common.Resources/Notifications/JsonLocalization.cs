using System.Collections.Generic;

namespace Common.Resources.Notifications
{
    public class JsonLocalization
    {
        public string Key { get; set; }
        public Dictionary<string, string> LocalizedValue = new Dictionary<string, string>();
    }
}