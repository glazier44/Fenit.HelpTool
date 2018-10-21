using System.Collections.Generic;
using System.Xml.Serialization;

namespace Fenit.HelpTool.Core.SqlFileService.XmlModel
{
    [XmlRoot(ElementName = "Sql")]
    public class Sql
    {
        [XmlElement(ElementName = "Param")] public List<Param> Param { get; set; }

        [XmlAttribute(AttributeName = "executionStart")]
        public string ExecutionStart { get; set; }

        [XmlAttribute(AttributeName = "executionEnd")]
        public string ExecutionEnd { get; set; }

        [XmlAttribute(AttributeName = "executionTime")]
        public string ExecutionTime { get; set; }

        [XmlAttribute(AttributeName = "executionTimeMs")]
        public string ExecutionTimeMs { get; set; }

        [XmlAttribute(AttributeName = "success")]
        public string Success { get; set; }

        [XmlAttribute(AttributeName = "status")]
        public string Status { get; set; }

        [XmlAttribute(AttributeName = "operationName")]
        public string OperationName { get; set; }

        [XmlIgnore] public string SqlCommand { get; set; }
    }
}