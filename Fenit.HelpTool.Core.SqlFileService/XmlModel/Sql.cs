using System.Collections.Generic;
using System.Xml.Serialization;

namespace Fenit.HelpTool.Core.SqlFileService.XmlModel
{
    [XmlRoot(ElementName = "Param")]
    public class Params
    {
        [XmlAttribute(AttributeName = "name")] public string Name { get; set; }
        [XmlAttribute(AttributeName = "type")] public string Type { get; set; }

        [XmlAttribute(AttributeName = "direction")]
        public string Direction { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Method")]
    public class Method
    {
        [XmlAttribute(AttributeName = "name")] public string Name { get; set; }

        [XmlAttribute(AttributeName = "module")]
        public string Module { get; set; }

        [XmlAttribute(AttributeName = "base")] public string Base { get; set; }

        [XmlElement(ElementName = "Parameter")]
        public List<Parameter> Parameter { get; set; }
    }

    [XmlRoot(ElementName = "Parameter")]
    public class Parameter
    {
        [XmlAttribute(AttributeName = "name")] public string Name { get; set; }
        [XmlAttribute(AttributeName = "type")] public string Type { get; set; }
    }

    [XmlRoot(ElementName = "StackTrace")]
    public class StackTrace
    {
        [XmlElement(ElementName = "Method")] public List<Method> Method { get; set; }
    }

    [XmlRoot(ElementName = "Sql")]
    public class Sql
    {
        [XmlElement(ElementName = "ErrorInfo")]
        public ErrorInfo ErrorInfo { get; set; }

        [XmlElement(ElementName = "Param")] public List<Param> Param { get; set; }

        [XmlElement(ElementName = "StackTrace")]
        public StackTrace StackTrace { get; set; }

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
