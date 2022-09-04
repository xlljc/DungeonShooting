
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

public static class ZipUtils
{
    
    /// <summary>
    /// 压缩文件
    /// </summary>
    /// <param name="strFile">文件路径</param>
    /// <param name="strZip">输出zip路径</param>
    public static void ZipFile(string strFile, string strZip)
    {
        ZipOutputStream outstream = new ZipOutputStream(File.Create(strZip));
        outstream.SetLevel(6);
        ZipCompress(strFile, outstream, strFile);
        outstream.Finish();
        outstream.Close();
    }

    private static void ZipCompress(string strFile, ZipOutputStream outstream, string staticFile)
    {
        Crc32 crc = new Crc32();
        //获取指定目录下所有文件和子目录文件名称
        string[] filenames = Directory.GetFileSystemEntries(strFile);
        //遍历文件
        foreach (string file in filenames)
        {
            if (Directory.Exists(file))
            {
                ZipCompress(file, outstream, staticFile);
            }
            //否则，直接压缩文件
            else
            {
                //打开文件
                FileStream fs = File.OpenRead(file);
                //定义缓存区对象
                byte[] buffer = new byte[fs.Length];
                //通过字符流，读取文件
                fs.Read(buffer, 0, buffer.Length);
                //得到目录下的文件（比如:D:\Debug1\test）,test
                string tempfile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                ZipEntry entry = new ZipEntry(tempfile);
                entry.DateTime = DateTime.Now;
                entry.Size = fs.Length;
                fs.Close();
                crc.Reset();
                crc.Update(buffer);
                entry.Crc = crc.Value;
                outstream.PutNextEntry(entry);
                //写文件
                outstream.Write(buffer, 0, buffer.Length);
            }
        }
    }

}