using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;

namespace Fenit.Toolbox.Yaml.Extension
{
    public static class Extensions
    {
        public static string SerializationYaml(this List<object> srcDir)
        {
            using (var stringWriter = new StringWriter())
            {
                var serializer = new Serializer();
                serializer.Serialize(stringWriter, srcDir);
                return stringWriter.ToString();
            }
        }

        public static List<T> DeserializationYaml<T>(this string @string)
        {

            var deserializer = new DeserializerBuilder()
                .Build();

            var contacts = deserializer.Deserialize<List<T>>(@string);
            return contacts;
        }
    }
}