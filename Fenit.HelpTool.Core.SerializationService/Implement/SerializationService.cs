using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fenit.HelpTool.Core.Service.Abstract;
using Fenit.HelpTool.Core.Service.Model.Settings;
using Fenit.HelpTool.Core.Service.Model.Shifter;
using Newtonsoft.Json;

namespace Fenit.HelpTool.Core.SerializationService.Implement
{
    public class SerializationService : ISerializationService
    {
        private readonly string _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.ft");

        #region public

        #region ShifterConfig

        public bool SaveShifterConfig(List<ShifterConfig> shifterConfig)
        {
            var i = 1;
            foreach (var config in shifterConfig.OrderBy(w => w.Order))
            {
                config.Order = i;
                i++;
            }

            SaveToFile(GetConfigPath(), shifterConfig.OrderBy(w => w.Id));
            return true;
        }

        public List<ShifterConfig> LoadConfig()
        {
            var result = new List<ShifterConfig>();
            var config = LoadShifterConfigSettings();
            var path = GetConfigPath();
            if (File.Exists(path))
            {
                var list = LoadFromFile<List<ShifterConfig>>(path);
                if (list != null)
                    return list.Where(w => config.ShowArchive || !w.Archive).OrderBy(w => w.Order).ToList();
            }

            return result;
        }

        #endregion

        #region ShifterConfigSettings

        public bool SaveShifterConfigSettings(ShifterConfigSettings shifterConfigSettings)
        {
            var settings = LoadSettings();
            if (settings != null) settings = new Settings();

            settings.ShifterConfigSettings = shifterConfigSettings;
            SaveSettings(settings);
            return true;
        }

        public ShifterConfigSettings LoadShifterConfigSettings()
        {
            var result = new ShifterConfigSettings();
            var settings = LoadSettings();
            if (settings != null && settings.ShifterConfigSettings != null) return settings.ShifterConfigSettings;

            return result;
        }

        #endregion

        #endregion

        #region private

        private T LoadFromFile<T>(string path)
        {
            using (var file = File.OpenText(path))
            {
                var serializer = new JsonSerializer();
                return (T) serializer.Deserialize(file, typeof(T));
            }
        }

        private void SaveToFile(string path, object obj)
        {
            var s = new JsonSerializerSettings {Formatting = Formatting.Indented};
            using (var file = File.CreateText(path))
            {
                var serializer = JsonSerializer.Create(s);
                serializer.Serialize(file, obj);
            }
        }

        private string GetConfigPath(ShifterConfigSettings configSettings)
        {
            if (configSettings != null)
                return Path.Combine(configSettings.ConfigPath, configSettings.FileName);

            return string.Empty;
        }

        private string GetConfigPath()
        {
            return GetConfigPath(LoadShifterConfigSettings());
        }

        private bool SaveSettings(Settings settings)
        {
            SaveToFile(_configPath, settings);
            return true;
        }


        private Settings LoadSettings()
        {
            var result = new Settings();
            if (File.Exists(_configPath))
            {
                var settings = LoadFromFile<Settings>(_configPath);
                if (settings != null) return settings;
            }

            return result;
        }

        #endregion
    }
}