using System;
using System.Drawing;
using System.Windows.Forms;

namespace FrameworkControls
{

	partial class CustomSearch
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.searchBox = new System.Windows.Forms.TextBox();
			this.searchBtn = new System.Windows.Forms.Button();
			this.searchPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.searchPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// searchBox
			// 
			this.searchBox.AutoSize = false;
			this.searchBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.searchBox.Font = new System.Drawing.Font("微软雅黑", 24F);
			this.searchBox.Location = new System.Drawing.Point(0, 0);
			this.searchBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.searchBox.Name = "searchBox";
			this.searchBox.Size = new System.Drawing.Size(400, 50);
			// this.searchBox.Width = 400;
			this.searchBox.TabIndex = 0;
			this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
			// 
			// searchBtn
			// 
			this.searchBtn.Cursor = System.Windows.Forms.Cursors.Hand;
			this.searchBtn.Location = new System.Drawing.Point(400, 0);
			this.searchBtn.Margin = new System.Windows.Forms.Padding(0);
			this.searchBtn.Name = "searchBtn";
			this.searchBtn.Size = new System.Drawing.Size(75, 60);
			this.searchBtn.TabIndex = 1;
			this.searchBtn.Text = "搜索";
			this.searchBtn.UseVisualStyleBackColor = true;
			// 
			// searchPanel
			// 
			this.searchPanel.AutoSize = true;
			this.searchPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.searchPanel.BackColor = System.Drawing.Color.White;
			this.searchPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.searchPanel.Controls.Add(this.searchBox);
			this.searchPanel.Controls.Add(this.searchBtn);
			this.searchPanel.Location = new System.Drawing.Point(0, 0);
			this.searchPanel.Margin = new System.Windows.Forms.Padding(0);
			this.searchPanel.Name = "searchPanel";
			this.searchPanel.Size = new System.Drawing.Size(477, 62);
			this.searchPanel.TabIndex = 2;
			// 
			// CustomSearch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.searchPanel);
			this.Name = "CustomSearch";
			this.Size = new System.Drawing.Size(991, 435);
			this.Load += new System.EventHandler(this.CustomSearch_Load);
			this.searchPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox searchBox;
		private System.Windows.Forms.Button searchBtn;
		private System.Windows.Forms.FlowLayoutPanel searchPanel;
	}

	public class TextBoxWithPlaceholder : TextBox
	{
		public string PlaceHolderStr { get; set; }

		// 在Winform程序中，可以重写WndProc函数，来捕捉所有发生的窗口消息。
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (m.Msg == 0xF || m.Msg == 0x133)
			{
				WmPaint(ref m);
			}
		}

		private void WmPaint(ref Message m)
		{
			Graphics g = Graphics.FromHwnd(base.Handle);
			if (!String.IsNullOrEmpty(this.PlaceHolderStr) && string.IsNullOrEmpty(this.Text))
			{
				g.DrawString(this.PlaceHolderStr, this.Font, new SolidBrush(Color.LightGray), 0, 0);
			}
		}
	}
}
