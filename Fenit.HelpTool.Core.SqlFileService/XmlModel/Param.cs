using System.Xml.Serialization;

namespace Fenit.HelpTool.Core.SqlFileService.XmlModel
{
    [XmlRoot(ElementName = "Param")]
    public class Param
    {
        [XmlAttribute(AttributeName = "name")] public string Name { get; set; }

        [XmlAttribute(AttributeName = "type")] public string Type { get; set; }

        [XmlAttribute(AttributeName = "direction")]
        public string Direction { get; set; }

        [XmlText] public string Text { get; set; }


    }
}