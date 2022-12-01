namespace DataViewDemo
{
	partial class DataViewDemo
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panel6 = new System.Windows.Forms.Panel();
			this.panel7 = new System.Windows.Forms.Panel();
			this.panel8 = new System.Windows.Forms.Panel();
			this.flowLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.BackColor = System.Drawing.Color.Orange;
			this.flowLayoutPanel1.Controls.Add(this.panel1);
			this.flowLayoutPanel1.Controls.Add(this.panel2);
			this.flowLayoutPanel1.Controls.Add(this.panel3);
			this.flowLayoutPanel1.Controls.Add(this.panel4);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(600, 404);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Green;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(400, 100);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.panel2.BackColor = System.Drawing.Color.Blue;
			this.panel2.Location = new System.Drawing.Point(0, 100);
			this.panel2.Margin = new System.Windows.Forms.Padding(0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(100, 100);
			this.panel2.TabIndex = 1;
			// 
			// panel3
			// 
			this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.panel3.BackColor = System.Drawing.Color.Moccasin;
			this.panel3.Location = new System.Drawing.Point(300, 200);
			this.panel3.Margin = new System.Windows.Forms.Padding(0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(100, 100);
			this.panel3.TabIndex = 2;
			// 
			// panel4
			// 
			this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.panel4.BackColor = System.Drawing.Color.MediumSpringGreen;
			this.panel4.Location = new System.Drawing.Point(0, 300);
			this.panel4.Margin = new System.Windows.Forms.Padding(0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(400, 100);
			this.panel4.TabIndex = 3;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.Info;
			this.flowLayoutPanel2.Controls.Add(this.panel5);
			this.flowLayoutPanel2.Controls.Add(this.panel6);
			this.flowLayoutPanel2.Controls.Add(this.panel7);
			this.flowLayoutPanel2.Controls.Add(this.panel8);
			this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 451);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(600, 150);
			this.flowLayoutPanel2.TabIndex = 1;
			this.flowLayoutPanel2.FlowDirection = FlowDirection.LeftToRight;
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(100, 100);
			this.panel5.TabIndex = 0;
			this.panel5.Margin = new Padding(0, 40, 0, 0);
			// 
			// panel6
			// 
			this.panel6.BackColor = System.Drawing.SystemColors.Menu;
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(100, 50);
			this.panel6.TabIndex = 1;
			this.panel6.Anchor = AnchorStyles.Top;
			// 
			// panel7
			// 
			this.panel7.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(100, 50);
			this.panel7.TabIndex = 2;
			this.panel7.Anchor = AnchorStyles.Bottom;
			// 
			// panel8
			// 
			this.panel8.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(100, 50);
			this.panel8.TabIndex = 3;
			this.panel8.Anchor = AnchorStyles.Bottom | AnchorStyles.Top;
			// 
			// DataViewDemo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(600, 600);
			this.Controls.Add(this.flowLayoutPanel2);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Name = "DataViewDemo";
			this.Text = "DataViewDemo";
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private FlowLayoutPanel flowLayoutPanel1;
		private Panel panel1;
		private Panel panel2;
		private Panel panel3;
		private Panel panel4;
		private FlowLayoutPanel flowLayoutPanel2;
		private Panel panel5;
		private Panel panel6;
		private Panel panel7;
		private Panel panel8;
	}
}