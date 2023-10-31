using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace FTPUpload
{
    class Class1
    {

        public static void Upload(string file, bool Cotas)
        {
            string filename = @"C:\NESTDATA\"+ file +".xml";

            if (Cotas)
            {
                filename = @"C:\NESTDATA\" + file + ".xls";
            }

            FileInfo fileInf = new FileInfo(filename);
            
            string uri = "ftp://ftp.nestinvestimentos.com.br/" + fileInf.Name;
            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create
                     (new Uri("ftp://ftp.nestinvestimentos.com.br/" + fileInf.Name));

            if (Cotas)
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create
                         (new Uri("ftp://ftp.nestinvestimentos.com.br/relatorios/" + fileInf.Name));
            }

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential("nestxml", "Z8Cy3fAY");
            if (Cotas)
            {
                reqFTP.Credentials = new NetworkCredential("nestfiles", "Z8Cy3fAY");
            }

            // By default KeepAlive is true, where the control connection
            // is not closed after a command is executed.
            reqFTP.KeepAlive = false;            

            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.
            reqFTP.UseBinary = true;

            // Notify the server about the size of the uploaded file
            reqFTP.ContentLength = fileInf.Length;

            // The buffer size is set to 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            // Opens a file stream (System.IO.FileStream) to read the file
            // to be uploaded
            FileStream fs = fileInf.OpenRead();

            try
            {
                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload
                    // Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Upload Error");
            }
        }

        public static void Upload(string file)
        {
            Upload(file, false);
        }
    }
}
