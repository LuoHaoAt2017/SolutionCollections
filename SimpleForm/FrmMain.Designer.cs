namespace WindowsFormsApp
{
	partial class FrmMain
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

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.StartTimerBtn = new System.Windows.Forms.Button();
			this.TimeLabel = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// StartTimerBtn
			// 
			this.StartTimerBtn.Location = new System.Drawing.Point(520, 313);
			this.StartTimerBtn.Name = "StartTimerBtn";
			this.StartTimerBtn.Size = new System.Drawing.Size(150, 60);
			this.StartTimerBtn.TabIndex = 0;
			this.StartTimerBtn.Text = "启动定时器";
			this.StartTimerBtn.UseVisualStyleBackColor = true;
			this.StartTimerBtn.Click += new System.EventHandler(this.StartTimerBtn_Click);
			// 
			// TimeLabel
			// 
			this.TimeLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.TimeLabel.Location = new System.Drawing.Point(518, 186);
			this.TimeLabel.Name = "TimeLabel";
			this.TimeLabel.Size = new System.Drawing.Size(160, 65);
			this.TimeLabel.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(129, 313);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(139, 60);
			this.button1.TabIndex = 2;
			this.button1.Text = "压缩视频";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.TimeLabel);
			this.Controls.Add(this.StartTimerBtn);
			this.Name = "FrmMain";
			this.Text = "SimpleForm";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button StartTimerBtn;
		private System.Windows.Forms.Label TimeLabel;
		private System.Windows.Forms.Button button1;
	}
}

