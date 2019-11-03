using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Fenit.Toolbox.Android.ApkReadVersion
{
    public class ApkReader
    {
        public ApkModel ApkModel { get; private set; }

        public void Read(string apkPath)
        {
            var zip =
                new ZipInputStream(File.OpenRead(apkPath));
            var filestream = new FileStream(apkPath, FileMode.Open, FileAccess.Read);
            var zipfile = new ZipFile(filestream);
            ZipEntry item;


            while ((item = zip.GetNextEntry()) != null)
                if (item.Name == "AndroidManifest.xml")
                {
                    var bytes = new byte[50 * 1024];

                    var strm = zipfile.GetInputStream(item);
                    var size = strm.Read(bytes, 0, bytes.Length);

                    using (var s = new BinaryReader(strm))
                    {
                        var bytes2 = new byte[size];
                        Array.Copy(bytes, bytes2, size);
                        var decompress = new AndroidDecompress();
                        ApkModel = decompress.DecompressXml(bytes);
                    }
                }
        }
    }
}