using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Utility
{
    public class Ftp
    {
        public Ftp()
        {
        }

        public Ftp(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public Ftp(string connectionString, string userName, string password) : this(connectionString)
        {
            this.UserName = userName;
            this.Password = password;
        }

        private string _connectionString;

        public string ConnectionString
        {
            get
            {
                return _connectionString.TrimEnd('/');
            }
            set
            {
                _connectionString = value;
            }
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string GetResponse(string fileName)
        {
            string r = string.Empty;
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ConnectionString + "/" + fileName);
            req.Method = WebRequestMethods.Ftp.DownloadFile;
            req.Credentials = new NetworkCredential(UserName, Password);

            FtpWebResponse response = (FtpWebResponse)req.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            r = reader.ReadToEnd();
            reader.Close();
            response.Close();

            return r;
        }

        public bool UploadFile(string path, string uploadFileName)
        {
            FtpWebRequest request = CreateFtpRequestObject(uploadFileName);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            byte[] fileContents = File.ReadAllBytes(path);
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.Close();
            return response.StatusCode == FtpStatusCode.CommandOK;
        }

        public bool DeleteFile(string fileName)
        {
            FtpWebRequest req = CreateFtpRequestObject(fileName);
            req.Method = WebRequestMethods.Ftp.DeleteFile;
            FtpWebResponse res = (FtpWebResponse)req.GetResponse();
            res.Close();
            return res.StatusCode == FtpStatusCode.CommandOK;
        }

        private FtpWebRequest CreateFtpRequestObject(string fileName)
        {
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ConnectionString + "/" + fileName);
            req.Credentials = new NetworkCredential(UserName, Password);
            return req;
        }
    }
}
