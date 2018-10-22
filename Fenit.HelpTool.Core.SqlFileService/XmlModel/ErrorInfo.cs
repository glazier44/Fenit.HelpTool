using System.Xml.Serialization;

namespace Fenit.HelpTool.Core.SqlFileService.XmlModel
{
    [XmlRoot(ElementName = "ErrorInfo")]
    public class ErrorInfo
    {
        [XmlAttribute(AttributeName = "errCode")]
        public string ErrCode { get; set; }

        [XmlText] public string Text { get; set; }
    }
}