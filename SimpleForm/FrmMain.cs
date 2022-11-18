using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
	public partial class FrmMain : Form
	{
		// this.timerCode.Tick += new System.EventHandler(this.timerCode_Tick);
		private System.Windows.Forms.Timer timer;

		public FrmMain()
		{
			InitializeComponent();

			this.timer.Interval = 1000;
			this.timer.Tick +=  new System.EventHandler(this.TimerTickHandler);
		}

		public void TimerTickHandler(object o, EventArgs e)
		{
			this.TimeLabel.Text = DateTime.Now.ToString();
		}

		private void StartTimerBtn_Click(object sender, EventArgs e)
		{
			this.timer.Start();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			new Thread(() =>
			{
				while (true)
				{

				}
			}).Start();
		}
	}
}
