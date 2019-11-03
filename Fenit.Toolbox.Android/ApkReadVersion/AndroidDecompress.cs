using System;
using System.Text;

namespace Fenit.Toolbox.Android.ApkReadVersion
{
    public class AndroidDecompress
    {
        private static readonly int endDocTag = 0x00100101;
        private static readonly int endTag = 0x00100103;
        private static readonly string spaces = "                                             ";
        private static readonly int startTag = 0x00100102;
        private string data = "";

        public ApkModel DecompressXml(byte[] xml)
        {
            var apkModel = new ApkModel();
            var numbStrings = Lew(xml, 4 * 4);
            var sitOff = 0x24;
            var stOff = sitOff + numbStrings * 4;
            var xmlTagOff = Lew(xml, 3 * 4);
            for (var ii = xmlTagOff; ii < xml.Length - 4; ii += 4)
                if (Lew(xml, ii) == startTag)
                {
                    xmlTagOff = ii;
                    break;
                }


            var off = xmlTagOff;
            var indent = 0;
            var startTagLineNo = -2;
            while (off < xml.Length)
            {
                var tag0 = Lew(xml, off);
                var lineNo = Lew(xml, off + 2 * 4);
                var nameNsSi = Lew(xml, off + 4 * 4);
                var nameSi = Lew(xml, off + 5 * 4);

                if (tag0 == startTag)
                {
                    var tag6 = Lew(xml, off + 6 * 4);
                    var numbAttrs = Lew(xml, off + 7 * 4);
                    off += 9 * 4;
                    var name = CompXmlString(xml, sitOff, stOff, nameSi);
                    startTagLineNo = lineNo;
                    var sb = "";
                    for (var ii = 0; ii < numbAttrs; ii++)
                    {
                        var attrNameNsSi = Lew(xml, off);
                        var attrNameSi = Lew(xml, off + 1 * 4);
                        var attrValueSi = Lew(xml, off + 2 * 4);
                        var attrFlags = Lew(xml, off + 3 * 4);
                        var attrResId = Lew(xml, off + 4 * 4);
                        off += 5 * 4;

                        var attrName = CompXmlString(xml, sitOff, stOff, attrNameSi);
                        var attrValue = attrValueSi != -1
                            ? CompXmlString(xml, sitOff, stOff, attrValueSi)
                            : attrResId.ToString();
                        sb += " " + attrName + "=\"" + attrValue + "\"";
                        if (attrName == "versionName")
                            apkModel.VersionName = attrValue;
                    }

                    PrtIndent(indent, "<" + name + sb + ">");
                    indent++;
                }
                else if (tag0 == endTag)
                {
                    indent--;
                    off += 6 * 4;
                    var name = CompXmlString(xml, sitOff, stOff, nameSi);
                    PrtIndent(indent, "</" + name + ">  \r\n");
                }
                else if (tag0 == endDocTag)
                {
                    break;
                }
                else
                {
                    Prt("  Unrecognized tag code '" + tag0.ToString("X")
                                                    + "' at offset " + off);
                    break;
                }
            }


            return apkModel;
        }


        private string CompXmlString(byte[] xml, int sitOff, int stOff, int strInd)
        {
            if (strInd < 0) return null;
            var strOff = stOff + Lew(xml, sitOff + strInd * 4);
            return CompXmlStringAt(xml, strOff);
        }


        private string CompXmlStringAt(byte[] arr, int strOff)
        {
            var strLen = ((arr[strOff + 1] << 8) & 0xff00) | (arr[strOff] & 0xff);
            var chars = new byte[strLen];
            for (var ii = 0; ii < strLen; ii++) chars[ii] = arr[strOff + 2 + ii * 2];


            return Encoding.UTF8.GetString(chars);
        }


        private static int Lew(byte[] arr, int off)
        {
            return (int) (((arr[off + 3] << 24) & 0xff000000) | ((arr[off + 2] << 16) & 0xff0000) |
                          ((arr[off + 1] << 8) & 0xff00) | (arr[off] & 0xFF));
        }

        private void Prt(string p)
        {
            data += p;
        }

        private void PrtIndent(int indent, string str)
        {
            Prt(spaces.Substring(0, Math.Min(indent * 2, spaces.Length)) + str);
        }
    }
}