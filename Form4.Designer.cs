using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
namespace ZmSync
{
	partial class Form4
	{

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
		{
			try {
				if (disposing && components != null) {
					components.Dispose();
				}
			} finally {
				base.Dispose(disposing);
			}
		}

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components= null;
        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
		{
			this.DataGridView1 = new System.Windows.Forms.DataGridView();
			this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
			this.Button4 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// DataGridView1
			// 
			this.DataGridView1.AllowUserToOrderColumns = true;
			this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataGridView1.Location = new System.Drawing.Point(0, 28);
			this.DataGridView1.Name = "DataGridView1";
			this.DataGridView1.ReadOnly = true;
			this.DataGridView1.RowTemplate.Height = 23;
			this.DataGridView1.Size = new System.Drawing.Size(918, 397);
			this.DataGridView1.TabIndex = 0;
			// 
			// MenuStrip1
			// 
			this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
			this.MenuStrip1.Name = "MenuStrip1";
			this.MenuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
			this.MenuStrip1.Size = new System.Drawing.Size(918, 28);
			this.MenuStrip1.TabIndex = 3;
			this.MenuStrip1.Text = "MenuStrip1";
			// 
			// Button4
			// 
			this.Button4.Location = new System.Drawing.Point(303, 2);
			this.Button4.Name = "Button4";
			this.Button4.Size = new System.Drawing.Size(93, 28);
			this.Button4.TabIndex = 8;
			this.Button4.Text = "导出xlsx";
			this.Button4.UseVisualStyleBackColor = true;
			this.Button4.Click += new System.EventHandler(this.Button4_Click);
			// 
			// Form4
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(918, 425);
			this.Controls.Add(this.Button4);
			this.Controls.Add(this.DataGridView1);
			this.Controls.Add(this.MenuStrip1);
			this.MainMenuStrip = this.MenuStrip1;
			this.Name = "Form4";
			this.Text = "查询结果";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form4_FormClosing);
			this.Load += new System.EventHandler(this.Form4_Load);
			((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
        #endregion
        internal System.Windows.Forms.DataGridView DataGridView1;
		internal System.Windows.Forms.MenuStrip MenuStrip1;

        private System.Windows.Forms.Button Button4;
    }
}
