namespace ControlLibrary
{
	partial class AlarmControl
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
			this.btnAlarmOff = new System.Windows.Forms.Button();
			this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.btnAlarmOn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblDisplay
			// 
			this.lblDisplay.Location = new System.Drawing.Point(3, 0);
			this.lblDisplay.Size = new System.Drawing.Size(89, 19);
			this.lblDisplay.Text = "17:16:20";
			// 
			// lblAlarm
			// 
			this.lblAlarm.AutoSize = true;
			this.lblAlarm.Location = new System.Drawing.Point(349, 102);
			this.lblAlarm.Size = new System.Drawing.Size(35, 12);
			this.lblAlarm.TabIndex = 1;
			this.lblAlarm.Text = "Alarm";
			// 
			// btnAlarmOff
			// 
			this.btnAlarmOff.Location = new System.Drawing.Point(337, 314);
			this.btnAlarmOff.Name = "btnAlarmOff";
			this.btnAlarmOff.Size = new System.Drawing.Size(75, 23);
			this.btnAlarmOff.TabIndex = 1;
			this.btnAlarmOff.Text = "禁用警报";
			this.btnAlarmOff.UseVisualStyleBackColor = true;
			this.btnAlarmOff.Click += new System.EventHandler(this.btnAlarmOff_Click);
			// 
			// dateTimePicker
			// 
			this.dateTimePicker.Location = new System.Drawing.Point(499, 182);
			this.dateTimePicker.Name = "dateTimePicker";
			this.dateTimePicker.Size = new System.Drawing.Size(200, 21);
			this.dateTimePicker.TabIndex = 2;
			this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
			// 
			// btnAlarmOn
			// 
			this.btnAlarmOn.Location = new System.Drawing.Point(499, 313);
			this.btnAlarmOn.Name = "btnAlarmOn";
			this.btnAlarmOn.Size = new System.Drawing.Size(75, 23);
			this.btnAlarmOn.TabIndex = 3;
			this.btnAlarmOn.Text = "开启警报";
			this.btnAlarmOn.UseVisualStyleBackColor = true;
			this.btnAlarmOn.Click += new System.EventHandler(this.btnAlarmOn_Click);
			// 
			// AlarmControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.Controls.Add(this.btnAlarmOn);
			this.Controls.Add(this.dateTimePicker);
			this.Controls.Add(this.btnAlarmOff);
			this.Name = "AlarmControl";
			this.Controls.SetChildIndex(this.lblDisplay, 0);
			this.Controls.SetChildIndex(this.lblAlarm, 0);
			this.Controls.SetChildIndex(this.btnAlarmOff, 0);
			this.Controls.SetChildIndex(this.dateTimePicker, 0);
			this.Controls.SetChildIndex(this.btnAlarmOn, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnAlarmOff;
		private System.Windows.Forms.DateTimePicker dateTimePicker;
		private System.Windows.Forms.Button btnAlarmOn;
	}
}
