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
//using System.Net;
using System.Text;
//using System.Threading;
using System.Globalization; //CultureInfo.InvariantCulture;

namespace ZmSync
{

	public static class Txthelper
	{
		public static void WriteLine(string tmpPath, string m)
		{
			try {
				using (StreamWriter sw = new StreamWriter(tmpPath, true)){
					sw.WriteLine(m);
					sw.Flush();
//					sw.Close();
//					sw.Dispose();
	       		}
			} catch {
			}
		}
		public static void WriteLine(string tmpPath, string m, Encoding encoding)
		{
			try {
				using (StreamWriter sw = new StreamWriter(tmpPath, true, encoding)){
					sw.WriteLine(m);
					sw.Flush();
//					sw.Close();
//					sw.Dispose();
				}
			} catch {
			} 
		}
		public static string ReadFile(string tmpPath)
		{
			string str = "";
			//str = File.ReadAllText(TmpPath)
			if (System.IO.File.Exists(tmpPath)) {
				using (StreamReader sr = new StreamReader(tmpPath,Encoding.UTF8)){
					str = sr.ReadToEnd();
//					sr.Close();
				}
			}
			return str;
		}
		public static string ReadFile1(string tmpPath)
		{
			string str = "";
			string[] readText;
			//str = File.ReadAllText(TmpPath)
			if (System.IO.File.Exists(tmpPath)) {
				readText = File.ReadAllLines(tmpPath);
			
	            if(readText.Length>1000)
	            {
	                for (int i = readText.Length - 1000; i <= readText.Length-1; i++)
	                {
	                    str+=readText[i]+Environment.NewLine;
	                }
	            }else
	                for(int i=0;i<readText.Length;i++)
	                {
	                    str+=readText[i]+Environment.NewLine;
	                }
			}
//            return strSol + listFile[listFile.Count-1];
			return str;
		}
//        public static string Readtxt(string tmpPath)
//		{
//			string line = null;
//			string sql = null;
//			//sql = File.ReadAllText(TmpPath)
//			StringBuilder contents = new StringBuilder();
//			using (StreamReader sr = new StreamReader(tmpPath, Encoding.Default)) {
//
//				//Read the first line of text
//				line = sr.ReadLine();
//				contents.Append(line);
//				contents.Append(Environment.NewLine);
//				//Continue to read until you reach end of file
//				while ((!sr.EndOfStream)) {
//					//Read the next line
//					line = sr.ReadLine();
//					contents.Append(line);
//					contents.Append(Environment.NewLine);
//				}
//				//close the file
//				//sr.Close()
//			}
//			sql = contents.ToString();
//			contents.Clear();
//			return sql;
//		}

		public static void Write(string tmpPath, string m)
		{
			try {
				using(FileStream fs = new FileStream(tmpPath, FileMode.Create,FileAccess.Write)){
						fs.SetLength(0);
					}
				using (StreamWriter sw = new StreamWriter(tmpPath, true)){
					sw.Write(m);
					sw.Flush();
//					sw.Close();
//					sw.Dispose();
	       		}
			} catch {
			}
		}

    }
}


