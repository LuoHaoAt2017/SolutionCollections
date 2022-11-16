namespace ExtendedControls.Components
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
			this.searchBtn = new System.Windows.Forms.Button();
			this.searchBox = new System.Windows.Forms.TextBox();
			this.searchPanel = new System.Windows.Forms.Panel();
			this.searchPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// searchBtn
			// 
			this.searchBtn.Location = new System.Drawing.Point(245, 34);
			this.searchBtn.Name = "searchBtn";
			this.searchBtn.Size = new System.Drawing.Size(75, 23);
			this.searchBtn.TabIndex = 1;
			this.searchBtn.Text = "button1";
			this.searchBtn.UseVisualStyleBackColor = true;
			// 
			// searchBox
			// 
			this.searchBox.Location = new System.Drawing.Point(39, 34);
			this.searchBox.Name = "searchBox";
			this.searchBox.PlaceholderText = "请输入检查号";
			this.searchBox.Size = new System.Drawing.Size(200, 23);
			this.searchBox.TabIndex = 0;
			this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
			// 
			// searchPanel
			// 
			this.searchPanel.AutoSize = true;
			this.searchPanel.Controls.Add(this.searchBox);
			this.searchPanel.Controls.Add(this.searchBtn);
			this.searchPanel.Location = new System.Drawing.Point(353, 53);
			this.searchPanel.Name = "searchPanel";
			this.searchPanel.Size = new System.Drawing.Size(453, 100);
			this.searchPanel.TabIndex = 2;
			// 
			// CustomSearch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.searchPanel);
			this.Name = "CustomSearch";
			this.Size = new System.Drawing.Size(898, 235);
			this.searchPanel.ResumeLayout(false);
			this.searchPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Button searchBtn;
		private TextBox searchBox;
		private Panel searchPanel;
	}
}
