using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Localization;

namespace Common.Resources.Notifications
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        string _culture = "en-US";
        List<JsonLocalization> _localization = new List<JsonLocalization>();
        public JsonStringLocalizer(string resourseJson = "resources")
        {
            _localization = JsonSerializer.Deserialize<List<JsonLocalization>>(File.ReadAllText($"{resourseJson}.json"));
        }

        public void SetCulture(string culture) => _culture = culture;

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, resourceNotFound: format == null);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) =>
            _localization
                .Where(l => l.LocalizedValue.Keys.Any(lv => lv == _culture))
                .Select(l => new LocalizedString(l.Key, l.LocalizedValue[_culture], true));

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            var jsonStringLocalizer = new JsonStringLocalizer();

            if (culture != null)
                jsonStringLocalizer.SetCulture(culture.Name);

            return jsonStringLocalizer;
        }

        private string GetString(string name)
        {
            var query = _localization.Where(l => l.LocalizedValue.Keys.Any(lv => lv == _culture));
            var value = query.FirstOrDefault(l => l.Key == name);
            return value.LocalizedValue[_culture];
        }
    }
}