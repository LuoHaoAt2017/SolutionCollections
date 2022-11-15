using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// UserControl 测试容器

// 复合控件 = 控件 + 组件 + 代码

namespace ControlLibrary
{
    public partial class ClockControl: UserControl
    {
        private Color colFColor;

        private Color colBColor;

        public Color ClockForeColor
        {
			get
			{
				return colFColor;
			}
			set
			{
				colFColor = value;
				lblDisplay.ForeColor = colFColor;
			}
		}

        public Color ClockBackColor
        {
            get
            {
                return colBColor;
            }
            set 
            { 
                colBColor = value;
                lblDisplay.BackColor = colBColor;
            }
        }

        public ClockControl()
        {
            InitializeComponent();
        }

        protected virtual void timer1_Tick(object sender, EventArgs e)
        {
            this.lblDisplay.Text = DateTime.Now.ToLongTimeString();
        }

        private void ClockControl_Load(object sender, EventArgs e)
        {

        }
    }
}
