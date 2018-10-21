using System.Collections.Generic;
using System.Xml.Serialization;
using Fenit.HelpTool.Core.SqlFileService.Enum;

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


        [XmlIgnore]
        public ParamType ParamType
        {
            get
            {
                switch (Direction)
                {
                    case "Input":
                    {
                        return ParamType.Input;
                    }
                    case "Output":
                    {
                        return ParamType.Output;
                    }
                    case "ReturnValue":
                    {
                        return ParamType.Result;
                    }
                    default:
                        return ParamType.Non;
                }
            }
        }
  

    }
}