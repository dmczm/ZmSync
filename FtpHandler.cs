using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.Diagnostics;
//using System.Windows.Forms;
//using System.Linq;
//using System.Xml.Linq;
using System.IO;
using System.Net;
using System.Text;
namespace ZmSync
{

	public class FtpHandler
	{
		// Methods
		public FtpHandler(string ftpServer, string ftpPort, string ftpUser, string ftpPassword, string ftpname)
		{
			this.ftpServer = ftpServer;
			this.ftpPort = ftpPort;
			this.ftpUser = ftpUser;
			this.ftpPassword = ftpPassword;
			this.ftpname = ftpname;
		}
		public void Close()
		{
			this.ftpServer = null;
			this.ftpPort = null;
			this.ftpUser = null;
			this.ftpPassword = null;
			this.ftpname = null;
		}

//		private static string ConvertToFtpDirPath(string filePath)
//		{
//			string str = "/";
//			if ((((filePath != null)) && (!string.IsNullOrEmpty(filePath.Trim())))) {
//				str = filePath.Trim();
//				if (str.Contains("\\")) {
//					str = str.Replace("\\", "/");
//				}
//				if ((str.Substring(0, 1) != "/")) {
//					str = ("/" + str);
//				}
//				if ((str.Substring((str.Length - 1), 1) != "/")) {
//					str = (str + "/");
//				}
//			}
//			return str;
//		}

//		public void DownloadFile(string fileName, string ftpDirPath, string localSaveDirPath)
//		{
//			try {
//				if (fileName.Contains(".")) {
//					string str = ConvertToFtpDirPath(ftpDirPath);
//					FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Concat(new string[] {
//						@"ftp://",
//						this.ftpServer,
//						@":",
//						this.ftpPort,
//						str,
//						fileName
//					})));
//					if ((this.ftpCredential == null)) {
//						this.ftpCredential = new NetworkCredential(this.ftpUser, this.ftpPassword);
//					}
//					request.Credentials = this.ftpCredential;
//					request.Method = "RETR";
//					request.UseBinary = true;
//					FileStream stream = new FileStream((localSaveDirPath + fileName), FileMode.Create);
//					FtpWebResponse response = (FtpWebResponse)request.GetResponse();
//					Stream responseStream = response.GetResponseStream();
//					long contentLength = response.ContentLength;
//					//int count = 0x800;
//					int count = 2048;
//					byte[] buffer = new byte[count];
//					int i = responseStream.Read(buffer, 0, count);
//					while ((i > 0)) {
//						stream.Write(buffer, 0, i);
//						i = responseStream.Read(buffer, 0, count);
//					}
//					responseStream.Close();
//					stream.Close();
//					response.Close();
//				}
//			} catch (Exception) {
//                //throw;
//			}
//		}
		
//		public List<string> GetFileList(string ftpDirPath)
//		{
//			List<string> list = new List<string>();
//			try {
//				string str = this.ConvertToFtpDirPath(ftpDirPath);
//				FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Concat(new string[] {
//					"ftp://",
//					this.ftpServer,
//					":",
//					this.ftpPort,
//					str
//				})));
//				if ((this.ftpCredential == null)) {
//					this.ftpCredential = new NetworkCredential(this.ftpUser, this.ftpPassword);
//				}
//				request.Credentials = this.ftpCredential;
//				request.Method = "NLST";
//				request.UseBinary = true;
//				request.UsePassive = true;
//				using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) {
//					//Using reader As StreamReader = New StreamReader(response.GetResponseStream)
//					using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default)) {
//						string str3 = "";
//						//Do While (Not str3 = reader.ReadLine Is Nothing)
//						while ((str3 = reader.ReadLine())!= null) {
//							list.Add(str3);
//						}
//					}
//					return list;
//				}
//			} catch (Exception obj1) {
//				throw;
//			}
//			return list;
//		}

	
		public void UpLoadFile1(string fullname)
		{
			GC.Collect();
//			FileInfo fileInf = new FileInfo(filename);
			string FileName=Path.GetFileName(fullname);
			FtpWebRequest reqFTP = null;
			string str1 = null;
			//str1 = String.Concat(New String() {Me.ftpServer, ":", Me.ftpPort, "/", fileInf.Name})
			str1 = this.ftpServer + @":" + this.ftpPort + @"/" + FileName;

			reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(str1));
			reqFTP.Credentials = new NetworkCredential(ftpUser, ftpPassword);
			reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
			reqFTP.KeepAlive = false;
			reqFTP.UseBinary = true;
			reqFTP.UsePassive = true;
			
//			reqFTP.ContentLength = fileInf.Length;
			const int bufferLength = 2048;
//			byte[] buffer = new byte[buffLength + 1];
			byte[] buffer = new byte[bufferLength];

//			FileStream stream = fileInf.OpenRead();
//			Stream requestStream = null;
			int readBytes = 0;
			try {
//				using (FileStream stream = fileInf.OpenRead()){
				using (FileStream stream = File.OpenRead(fullname)){
					using (Stream requestStream = reqFTP.GetRequestStream()){
						do
						{
							readBytes = stream.Read(buffer, 0, bufferLength);
							requestStream.Write(buffer, 0, readBytes);
						}
						while (readBytes != 0);
//						int i = stream.Read(buffer, 0, bufferLength);
//						while ((i != 0)) {
//							requestStream.Write(buffer, 0, i);
//							i = stream.Read(buffer, 0, bufferLength);
//						}
					}
				}
			} catch (Exception ex) {
				//Form1.追加日志(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ex.ToString + vbCrLf)
				throw new Exception(ex.Message);
			} 
		}
		public void UpLoadFileport(string fullname,bool usepassive)
		{
			GC.Collect();
//			FileInfo fileInf = new FileInfo(filename);
			string FileName=Path.GetFileName(fullname);
			FtpWebRequest reqFTP = null;
			string str1 = null;
			//str1 = String.Concat(New String() {Me.ftpServer, ":", Me.ftpPort, "/", fileInf.Name})
			str1 = this.ftpServer + @":" + this.ftpPort + @"/" + FileName;

			reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(str1));
			reqFTP.Credentials = new NetworkCredential(ftpUser, ftpPassword);
			reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
			reqFTP.KeepAlive = false;
			reqFTP.UseBinary = true;
			reqFTP.UsePassive = usepassive;
			
//			reqFTP.ContentLength = fileInf.Length;
			const int bufferLength = 2048;
//			byte[] buffer = new byte[buffLength + 1];
			byte[] buffer = new byte[bufferLength];

//			FileStream stream = fileInf.OpenRead();
//			Stream requestStream = null;
			int readBytes = 0;
			try {
//				using (FileStream stream = fileInf.OpenRead()){
				using (FileStream stream = File.OpenRead(fullname)){
					using (Stream requestStream = reqFTP.GetRequestStream()){
						do
						{
							readBytes = stream.Read(buffer, 0, bufferLength);
							requestStream.Write(buffer, 0, readBytes);
						}
						while (readBytes != 0);
//						int i = stream.Read(buffer, 0, bufferLength);
//						while ((i != 0)) {
//							requestStream.Write(buffer, 0, i);
//							i = stream.Read(buffer, 0, bufferLength);
//						}
					}
				}
			} catch (Exception ex) {
				//Form1.追加日志(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ex.ToString + vbCrLf)
				throw new Exception(ex.Message);
			} 
		}

		/// <summary>  
		/// 获取当前目录下明细(包含文件和文件夹)  
		/// </summary>  
		public string GetFilesDetailList()
		{
			try {
				StringBuilder result = new StringBuilder();
				FtpWebRequest ftp ;
//				ftp = (FtpWebRequest)WebRequest.Create(new Uri(string.Concat(new string[] {
//					this.ftpServer,
//					":",
//					this.ftpPort + "/"
//				})));
//				
				ftp = (FtpWebRequest)WebRequest.Create(new Uri(this.ftpServer+@":"+this.ftpPort + @"/"));
				ftp.Credentials = new NetworkCredential(ftpUser, ftpPassword);
				//ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails
				ftp.Method = WebRequestMethods.Ftp.ListDirectory;
				ftp.KeepAlive=false;
//				WebResponse response = ftp.GetResponse();
//				StreamReader reader = new StreamReader(response.GetResponseStream());
				FtpWebResponse response = (FtpWebResponse) ftp.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				//Dim reader As StreamReader = New StreamReader(response.GetResponseStream(), Encoding.Default)
				string line = null;
				line = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ftpname + "当前FTP文件列表:";

				//line = reader.ReadLine()
				while ((line != null)) {
					result.Append(line);
					//result.Append("\n")
					result.Append(Environment.NewLine);
					line = reader.ReadLine();
				}
				//result.Remove(result.ToString().LastIndexOf("\n"), 1)
				result.Remove(result.ToString().LastIndexOf(Environment.NewLine), 1);
				//删除多余的一个回车
				reader.Close();
				response.Close();
				//Return result.ToString().Split("\n")
				return result.ToString();
				//.Split(vbCrLf)
			} catch (Exception ex) {
				return ex.Message;
//				throw new Exception(ex.Message);
			}
		}


		// Fields
//		private NetworkCredential ftpCredential;
		private string ftpPassword;
		private string ftpPort;
		private string ftpServer;
		private string ftpUser;
		private string ftpname;
	}
}


