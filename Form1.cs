using Microsoft.CSharp;
using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
//using System.Xml.Linq;
//using System.Data.OleDb;
//using System.Data.SqlClient;
using System.IO;
//using System.IO.Compression;
//using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Xml;
using System.Security.Cryptography;

namespace ZmSync
{
	public partial class Form1 : System.Windows.Forms.Form
	{
		private delegate void LogChangeEventHandler(string sMsg);
		private System.Threading.Timer objTimer;
//		private static string Dfile=ZhiLian.DFile;
//		private string rizhi;
		private string path1;

		int index = 0;
		
		public Form1()
		{
			InitializeComponent();
		}
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (objTimer!=null){
				objTimer.Dispose();
			}
			ZhiLian.追加日志("自动任务程序已关闭");
		}

		public void StartTimer()
		{
			TimerCallback tcb = new TimerCallback(this.TimerMethod);
			this.objTimer = new System.Threading.Timer(tcb, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(10));
		}
		public void TimerMethod(object state)
		{
			string TmpPath = Directory.GetCurrentDirectory();
			string ddiStartTime = ZhiLian.GetXmlDdiStartTime(TmpPath+ "\\Config.xml");
//			string otherStartTime = ZhiLian.getXmlDdiStartTime(TmpPath+ "\\Other.xml");
//			Convert.ToDateTime(time).AddMinutes(10).ToString();

			//zidong()
			DateTime datetime1 = DateTime.Now;
//			ZhiLian.追加日志(Convert.ToDateTime(ddiStartTime,CultureInfo.InvariantCulture).ToString());
//			ZhiLian.追加日志(Convert.ToDateTime(ddiStartTime,CultureInfo.InvariantCulture).AddMinutes(10).ToString());
//			if (Convert.ToDateTime(datetime1.ToLongTimeString()) > Convert.ToDateTime("22:50:00") 
//				& Convert.ToDateTime(datetime1.ToLongTimeString()) < Convert.ToDateTime("22:59:59")) {
			if (Convert.ToDateTime(datetime1.ToLongTimeString(),CultureInfo.InvariantCulture)
			    > Convert.ToDateTime(ddiStartTime,CultureInfo.InvariantCulture)
			    & Convert.ToDateTime(datetime1.ToLongTimeString(),CultureInfo.InvariantCulture)
			    <= Convert.ToDateTime(ddiStartTime,CultureInfo.InvariantCulture).AddMinutes(10)) {
//				object aa=null;
				AllTiqu();
//				GC.Collect();
//				AllFtp();
//				ZhiLian.XmlSqlcommand();
				ProjectLog1();
			}
//			if (!String.IsNullOrEmpty(rizhi)) {
//				ZhiLian.追加日志(rizhi);
//				rizhi = "";
//			}
		}
		
		public static void 新建导出目录()
		{
			string[] parm = {
				Directory.GetCurrentDirectory() + "\\Log\\",
			};
			foreach(string path in parm){
				Directory.CreateDirectory(path);
			}
//			ZhiLian.XmlFactoryCreatefile();
		}
		
		/// <summary>
		/// 所有提取任务
		/// </summary>
		private static void AllTiqu()//(object state)
		{
//			ZhiLian.XmlDdi();		
			ZhiLian.XmlSync();
			GC.Collect();			
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

		private void Form1_Load(object sender, EventArgs e)
		{
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            menuStrip2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            foreach (ToolStripItem aa in menuStrip1.Items)
			{
				aa.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			}
            toolStripComboBox1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            button1.Visible=false;
			textBox2.Visible=false;
			hideitem();
			this.textBox1.Text = "程序正在运行中，请勿关闭";
//			this.textBox1.DeselectAll();
			this.textBox1.SelectionStart = 0;
            //this.MaximizeBox = false;
            新建导出目录();
			ZhiLian.追加日志("自动执行任务启动");
			//日志串(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " "  & "自动执行任务启动")
			//shoudong = 0
			StartTimer();
//			ZhiLian.XmlDDI();
//			Dfile=ZhiLian.DFile;
//			ZhiLian.Tpyy=XmlHelper.XmlDatabase();
		}
		/// <summary>
		/// 全部Sync任务
		/// </summary>
		public static void AllSync()//(object state)
		{
			ZhiLian.XmlSync();
		}
		private static string NowTime(string format)
		{
			return DateTime.Now.ToString(format,CultureInfo.InvariantCulture);
		}
		private void Hidden()
		{			
			menuStrip1.Enabled = false;
			menuStrip2.Enabled = false;
			this.textBox1.Text = "手动任务开始执行，请稍后。。。";
			this.textBox1.SelectionStart = 0;
			this.ProgressBar1.Visible = true;
			this.ProgressBar1.Maximum = 100;
			this.ProgressBar1.Value = 0;
			this.ProgressBar1.Step = 1;
		}
		private void ShowAll()
		{
			menuStrip1.Enabled = true;
			menuStrip2.Enabled = true;
			this.ProgressBar1.Visible = false;
		}

		/// <summary>
		/// 打开查看日志页面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
        	ToolStripMenuItemSelected();
            hideitem();
            toolStripMenuItem4.Visible = true;
            toolStripMenuItem5.Visible = true;
        }
        /// <summary>
        /// 隐藏menuStrip1所有元素
        /// </summary>
        void hideitem()
        {
        	foreach (ToolStripItem aa in menuStrip1.Items)
			{
				aa.Visible = false;
			}
//            toolStripComboBox1.Visible = false;
        }
        
		/// <summary>
		/// 打开参数配置页面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
        	ToolStripMenuItemSelected();
            hideitem();
            toolStripMenuItem11.Visible = true;
//            toolStripMenuItem12.Visible = true;
            toolStripMenuItem16.Visible = true;
        }

		/// <summary>
		/// 打开版本信息页面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
        	ToolStripMenuItemSelected();
            hideitem();
            toolStripMenuItem14.Visible = true;
//            toolStripMenuItem15.Visible = true;
        }
        public async void TextShow(String tmpPath)
        {
        	string str="";
        	Task t=Task.Run(() => {str= Txthelper.ReadFile(tmpPath);            
            } );
            await t;
            this.textBox1.Text = str;
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
            this.textBox1.ScrollToCaret();
            this.textBox1.ReadOnly = true;
        }
        private void ShowLog(string sMsg)
		{
			if (this.textBox1.InvokeRequired)
			{
				base.Invoke(new LogChangeEventHandler(this.ShowLog), new string[]
				{
					sMsg
				});
			}
			else if (string.IsNullOrEmpty(sMsg))
			{
				this.textBox1.Clear();
			}
			else
			{
				this.textBox1.Text = sMsg;				              		
				this.textBox1.SelectionStart = this.textBox1.Text.Length;
				this.textBox1.SelectionLength = 0;				
				this.textBox1.ScrollToCaret();			                		
				this.textBox1.ReadOnly = true;
			}
		}
        public void TextShow1(String tmpPath)
        {
        	string str="";
//        	Task t=Task.Run(() => {str= Txthelper.ReadFile1(tmpPath);            
//            } );
        	str= Txthelper.ReadFile1(tmpPath);
//            await t;
            ShowLog(str);
//            this.Invoke(new Action(() =>
//						            	{
//						                this.textBox1.Text = str;
//						                this.textBox1.SelectionStart = this.textBox1.Text.Length;
//						                this.textBox1.ScrollToCaret();
//						                this.textBox1.ReadOnly = true;
//						            	}
//				                       ));
        }
        public void ProjectLog()
        {
            string TmpPath = Directory.GetCurrentDirectory() + "\\Log\\Project" + DateTime.Now.ToString("yyyyMM") + ".log";
            TextShow(TmpPath);
        }
        public void ProjectLog1()
        {
            string TmpPath = Directory.GetCurrentDirectory() + "\\Log\\Project" + DateTime.Now.ToString("yyyyMM") + ".log";
            TextShow1(TmpPath);
//            string str = Txthelper.ReadFile1(TmpPath);
//             return str;
        }

        /// <summary>
        /// 查看本月日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void ToolStripMenuItem4Click(object sender, EventArgs e)
		{
			ToolStripMenuItemSelected();
			ProjectLog();
		}

		/// <summary>
		/// 查看全部日志
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ToolStripMenuItem5Click(object sender, EventArgs e)
		{
			Process.Start(Directory.GetCurrentDirectory() + "\\Log\\");
		}
		/// <summary>
		/// 读取版本信息文件
		/// </summary>
		void UpdateLog()
        {
//        	string str = null;
//			string TmpPath = null;
//			TmpPath = Directory.GetCurrentDirectory() + "\\Update.txt";
//			str = File.ReadAllText(TmpPath);
			string xitong;
			if (Environment.Is64BitOperatingSystem==true)
			{	xitong="x64";
			}
			else
			{	xitong="x86";
			}
			string jincheng;
			if (Environment.Is64BitProcess==true)
			{	jincheng="64位";
			}
			else
			{	jincheng="32位";
			}
			//获取系统信息
			System.OperatingSystem osInfo = System.Environment.OSVersion;
//			System.Environment.OSVersion.VersionString;
			this.textBox1.Text = "当前操作系统版本：" + osInfo +" "+xitong+" 本程序运行在"+jincheng
				+ Environment.NewLine + "当前运行的.net版本：" + Environment.Version + Environment.NewLine 
				+ "软件版本：v1.0.0.0版 2017-05-17"+ Environment.NewLine
				+ "操作系统版本支持win7及以上、server2008r2及以上"+ Environment.NewLine
				+ "更新日志："+ Environment.NewLine
				+ "v1.0.0.0 设定自动同步任务，并且强制校验SHA1值，用以确认同步成功，可设定保留备份天数，超过备份天数自动删除"+ Environment.NewLine;
			this.textBox1.DeselectAll();
//			取消全选
			this.textBox1.SelectionStart = 0;
            this.Show();
            this.textBox1.ReadOnly=true;
        }

		/// <summary>
		/// 查看版本信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ToolStripMenuItem14Click(object sender, EventArgs e)
		{
			ToolStripMenuItemSelected();
			UpdateLog();
		}
		/// <summary>
		/// 读取并显示Config.xml文件
		/// </summary>
		public void ReadConfig()
        {
        	string str = null;
			path1= Directory.GetCurrentDirectory() + "\\Config.xml";
//			path1= Directory.GetCurrentDirectory() + tmp;
			str = File.ReadAllText(path1);
			this.textBox1.Text = str;
			this.textBox1.ReadOnly=false;
			this.textBox1.DeselectAll();
			this.textBox1.SelectionStart = 0;
//            this.Show();
        }


		/// <summary>
		/// 配置Config界面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ToolStripMenuItem11Click(object sender, EventArgs e)
		{
			ToolStripMenuItemSelected();
//			ReadConfig("\\Config.xml");
			ReadConfig();
//			toolStripMenuItem17.Visible = false;
			toolStripMenuItem13.Visible = true;
			button1.Visible=true;
			textBox2.Visible=true;
		}

		/// <summary>
		/// 保存Config
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ToolStripMenuItem13Click(object sender, EventArgs e)
		{
			Txthelper.Write(path1,textBox1.Text);
			MessageBox.Show("保存成功");
//			ZhiLian.XmlFactoryCreatefile();
		}

		/// <summary>
		/// 查看配置信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ToolStripMenuItem16Click(object sender, EventArgs e)
		{
			ZhiLian.GetXmlInfo();
		}
		
		/// <summary>
		/// 将所有Config中的Job添加至toolStripComboBox1中
		/// </summary>
		private void XmlFactorynameadd()
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
	            //使用xpath表达式选择文档中所有的Job子节点
	            XmlNodeList factoryNodeList = xmlDoc.SelectNodes(@"/Config/Job");
	            if (factoryNodeList != null)
	            {
	                foreach (XmlNode factoryNode in factoryNodeList)
	                {
	                    //通过Attributes获得属性名字为name的属性
	                    string name = factoryNode.Attributes["name"].Value;
	                    toolStripComboBox1.Items.Add(name);
	                } 
	            }
	            reader.Close();
			}catch (Exception ex){
				ZhiLian.追加日志(ex.Message);
			}
		}

		/// <summary>
		/// 打开手动提取页面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ToolStripMenuItem6Click(object sender, EventArgs e)
		{
			ToolStripMenuItemSelected();
			hideitem();
			toolStripComboBox1.Items.Clear();
			XmlFactorynameadd();
			toolStripMenuItem1.Visible = true;
            toolStripMenuItem2.Visible = true;
            toolStripMenuItem3.Visible = true;
            toolStripComboBox1.Visible = true;
            toolStripComboBox1.MaxDropDownItems=12;
		}
		void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBox1.Focus();
		}

		/// <summary>
		/// 提取单个任务
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			ZhiLian.追加日志("同步任务开始");
			Hidden();
			string text=toolStripComboBox1.Text;
			if (string.IsNullOrEmpty(text) == false){
					await Task.Run(() => {ZhiLian.XmlSync(text);
				           	});
			} else {
				MessageBox.Show("请选择需要提取的任务，再点击提取单个任务");
				ZhiLian.追加日志("未选择需要提取的任务家");
//				ZhiLian.追加日志("手动提取任务完毕");
//				return;
			}
			ZhiLian.追加日志("同步任务执行完毕");
			ShowAll();
			toolStripComboBox1.SelectedItem = null;
            GC.Collect();
//            ProjectLog();
            ProjectLog1();
            

		}

		/// <summary>
		/// 校验文件SHA1值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
//			ZhiLian.追加日志("手动提取任务开始");
			Hidden();		
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			// '设置文件类型
//			openFileDialog1.Filter = "xlsx files(*.xlsx)|*.xlsx";
			// '点了保存按钮进入
			if ((openFileDialog1.ShowDialog() == DialogResult.OK)) {
				string localFilePath = null;
				localFilePath = openFileDialog1.FileName.ToString();
				ZhiLian.追加日志("");
				string sha1=null;
				Task t=Task.Run(() => {sha1=GetFileSHA1(localFilePath);
				                } );
				await t;
//				t.Wait();
////				Task.WaitAll();
				t.Dispose();
				ZhiLian.追加日志("此文件SHA1值为："+sha1);
			}
			ZhiLian.追加日志("");			
			GC.Collect();	

			ShowAll();
			//            ProjectLog();
            ProjectLog1();
		}
		
		/// <summary>
		/// 同步全部任务
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			ZhiLian.追加日志("同步任务开始");
			Hidden();
			Task t=Task.Run(() => {ZhiLian.XmlSync();
			                } );
			await t;
			t.Dispose();
			ZhiLian.追加日志("同步任务执行完毕");
			//TextBox2.Visible = False
			ShowAll();
			//            ProjectLog();
            ProjectLog1();
		}
		public void ToolStripMenuItemSelected()
		{
			foreach (ToolStripItem aa in menuStrip2.Items)
			{
				ClearColor();
				aa.BackColor = System.Drawing.Color.LightSkyBlue;
			}
			
			foreach (ToolStripItem aa in menuStrip2.Items)
			{
				if (aa.Selected == true){
					ClearColor();
					aa.BackColor = System.Drawing.Color.LightSkyBlue;
					this.textBox1.Text = "程序正在运行中，请勿关闭";
					this.textBox1.DeselectAll();
					this.textBox1.SelectionStart = 0;
				}
			}
			foreach (ToolStripItem aa in menuStrip1.Items)
			{
				if (aa.Selected == true & aa.Name!="toolStripMenuItem5" & aa.Name!="toolStripMenuItem13" & aa.Name!="toolStripMenuItem15" & aa.Name!="toolStripMenuItem16" & aa.Name!="toolStripMenuItem17"){
					ClearColor();
					aa.BackColor = System.Drawing.Color.LightSkyBlue;
				}
			}
		}
		void ClearColor()
		{
			foreach (ToolStripItem aa in menuStrip1.Items)
			{
				aa.BackColor=System.Drawing.SystemColors.ControlLightLight;
			}
			foreach (ToolStripItem aa in menuStrip2.Items)
			{
				aa.BackColor=System.Drawing.SystemColors.ControlLightLight;
			}
			button1.Visible=false;
			textBox2.Visible=false;
		}
		
		/// <summary>
		/// 点击查找按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Button1Click(object sender, EventArgs e)
		{
			if(string.IsNullOrEmpty(textBox2.Text))
			{
				MessageBox.Show("未输入查找内容", "提示", MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);
			}else{
				if (index>=textBox1.Text.Length-1){
					index=0;
				}
				index = textBox1.Text.IndexOf(textBox2.Text, index);
	            if (index < 0)
	            {
	                index = 0;
	                textBox1.SelectionStart = 0;
	                textBox1.SelectionLength = 0;
	                MessageBox.Show("已到结尾", "提示", MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);
	                return;
	            }
	            textBox1.SelectionStart = index;
	            textBox1.ScrollToCaret();
	            textBox1.SelectionLength = textBox2.Text.Length;
	//            index = index + textBox2.Text.Length;
	            index = index + 1;
	            textBox1.Focus();
			}
		}
		
		/// <summary>
		/// 点击关闭按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ToolStripMenuItem10Click(object sender, EventArgs e)
		{
	
		}
    }
}
