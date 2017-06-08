using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace ZmSync
{	
	static class Program 
	{
		/// <summary>
        /// 应用程序的主入口点。
        /// </summary>
		[STAThread]
		public static void Main()//(string[] args)
		{
			bool ret;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out ret);
            if (ret)
            {
            //Application.SetCompatibleTextRenderingDefault(UseCompatibleTextRendering);
            //MyProject.Application.Run(args);
	            Application.EnableVisualStyles();
	            Application.SetCompatibleTextRenderingDefault(false);
	            Application.Run(new Form1());
	            mutex.ReleaseMutex();
            }else
            {
//                MessageBox.Show(null, "有一个和本程序相同的应用程序已经在运行，请不要同时运行多个本程序。\n\n这个程序即将退出。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //   提示信息，可以删除。   
                Application.Exit();//退出程序   
            }
        }
	}
}
