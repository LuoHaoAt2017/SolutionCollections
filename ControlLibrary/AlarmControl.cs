using System;
using System.Drawing;

namespace ControlLibrary
{
	public partial class AlarmControl : ControlLibrary.ClockControl
	{
		public AlarmControl()
		{
			InitializeComponent();
		}

		private bool blnColorTicker;

		private DateTime dteAlarmTime;

		private bool blnAlarmSet;

		public DateTime AlarmTime
		{
			get
			{
				return dteAlarmTime;
			}
			set
			{
				dteAlarmTime = value;
			}
		}

		public bool AlarmSet
		{
			get
			{
				return blnAlarmSet;
			}
			set
			{
				blnAlarmSet = value;
			}
		}

		protected override void timer1_Tick(object sender, System.EventArgs e)
		{
			base.timer1_Tick(sender, e);
			if(AlarmSet == false) return;
			if(AlarmTime.Date == DateTime.Now.Date)
			{
				lblAlarm.Visible = true;
				if (blnColorTicker == false)
				{
					lblAlarm.BackColor = Color.Red;
					blnColorTicker = true;
				} else
				{
					lblAlarm.BackColor = Color.Blue;
					blnColorTicker = false;
				}
			}
			else
			{
				lblAlarm.Visible = false;
			}
		}

		private void btnAlarmOff_Click(object sender, EventArgs e)
		{
			AlarmSet = false;
			lblAlarm.Visible = false;
		}

		private void btnAlarmOn_Click(object sender, EventArgs e)
		{
			AlarmSet = true;
			lblAlarm.Visible = true;
		}

		private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			this.AlarmTime = this.dateTimePicker.Value;
			lblAlarm.Text = "Alarm Time is " + AlarmTime.ToShortTimeString();
		}

	}
}
