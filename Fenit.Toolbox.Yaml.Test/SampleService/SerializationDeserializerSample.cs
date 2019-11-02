using System;
using System.Collections.Generic;
using System.Text;
using Fenit.HelpTool.Core.Service.Model.Settings;
using Fenit.Toolbox.Yaml.Test.Models;
using InstallPackageLib.ProgramsType;
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

        public void Test()
        {
            var yaml = @"- Programs:
  - Name: ApplicationHost
  - Name: SerwerSerializacji
  - Name: CmdServer
  - Name: Mobmago
  - Name: AppLoader
  Name: Serwerowe
- Programs:
  - Name: KomoraCE
  - Name: ZatowCE
  - Name: KodyKreskowe
  - Name: Inwentaryzacja
  - Name: MagazynCE
  - Name: StacjaWydań
  - Name: StacjaWydań
  Name: Mobilny
- Programs:
  - Name: Zwroty
  - Name: DIZ
  Name: Desktop
- Programs:
  - Name: Komora
  - Name: KodyKreskowe
  Name: Android
- Programs:
  - Name: HelpTool
  Name: Inne";

            var deserializer = new DeserializerBuilder()
                .Build();

            var contacts = deserializer.Deserialize<List<ProgramType>>(yaml);
            Console.WriteLine(contacts);
        }
    }
}
