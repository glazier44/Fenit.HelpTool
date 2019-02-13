using System;
using System.Collections.Generic;
using System.IO;
using Fenit.HelpTool.Core.Service.Abstract;
using Fenit.HelpTool.Core.Service.Model.Shifter;
using Newtonsoft.Json;

namespace Fenit.HelpTool.Core.SerializationService.Implement
{
    public class SerializationService : ISerializationService
    {
        private readonly string configPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ShifterConfig.json");

        public bool SaveShifterConfig(List<ShifterConfig> shifterConfig)
        {
            SaveToFile(configPath, shifterConfig);
            return true;
        }

        public List<ShifterConfig> LoadConfig()
        {
            var result = new List<ShifterConfig>();
            if (File.Exists(configPath))
            {
                var list = LoadFromFile<List<ShifterConfig>>(configPath);
                if (list != null)
                {
                    return list;
                }
            }

            return result;
        }

        private T LoadFromFile<T>(string path)
        {
            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
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
    }
}