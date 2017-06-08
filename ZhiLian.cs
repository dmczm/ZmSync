/*
 * 由SharpDevelop创建。
 * 用户： zm
 * 日期: 2016/5/17
 * 时间: 15:54
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Globalization; //CultureInfo.InvariantCulture;
using System.IO;
//using System.IO.Compression;
using System.Data;
//using System.Data.OleDb;
//using System.Data.SqlClient;
//using System.Threading;
using System.Text;
//using System.Linq;
//using System.Net;
using System.Xml;
using System.Security.Cryptography;
//using OfficeOpenXml;
//using Excel = Microsoft.Office.Interop.Excel;

namespace ZmSync
{
	/// <summary>
	/// Description of ZhiLian.
	/// </summary>
	public static class ZhiLian
	{
	
		private static string NowTime(string format)
        {
        	return DateTime.Now.ToString(format,CultureInfo.InvariantCulture);
        }
		public static void 追加日志(string txt)
		{
//			Txthelper tx = new Txthelper();
			Txthelper.WriteLine(Directory.GetCurrentDirectory() + "\\Log\\project" 
        	          + DateTime.Now.ToString("yyyyMM",CultureInfo.InvariantCulture) + ".log", 
        	          DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss",CultureInfo.InvariantCulture)+ " " +txt);
		}

        public static void 追加日志(string fileName,string txt)
        {
//            Txthelper tx = new Txthelper();
            Txthelper.WriteLine(fileName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss",CultureInfo.InvariantCulture)+ " " +txt);
        }
        public static void XmlSync()
        {
			try{
        		string TmpPath = Directory.GetCurrentDirectory();// + "\\config\\";
	            string xmlFilePath = TmpPath + "\\Config.xml";
	            XmlDocument xmlDoc = new XmlDocument();
	            XmlReaderSettings settings = new XmlReaderSettings();
	   			settings.IgnoreComments = true;//忽略文档里面的注释
	   			XmlReader reader = XmlReader.Create(xmlFilePath, settings);
	   			xmlDoc.Load(reader);
	//            doc.Load(xmlFilePath);
	            //使用xpath表达式选择文档中所有的Factory子节点
	            XmlNodeList factoryNodeList = xmlDoc.SelectNodes(@"/Config/Job");
	            if (factoryNodeList != null)
	            {
	                foreach (XmlNode factoryNode in factoryNodeList)
	                {
	                    //通过Attributes获得属性名字为name的属性
	                    string name = factoryNode.Attributes["name"].Value;
	                    追加日志(name+"任务启动");
							string OldFile = factoryNode["OldFile"].InnerText;
							string NewFile = factoryNode["NewFile"].InnerText;
							string Day = factoryNode["Day"].InnerText;
							int result=0;
							if(string.IsNullOrEmpty(Day) || Day=="0"){
								result=0;
							}else{
								result=int.Parse(Day);
							}
							CopyDirectory(OldFile, NewFile,result);
	                } 
	            }
	            reader.Close();
			}catch (Exception ex){
				追加日志(ex.Message);
			}
		}
       public static void XmlSync(string m)
        {
			try{
        		string TmpPath = Directory.GetCurrentDirectory();// + "\\config\\";
	            string xmlFilePath = TmpPath + "\\Config.xml";
	            XmlDocument xmlDoc = new XmlDocument();
	            XmlReaderSettings settings = new XmlReaderSettings();
	   			settings.IgnoreComments = true;//忽略文档里面的注释
	   			XmlReader reader = XmlReader.Create(xmlFilePath, settings);
	   			xmlDoc.Load(reader);
	//            doc.Load(xmlFilePath);
	            //使用xpath表达式选择文档中所有的Factory子节点
	            XmlNodeList factoryNodeList = xmlDoc.SelectNodes(@"/Config/Job");
	            if (factoryNodeList != null)
	            {
	                foreach (XmlNode factoryNode in factoryNodeList)
	                {
	                	//通过Attributes获得属性名字为name的属性
	                	string name = factoryNode.Attributes["name"].Value;
	                	if (name==m){
		                    追加日志(name+"任务启动");
								string OldFile = factoryNode["OldFile"].InnerText;
								string NewFile = factoryNode["NewFile"].InnerText;
								string Day = factoryNode["Day"].InnerText;
								int result=0;
								if(string.IsNullOrEmpty(Day) || Day=="0"){
									result=0;
								}else{
									result=int.Parse(Day);
								}
								CopyDirectory(OldFile, NewFile,result);
	                	}
	                } 
	            }
	            reader.Close();
			}catch (Exception ex){
				追加日志(ex.Message);
			}
		}
        
        /// <summary>
        /// 显示Config配置参数信息
        /// </summary>
        public static void GetXmlInfo()
        {
        	DataSet myds = new DataSet();
        	DataTable dt = new DataTable();
        	dt.Columns.Add("任务名称");
        	dt.Columns.Add("OldFile");
			dt.Columns.Add("NewFile");
			dt.Columns.Add("保留文件天数");
			string TmpPath = Directory.GetCurrentDirectory(); //+ "\\config\\";
            string xmlFilePath = TmpPath + "\\Config.xml";
            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
   			settings.IgnoreComments = true;//忽略文档里面的注释
   			XmlReader reader = XmlReader.Create(xmlFilePath, settings);
   			xmlDoc.Load(reader);
   			int j=0;
//            doc.Load(xmlFilePath);
            //使用xpath表达式选择文档中所有的Factory子节点
            XmlNodeList factoryNodeList = xmlDoc.SelectNodes(@"/Config/Job");
            if (factoryNodeList != null)
            {
                foreach (XmlNode factoryNode in factoryNodeList)
                {
							dt.Rows.Add(factoryNode.Attributes["name"].Value);							
		        			for (int i = 0; i < factoryNode.ChildNodes.Count; i++)
        					{
					            string colName = factoryNode.ChildNodes.Item(i).InnerText;
					            dt.Rows[j][i+1]=colName;
        					}
							j++;
                } 
            }
            reader.Close();
			myds.Tables.Add(dt);
            Form4 Form4 = new Form4();
	        Form4.DS = myds;
	        Form4.Show();
	        dt.Dispose();
//            dataGridView1.DataSource = myds.Tables[0];
        }


        
        /// <summary>
        /// 获取XML中定义的DdiStartTime自动执行时间
        /// </summary>
        /// <param name="xmlFilePath">XML文件名</param>
        /// <returns>将时间返回字符串</returns>
        public static string GetXmlDdiStartTime(string xmlFilePath)
        {
//        	string TmpPath = Directory.GetCurrentDirectory(); //+ "\\config\\";
//          string xmlFilePath = TmpPath + "\\Config.xml";
			string time;
            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
   			settings.IgnoreComments = true;//忽略文档里面的注释
   			XmlReader reader = XmlReader.Create(xmlFilePath, settings);
   			try{
	   			xmlDoc.Load(reader);
				XmlNode SysConfigNode = xmlDoc.SelectSingleNode(@"/Config/SysConfig");
	//			XmlElement xe = (XmlElement)SysConfigNode;
				time = SysConfigNode["DdiStartTime"].InnerText;
				time = Convert.ToDateTime(time).ToString();
				string time1 = Convert.ToDateTime(time).AddMinutes(10).ToString();
	//            追加日志(time+" "+ time1);
				reader.Close();
	            return time;
   			}catch(Exception ex){
   				追加日志(ex.Message);
   				return Convert.ToDateTime("23:45:00").ToString();
   			}finally{
   				if(reader!=null){
   					reader.Close();
   				}
   			}	
        }
        
        public  static void   CopyDirectory(   string   sourceDirName,   string   destDirName,int delflag)   
        {
            try
            {
                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                    File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));

                }

                if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
                    destDirName = destDirName + Path.DirectorySeparatorChar;

                string[] files = Directory.GetFiles(sourceDirName);
                
                foreach (string file in files)
                {                      
                    if(File.Exists(destDirName + Path.GetFileName(file)))
//                    	continue;
                    {
                    	ZhiLian.追加日志(Path.GetFileName(file)+" 已进行过同步，本次无需同步");
                    }else{       
	                    
	                    FileInfo fi = new FileInfo(file);
	                    string filelength="";
	                    if(fi.Length<1024*1024){
	                    	filelength=fi.Length/1024+"KB";
	                    }else if(fi.Length>=1024*1024 && fi.Length<1024*1024*1024){
	                    	filelength=fi.Length/1024/1024+"MB";
	                    }else if(fi.Length>=1024*1024*1024){
	                    	filelength=fi.Length/1024/1024/1024+"GB";
	                    }
	                    ZhiLian.追加日志(Path.GetFileName(file)+" 准备进行源文件SHA1校验，文件大小为"+filelength);
	                    string sha1old=GetFileSHA1(file);
	                    ZhiLian.追加日志(Path.GetFileName(file)+" 源文件SHA1值为:"+sha1old);
	                    ZhiLian.追加日志(Path.GetFileName(file)+" 文件同步开始");
	//                    File.Copy(file, destDirName + Path.GetFileName(file),true);
	                    File.Copy(file, destDirName + Path.GetFileName(file));
	                    
	                    ZhiLian.追加日志(Path.GetFileName(file)+" 文件同步成功，准备进行备份文件SHA1校验");
	                    string sha1new=GetFileSHA1(destDirName + Path.GetFileName(file));
	                    ZhiLian.追加日志(Path.GetFileName(file)+" 备份文件SHA1值为:"+sha1new);
	                    if(sha1old==sha1new)
	                    {
	                    	ZhiLian.追加日志(Path.GetFileName(file)+" 文件校验成功");
	                    }else{
	                    	ZhiLian.追加日志(Path.GetFileName(file)+" 文件校验失败");
	                    	File.Move(destDirName + Path.GetFileName(file), destDirName + Path.GetFileName(file)+"文件校验失败");
	                    }
	                    File.SetAttributes(destDirName + Path.GetFileName(file), FileAttributes.Normal);
	//                    total++;
                    }
                }
                
				//超过标记天数删除修改日期最早的文件
				while(Directory.GetFiles(destDirName).Length>delflag && delflag!=0){
					string[] destfiles = Directory.GetFiles(destDirName);
	                if(destfiles.Length>delflag){
	                	int small=0;
	                	for(int i=1;i<destfiles.Length;i++){
	                		FileInfo fi = new FileInfo(destfiles[small]);
	                		FileInfo fi1 = new FileInfo(destfiles[i]);
	                		
	                		if(fi.LastWriteTime>fi1.LastWriteTime){
	                			small=i;
	                		}
	                	}
	                	ZhiLian.追加日志(Path.GetFileName(destfiles[small])+" 超过设置的备份文件数，自动删除日期最早的文件");
	                	File.Delete(destfiles[small]);
	                	
	                }
				}
                string[] dirs = Directory.GetDirectories(sourceDirName);
                foreach (string dir in dirs)
                {
                    CopyDirectory(dir, destDirName + Path.GetFileName(dir),delflag);
                }
            }
            catch (Exception ex)
            {
                ZhiLian.追加日志(ex.Message);
            }
        }   
        public static string GetFileSHA1(string filePath)
		     {
		       SHA1 sha = new SHA1CryptoServiceProvider();
		       FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
		       byte[] result = sha.ComputeHash(fs);
		       sha.Clear();
		       fs.Dispose();
				StringBuilder returnValue = new StringBuilder();
				
				    //loop for each byte and add it to StringBuilder
				    for (int i = 0; i < result.Length; i++)
				    {
				        returnValue.Append(result[i].ToString("X2"));
				    }
				
				    // return hexadecimal string
				    return returnValue.ToString();
		     }
        
	}
	
}
