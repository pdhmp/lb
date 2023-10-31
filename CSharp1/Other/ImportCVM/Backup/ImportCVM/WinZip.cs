using System;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace ImportCVM
{
    static class WinZip
    {

        public static void ZipFiles(string[] fileNames, string outFileName)
        {
            byte[] buffer;

            using (FileStream zip = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (ZipOutputStream outstream = new ZipOutputStream(zip))
                {
                    foreach (string infileName in fileNames)
                    {
                        using (FileStream inputStream = new FileStream(infileName, FileMode.Open, FileAccess.Read))
                        {
                            buffer = new byte[inputStream.Length];
                            inputStream.Read(buffer, 0, buffer.Length);

                            ZipEntry entry = new ZipEntry(infileName.Substring(infileName.LastIndexOf('\\') + 1));
                            outstream.PutNextEntry(entry);
                            outstream.Write(buffer, 0, buffer.Length);
                        }
                    }
                }
            }
        }

        public static void UnZipFiles(string zipPathAndFile, string outputFolder, string password, bool deleteZipFile)
        {
            ZipInputStream s = new ZipInputStream(File.OpenRead(zipPathAndFile));
            if (password != null && password != String.Empty)
                s.Password = password;
            ZipEntry theEntry;
            string tmpEntry = String.Empty;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string directoryName = outputFolder;
                string fileName = Path.GetFileName(theEntry.Name);
                // create directory 
                if (directoryName != "")
                {
                    Directory.CreateDirectory(directoryName);
                }
                if (fileName != String.Empty)
                {
                    if (theEntry.Name.IndexOf(".ini") < 0)
                    {
                        string fullPath = directoryName + "\\" + theEntry.Name;
                        fullPath = fullPath.Replace("\\ ", "\\");
                        string fullDirPath = Path.GetDirectoryName(fullPath);
                        if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath);
                        FileStream streamWriter = File.Create(fullPath);
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        streamWriter.Close();
                    }
                }
            }
            s.Close();
            if (deleteZipFile)
                File.Delete(zipPathAndFile);
        }
    }
}
