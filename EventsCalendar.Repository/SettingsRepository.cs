using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsCalendar.Model;

namespace EventsCalendar.Repository
{
    public class SettingsRepository
    {
        private static Dictionary<String, String> _settings;
        private static SettingsRepository Default = new SettingsRepository();

        private static Object UpdateSettingsLock = new Object();
        public static void UpdateSettings()
        {
            lock (UpdateSettingsLock)
            {
                using (var db = new CalendarDbContext())
                {
                    _settings = db.Settings
                        .ToDictionary(s => s.Name, s => s.Value);
                }
            }
        }

        public String this[String name]
        {
            get
            {
                return SettingsRepository.Default[name, null];
            }
        }

        protected String this[String name, String defaultValue]
        {
            get
            {
                if (_settings == null)
                    UpdateSettings();

                if (String.IsNullOrWhiteSpace(name))
                    return null;

                if (ConfigurationManager.AppSettings.AllKeys.Any(k => name.Equals(k, StringComparison.OrdinalIgnoreCase)))
                    return ConfigurationManager.AppSettings[name];

                if (_settings.ContainsKey(name))
                    return _settings[name];

                return defaultValue;
            }
        }

        public T GetValue<T>(String name, T defaultValue = default(T))
        {
            var value = this[name];
            int intVal;
            decimal decVal;
            Double dblVal;
            DateTime dateVal;
            bool boolVal;

            if (_settings == null)
                UpdateSettings();

            switch (defaultValue.GetType().ToString())
            {
                case "System.Int32":
                case "System.Nullable`1[System.Int32]":
                    if (Int32.TryParse(value, out intVal))
                        return (T)Convert.ChangeType(intVal, typeof(T));
                    break;

                case "System.Decimal":
                case "System.Nullable`1[System.Decimal]":
                    if (Decimal.TryParse(value, out decVal))
                        return (T)Convert.ChangeType(decVal, typeof(T));
                    break;

                case "System.Double":
                case "System.Nullable`1[System.Double]":
                    if (Double.TryParse(value, out dblVal))
                        return (T)Convert.ChangeType(dblVal, typeof(T));
                    break;

                case "System.DateTime":
                case "System.Nullable`1[System.DateTime]":
                    if (DateTime.TryParse(value, out dateVal))
                        return (T)Convert.ChangeType(dateVal, typeof(T));
                    break;

                case "System.Boolean":
                case "System.Nullable`1[System.Boolean]":
                    if (Boolean.TryParse(value, out boolVal))
                        return (T)Convert.ChangeType(boolVal, typeof(T));
                    break;
            }

            return defaultValue;
        }

        public static void SetValue(string name, Object valueObj, bool dirtyWrite = false)
        {
            var value = Convert.ToString(valueObj);
            using (var db = new CalendarDbContext())
            {
                var prop = db.Settings.SingleOrDefault(s => s.Name == name);
                if (prop == null)
                {
                    db.Settings.Add(prop = new Setting { Name = name, Value = value });
                    db.SaveChanges();
                }

                if (prop.Value != Default[name] && prop.Value != value && !dirtyWrite)
                {
                    UpdateSettings();
                    throw new ApplicationException("Value has changed to something else, while you didn't know!");
                }

                prop.Value = value;
                db.SaveChanges();
            }
        }

        public static class Reddit
        {
            public static string BotLogin { get { return Default["Reddit.BotLogin"]; } set { SetValue("Reddit.BotLogin", value); } }
            public static string BotPassword { get { return Default["Reddit.BotPassword"]; } set { SetValue("Reddit.BotPassword", value); } }
        }

        public static class GoogleAPI
        {
            public static string ClientID { get { return Default["GoogleAPI.ClientID"]; } }
            public static string ClientSecret { get { return Default["GoogleAPI.ClientSecret"]; } }
            public static string ApiKey { get { return Default["GoogleAPI.ApiKey"]; } }
        }
    }
}
