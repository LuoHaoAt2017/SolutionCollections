using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace BuildWinForms
{
	partial class Form1
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
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Text = "Form1";
			this.Load += Form1_Load;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.AutoSize = true;
			this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.Text = "URL Opener";

			flowPanel = new FlowLayoutPanel();
			flowPanel.AutoSize = true;
			flowPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.Controls.Add(flowPanel);

			urlLabel = new Label();
			urlLabel.Name = "urlLabel";
			urlLabel.Text = "URL:";
			urlLabel.Width = 50;
			urlLabel.TextAlign = ContentAlignment.MiddleCenter;
			flowPanel.Controls.Add(urlLabel);

			urlTextBox = new TextBox();
			urlTextBox.Name = "urlTextBox";
			urlTextBox.Width = 250;
			flowPanel.Controls.Add(urlTextBox);

			urlButton = new Button();
			urlButton.Name = "urlButton";
			urlButton.Text = "Open URL in Browser";
			urlButton.Click += new EventHandler(urlButton_Click);
			flowPanel.Controls.Add(urlButton);

			groupBox = new GroupBox();
			groupBox.Text = "选择框";
			RadioButton radioButton1 = new RadioButton();
			RadioButton radioButton2 = new RadioButton();
			radioButton1.Text = "YES";
			radioButton1.AutoSize = true;
			radioButton1.Margin = new System.Windows.Forms.Padding(0);
			radioButton1.Location = new Point(20, 20);

			radioButton2.Text = "NOT";
			radioButton2.AutoSize = true;
			radioButton2.Margin = new System.Windows.Forms.Padding(0);
			radioButton2.Location = new Point(100, 20);
			groupBox.Controls.Add(radioButton1);
			groupBox.Controls.Add(radioButton2);
			flowPanel.Controls.Add(groupBox);
		}

		void urlButton_Click(object sender, EventArgs e)
		{
			try
			{
				Uri newUri = new Uri(urlTextBox.Text);
			}
			catch (UriFormatException uriEx)
			{
				MessageBox.Show("Sorry, your URL is malformed. Try again. Error: " + uriEx.Message);
				urlTextBox.ForeColor = Color.Red;
				return;
			}

			// Valid URI. Reset any previous error color, and launch the URL in the 
			// default browser.
			// NOTE: Depending on the user's settings, this method of starting the
			// browser may use an existing window in an existing Web browser process.
			// To get around this, start up a specific browser instance instead using one of
			// the overloads for Process.Start. You can examine the registry to find the
			// current default browser and launch that, or hard-code a specific browser.
			urlTextBox.ForeColor = Color.Black;
			Process.Start(urlTextBox.Text);
		}

		#endregion

		private FlowLayoutPanel flowPanel;
		private Label urlLabel;
		private Button urlButton;
		private TextBox urlTextBox;

		private GroupBox groupBox;
	}
}

