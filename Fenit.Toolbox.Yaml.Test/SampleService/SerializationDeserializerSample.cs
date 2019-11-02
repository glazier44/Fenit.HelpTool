using System;
using System.Collections.Generic;
using System.Text;
using Fenit.Toolbox.Yaml.Test.Models;
using YamlDotNet.Serialization;

namespace Fenit.Toolbox.Yaml.Test.SampleService
{
    public class SerializationDeserializerSample
    {

        public void First()
        {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var dict = deserializer.Deserialize<Dictionary<string, string>>("hello: world");
            Console.WriteLine(dict["hello"]);
        }

        public void ClassDes()
        {
            var yamlInput = 
@"- Name: Oz-Ware
  PhoneNumber: 123456789";
            var deserializer = new DeserializerBuilder()
                .Build();

            var contacts = deserializer.Deserialize<List<Contact>>(yamlInput);
            Console.WriteLine(contacts[0]);
        }
    }
}
