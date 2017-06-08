using System;
//using System.Collections;
//using System.Collections.Generic;
using System.Data;
using System.Drawing;
//using System.Diagnostics;
using System.Windows.Forms;
//using System.Linq;
//using System.Xml.Linq;
//using System.Data.OleDb;
using System.IO;
//using System.Text;

//using OfficeOpenXml;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Xml;
using System.Text;
using System.Globalization;
namespace ZmSync
{
	public partial class Form4 : System.Windows.Forms.Form
    {

		private DataSet ds;//= new DataSet();
		public DataSet DS
		{
			get{return this.ds;}
			set{this.ds = value;}
		}
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)

        {
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
//			Button1.Visible = false;
			//Button2.Visible = False
			DataGridView1.Refresh();
			DataGridView1.AllowUserToAddRows = false;
			//可以不显示最后一行空
			DataGridView1.ReadOnly = true;
			//不允许用户更改数据
//			DataGridView1.ReadOnly = false;
			DataGridView1.RowsDefaultCellStyle.BackColor = Color.White;//.AntiqueWhite;
			DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
			

			DataGridView1.DataSource = ds.Tables[0];
//			DataGridView1.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.ColumnHeader;
//			DataGridView1.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.AllCells;
//			for (int i=0;i<DataGridView1.Columns.Count;i++)
//			{
//				DataGridView1.Columns[i].Width = 92;
//			}
			
//			DataGridView1.DataSource = ds;//.Tables[1];
//			DataGridView1.DataBindings();
			Type dgvType = DataGridView1.GetType();
			System.Reflection.PropertyInfo pi = null;
			pi = dgvType.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			//pi.SetValue(dgvType, setting, Nothing)
			pi.SetValue(DataGridView1, true, null);
			this.WindowState = FormWindowState.Maximized;
			
		}
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
    	{
        	
			DataGridView1.DataSource = null;
			DataGridView1.Dispose();
			ds.Tables[0].Clear();
        	ds.Clear();
			ds.Dispose();
			GC.Collect();
		}

		private void Button4_Click(object sender, EventArgs e)
		{
//			ExportToXml(ds,@"D:\LX\test.xml");
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
			// '设置文件类型
			saveFileDialog1.Filter = "xlsx files(*.xlsx)|*.xlsx";
			// '点了保存按钮进入
			if ((saveFileDialog1.ShowDialog() == DialogResult.OK)) {
				string localFilePath = null;
				localFilePath = saveFileDialog1.FileName.ToString();
				DumpExcel(localFilePath, ds.Tables[0]);
				MessageBox.Show("数据导出成功!", "提示", MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);
			}
		}
		
		public static void DumpExcel(string flieName, DataTable dict)
		{
//			FileInfo newFile = new FileInfo(flieName);
//			if (newFile.Exists) {
//				newFile.Delete();
//				newFile = new FileInfo(flieName);
//			}
//
//			using (ExcelPackage pck = new ExcelPackage(newFile)) {
//				//Create the worksheet
//				ExcelWorksheet ws = pck.Workbook.Worksheets.Add("sheet1");
//				//Convert.ToDateTime(dict.Rows(0)("DateTime").ToString("yyyy-mm-dd"))
//				ws.Cells["A1"].LoadFromDataTable(dict, true);
//				
//				
//				//ws.Cells.Style.Numberformat.Format = "@"
//				for (int i = 0; i <= dict.Columns.Count - 1; i++) {
////					string str2 = dict.Columns[i].DataType.Name.ToLower(CultureInfo.InvariantCulture);
//					string str2 = dict.Columns[i].DataType.Name.ToUpper(CultureInfo.InvariantCulture);
//					//If (((str2.Equals("decimal") OrElse str2.Equals("single")) OrElse (str2.Equals("double") OrElse str2.Equals("sbyte"))) OrElse (((str2.Equals("int16") OrElse str2.Equals("int32")) OrElse (str2.Equals("int64") OrElse str2.Equals("byte"))) OrElse ((str2.Equals("uint16") OrElse str2.Equals("uint32")) OrElse str2.Equals("uint64")))) Then
//					//    ws.Column(i + 1).Style.Numberformat.Format = "decimal"
//					//Else
//					//    ws.Column(i + 1).Style.Numberformat.Format = "varchar"
//					//End If
////					if (str2.Equals("datetime")) {
//					if (str2.Equals("DATETIME")) {
//						ws.Column(i + 1).Style.Numberformat.Format = "yyyy/MM/dd";
//					}
//					ws.Column(i + 1).Width = 15;
//				}
//
//
//				//ws.Cells("A1").Value = dict.Columns(0).DataType.Name.ToLower
//				//ws.Cells("B1").Value = dict.Columns(1).DataType.Name.ToLower
//				//ws.Cells("C1").Value = dict.Columns(2).DataType.Name.ToLower
//				//ws.Cells("D1").Value = dict.Columns(3).DataType.Name.ToLower
//				//ws.Cells("E1").Value = dict.Columns(4).DataType.Name.ToLower
//				//ws = pck.Workbook.Worksheets.Add("采购")
//				//ws.Cells("A1").LoadFromDataTable(dict, True)
//				
//				//设置单元格所有边框
////				ws.Cells[1,1,dict.Rows.Count+1,dict.Columns.Count].Style.Border.Bottom.Style=OfficeOpenXml.Style.ExcelBorderStyle.Thin;
////				ws.Cells[1,1,dict.Rows.Count+1,dict.Columns.Count].Style.Border.Top.Style=OfficeOpenXml.Style.ExcelBorderStyle.Thin;
////				ws.Cells[1,1,dict.Rows.Count+1,dict.Columns.Count].Style.Border.Left.Style=OfficeOpenXml.Style.ExcelBorderStyle.Thin;
////				ws.Cells[1,1,dict.Rows.Count+1,dict.Columns.Count].Style.Border.Right.Style=OfficeOpenXml.Style.ExcelBorderStyle.Thin;
////					(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
//				pck.Save();
//				//pck.Dispose();
//				ws.Dispose();
//				dict.Dispose();
//			}
		}

    }
}
