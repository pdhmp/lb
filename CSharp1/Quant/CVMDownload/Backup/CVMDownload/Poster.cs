using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace CVMDownload
{
    class Poster
    {
        public string HttpPost(string uri, string parameters)
        {
            WebRequest webRequest = WebRequest.Create(uri);

            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(parameters);
            Stream os = null;
            try
            { // send the Post
                webRequest.ContentLength = bytes.Length;   //Count bytes to send
                os = webRequest.GetRequestStream();
                os.Write(bytes, 0, bytes.Length);         //Send it
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "HttpPost: Request error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (os != null)
                {
                    os.Close();
                }
            }

            try
            { 
                WebResponse webResponse = webRequest.GetResponse();
                if (webResponse == null)
                { return null; }
                StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.GetEncoding(28591));
                
                return sr.ReadToEnd().Trim();
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "HttpPost: Response error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        } 

        public void fileWrite(string FileName, string StringToWrite)
        {
            FileStream fout;

            try
            {
                fout = new FileStream(FileName, FileMode.Create);
            }
            catch (IOException exc)
            {
                Console.WriteLine(exc.Message + "\nError Opening Output File");
                return;
            }

            try
            {
                foreach (char ch in StringToWrite)
                {
                    fout.WriteByte((Byte)ch);
                }
            }
            catch (IOException exc)
            {
                Console.WriteLine(exc.Message + "File Error");
            }

            fout.Close();
        } 

    
    }
}
