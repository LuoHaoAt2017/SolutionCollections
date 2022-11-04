using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace HowToLayoutControls
{
	public partial class Entry : Form
	{
		private DataGridView songsDataGridView = new DataGridView();
		private Panel btnPanel = new Panel();
		private Button addNewBtn = new Button();
		private Button deleteBtn = new Button();
		private TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
		private int RowHeight = 40;
		private int ColumnCount = 5;

		public Entry()
		{
			InitializeComponent();
			// InitializeDataGridView();
			// InitializeDateTimePicker();
			// InitializeDomainUpDown();
			// InitializeErrorProvider();
			// InitializeFlowLayoutPanel();
			// InitializeAnchor();
			// InitializeTableLayoutPanel();
			// InitializeGroupBox();
			// InitializeTabControl();
		}

		private void InitializeTabControl()
		{
			TabControl tabControl = new TabControl();
			
			TabPage page1 = new TabPage();
			TabPage page2 = new TabPage();

			page1.Size = new Size(400, 400);
			page1.Text = "Page1";
			page1.TabIndex = 0;
			Button button1 = new Button();
			button1.AutoSize = true;
			button1.Text = "Hello World Page1";
			AddControlIntoPage(page1, button1);

			page2.Size = new Size(400, 400);
			page2.Text = "Page2";
			page2.TabIndex = 1;
			Button button2 = new Button();
			button2.AutoSize = true;
			button2.Text = "Hello World Page2";
			AddControlIntoPage(page2, button2);

			Button button3 = new Button();
			button3.AutoSize = true;
			button3.Text = "Page1";
			button3.Click += new EventHandler((object obj, EventArgs e) =>
			{
				tabControl.SelectedIndex = page1.TabIndex;
			});

			Button button4 = new Button();
			button4.AutoSize = true;
			button4.Text = "Page2";
			button4.Click += new EventHandler((object obj, EventArgs e) =>
			{
				tabControl.SelectedIndex = page2.TabIndex;
			});

			//tabControl.SelectedTab = page1;
			tabControl.Controls.AddRange(new Control[] { page1, page2 });
			FlowLayoutPanel layoutPanel = new FlowLayoutPanel();
			layoutPanel.FlowDirection = FlowDirection.TopDown;
			layoutPanel.AutoSize = true;
			layoutPanel.Controls.AddRange(new Control[] { tabControl, button3, button4 });
			this.Controls.Add(layoutPanel);
		}

		public void AddControlIntoPage(TabPage page, Control control)
		{
			page.Controls.Add(control);
		}

		private void InitializeGroupBox()
		{
			FlowLayoutPanel layout = new FlowLayoutPanel();
			layout.FlowDirection = FlowDirection.TopDown;
			layout.AutoSize = true;
			GroupBox group1 = new GroupBox();
			GroupBox group2 = new GroupBox();
			group1.Text = "Group1";
			group1.AutoSize = true;
			group1.FlatStyle = FlatStyle.Flat;
			group2.Text = "Group2";
			group2.AutoSize = true;
			group2.FlatStyle = FlatStyle.Standard;
			string[] strs = new string[3] { "基督教", "摩尼教", "印度教" };
			for(int i = 0; i < strs.Length; i++)
			{
				RadioButton radio = new RadioButton();
				radio.Text = strs[i];
				radio.Width = 100;
				radio.AutoCheck = true;
				radio.TextAlign = ContentAlignment.MiddleCenter;
				radio.Appearance = Appearance.Button;
				radio.Location = new Point(i * 100, 20);
				radio.CheckedChanged += RadioCheckedChanged;
				group1.Controls.Add(radio);
			}
			for (int i = 0; i < strs.Length; i++)
			{
				RadioButton radio = new RadioButton();
				radio.Text = strs[i];
				radio.Width = 100;
				radio.AutoCheck = true;
				radio.TextAlign = ContentAlignment.MiddleCenter;
				radio.Appearance = Appearance.Normal;
				radio.Location = new Point(i * 100, 20);
				radio.CheckedChanged += RadioCheckedChanged;
				group2.Controls.Add(radio);
			}
			layout.Controls.Add(group1);
			layout.Controls.Add(group2);
			this.Controls.Add(layout);
		}

		private void RadioCheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;

			if (radioButton.Checked == true)
			{
				MessageBox.Show(radioButton.Text);
			}
		}

		private void InitializeAnchor()
		{
			Button button1 = new Button();
			button1.Text = "Anchor Top";
			button1.AutoSize = true;
			button1.Location = new Point(0, 200);
			button1.Anchor = AnchorStyles.Top;

			Button button2 = new Button();
			button2.Text = "Anchor Bottom";
			button2.AutoSize = true;
			button2.Location = new Point(0, 400);
			button2.Anchor = AnchorStyles.Bottom;

			Button button3 = new Button();
			button3.Text = "Anchor Left";
			button3.AutoSize = true;
			button3.Location = new Point(400, 200);
			button3.Anchor = AnchorStyles.Left;

			Button button4 = new Button();
			button4.Text = "Anchor Right";
			button4.AutoSize = true;
			button4.Location = new Point(400, 400);
			button4.Anchor = AnchorStyles.Right;

			this.Controls.Add(button1);
			this.Controls.Add(button2);
			this.Controls.Add(button3);
			this.Controls.Add(button4);
		}

		private void InitializeDock()
		{
			Panel panel = new Panel();
			panel.Width = 400;
			panel.Height = 400;
			panel.BorderStyle = BorderStyle.FixedSingle;
			Button button = new Button();
			button.Text = "button";
			//button.Dock = DockStyle.Left;
			//button.Dock = DockStyle.Right;
			//button.Dock = DockStyle.Bottom;
			//button.Dock = DockStyle.Top;
			//button.Dock = DockStyle.Fill;
			panel.Controls.Add(button);
			this.Controls.Add(panel);
		}

		private void InitializeFlowLayoutPanel()
		{
			FlowLayoutPanel panel1 = new FlowLayoutPanel();
			panel1.Height = 300;
			panel1.Width = 600;
			panel1.Location = new Point(0, 0);
			// 指示应当对 FlowLayoutPanel 控件的内容进行换行还是剪裁。
			panel1.WrapContents = true; // false: 剪裁
			panel1.FlowDirection = FlowDirection.LeftToRight;
			string[] list = 
			{ 
				"AAAAAA", "BBBBBB", "CCCCCC",
				"DDDDDD", "EEEEEE", "FFFFFF",
				"GGGGGG", "HHHHHH", "IIIIII",
				"GGGGGG", "KKKKKK", "LLLLLL",
			};
			for(int i = 0; i < list.Length; i++)
			{
				AddButtonToPanel(list[i], panel1);
			}
			this.Controls.Add(panel1);

			FlowLayoutPanel panel2 = new FlowLayoutPanel();
			panel2.FlowDirection = FlowDirection.TopDown;
			panel2.Height = 400;
			panel2.Width = 600;
			panel2.Location = new Point(900, 0);
			for (int i = 0; i < list.Length; i++)
			{
				AddButtonToPanel(list[i], panel2);
			}
			this.Controls.Add(panel2);

			FlowLayoutPanel panel3 = new FlowLayoutPanel();
			panel3.Height = 300;
			panel3.Width = 300;
			panel3.BorderStyle = BorderStyle.FixedSingle;
			panel3.Location = new Point(0, 400);
			panel3.Padding = new Padding(0, 0, 0, 0);
			panel3.WrapContents = true;
			panel3.FlowDirection = FlowDirection.TopDown;
			Button button1 = new Button();
			Button button2 = new Button();
			Button button3 = new Button();
			Button button4 = new Button();
			Button button5 = new Button();
			Button button6 = new Button();
			button1.Width = 200;
			button1.Text = "button1";
			button2.Text = "button2";
			button3.Text = "button3";
			button4.Text = "button4";
			button5.Text = "button5";
			button6.Text = "button6";
			button2.Dock = DockStyle.Fill; // 第二个按钮采用与第一个按钮相同的宽度。 它不在 FlowLayoutPanel 控件的宽度范围内拉伸。
			button3.Dock = DockStyle.Bottom;
			button4.Anchor = AnchorStyles.Left; // 既锚定左边又锚定右边，就会在左右方向上拉伸元素
			button5.Anchor = AnchorStyles.Right;
			button6.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			panel3.Controls.Add(button1);
			panel3.Controls.Add(button2);
			panel3.Controls.Add(button3);
			panel3.Controls.Add(button4);
			panel3.Controls.Add(button5);
			panel3.Controls.Add(button6);
			this.Controls.Add(panel3);

			FlowLayoutPanel panel4 = new FlowLayoutPanel();
			panel4.Height = 300;
			panel4.Width = 600;
			panel4.BorderStyle = BorderStyle.FixedSingle;
			panel4.Location = new Point(320, 300);
			panel4.Padding = new Padding(0, 0, 0, 0);
			panel4.WrapContents = true;
			panel4.FlowDirection = FlowDirection.LeftToRight;
			Button button7 = new Button();
			Button button8 = new Button();
			Button button9 = new Button();
			Button button10 = new Button();
			Button button11 = new Button();
			Button button12 = new Button();
			button7.Height = 200;
			button7.Text = "button7";
			button8.Text = "button8";
			button9.Text = "button9";
			button10.Text = "button10";
			button11.Text = "button11";
			button12.Text = "button12";
			button8.Dock = DockStyle.Top; // 第二个按钮采用与第一个按钮相同的宽度。 它不在 FlowLayoutPanel 控件的宽度范围内拉伸。
			button9.Dock = DockStyle.Bottom;
			button10.Anchor = AnchorStyles.Top;
			button11.Anchor = AnchorStyles.Bottom;
			button12.Anchor = AnchorStyles.Bottom | AnchorStyles.Top;
			panel4.Controls.Add(button7);
			panel4.Controls.Add(button8);
			panel4.Controls.Add(button9);
			panel4.Controls.Add(button10);
			panel4.Controls.Add(button11);
			panel4.Controls.Add(button12);
			this.Controls.Add(panel4);
		}

		private void InitializeTableLayoutPanel()
		{
			// 通常，不应将 TableLayoutPanel 控件用作整个布局的容器。 可使用 TableLayoutPanel 控件为部分布局提供按比例调整大小的功能。
			// TableLayoutPanel 控件的每个单元格只能包含一个子控件。
			var RowCount = 20;
			tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
			tableLayoutPanel.ColumnCount = ColumnCount;
			tableLayoutPanel.Location = new Point(100, 100);
			tableLayoutPanel.Height = tableLayoutPanel.RowCount * RowHeight; // 默认是 0 行
			tableLayoutPanel.Dock = DockStyle.Top;

			this.Controls.Add(tableLayoutPanel);

			for(int i = 0; i < ColumnCount; i++)
			{
				tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, tableLayoutPanel.Width * 0.2f));
			}
			for(int j = 0; j < RowCount; j++)
			{
				AddTableRow(new string[5] { "apple", "orange", "banana", "casaba", "sugarcane" });
			}
			tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
			tableLayoutPanel.Height = tableLayoutPanel.RowCount * (RowHeight + 2 * 2);
		}

		private void AddTableRow(string[] fruits)
		{
			try
			{
				int rowIndex = tableLayoutPanel.RowCount;
				tableLayoutPanel.RowCount++;
				tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, RowHeight));
				for(int colIndex = 0; colIndex < ColumnCount; colIndex++)
				{
					Label label = new Label();
					label.Text = fruits[colIndex];
					label.Dock = DockStyle.Fill;
					label.Cursor = Cursors.Hand;
					label.Click += new EventHandler((object sender, EventArgs args) => {
						Label target = (Label)sender;
						target.BackColor = Color.Orange;
						// MessageBox.Show(target.Text);
					});
					label.Font = new Font("楷体", 13, FontStyle.Regular);
					label.TextAlign = ContentAlignment.MiddleCenter;
					tableLayoutPanel.Controls.Add(label, colIndex, rowIndex);
				}
			}
			catch(Exception)
			{

			}
		}

		private void InitializeErrorProvider()
		{
			ErrorProvider errorProvider = new ErrorProvider();
			TextBox numberBox = new TextBox();
			numberBox.Location = new Point(0, 100);
			numberBox.Width = 320;
			numberBox.Height = 80;
			numberBox.Validating += new CancelEventHandler((object sender, CancelEventArgs e) =>
			{
				try
				{
					int x = Int32.Parse(numberBox.Text);
					errorProvider.SetError(numberBox, "");
				}
				catch(Exception)
				{
					errorProvider.SetError(numberBox, "Not an integer value");
				}
			});

			this.Controls.Add(numberBox);

			//ErrorProvider errorProvide2 = new ErrorProvider();
			//DataSet dataSet = new DataSet();
			//DataTable table = new DataTable("Products");
			//GetProductSource(table);
			//dataSet.Tables.Add(table);
			//TextBox textBox = new TextBox();
			//textBox.Location = new Point(0, 0);
			//textBox.Width = 320;
			//textBox.Height = 80;
			//textBox.DataBindings.Add("Text", dataSet, "Products.ProductName");
			//errorProvide2.DataSource = dataSet;
			//errorProvide2.DataMember = "Products";
			//errorProvide2.ContainerControl = this;
			//table.Rows[5].SetColumnError("ProductName", "Bad data in this row.");
			//this.BindingContext[table].Position = 5;
			//this.Controls.Add(textBox);
		}

		private void InitializeDomainUpDown()
		{
			DomainUpDown domainUpDown = new DomainUpDown();
			domainUpDown.Sorted = true;
			domainUpDown.Name = "字母表";
			domainUpDown.Width = 320;
			domainUpDown.Height = 80;
			domainUpDown.ReadOnly = true;
			domainUpDown.Wrap = true;
			domainUpDown.Items.Add("故人西辞黄鹤楼");
			domainUpDown.Items.Add("烟花三月下扬州");
			domainUpDown.Items.Add("孤帆远影碧空尽");
			domainUpDown.Items.Add("未见长江天际流");
			this.Controls.Add(domainUpDown);
		}

		private void InitializeDateTimePicker()
		{
			DateTimePicker dateTimePicker = new DateTimePicker();
			dateTimePicker.Format = DateTimePickerFormat.Custom;
			dateTimePicker.CustomFormat = "'Today is:'yyyy-MM-dd"; // 向带格式的值添加文本
			// dateTimePicker.Format = DateTimePickerFormat.Time;
			// 设置控件的日期和时间值
			dateTimePicker.Value = new DateTime(2001, 10, 20);

			// MessageBox.Show("The selected value is " + dateTimePicker.Text);
			MessageBox.Show("The day of the week is " + dateTimePicker.Value.DayOfWeek.ToString());

			dateTimePicker.ShowUpDown = false;
			dateTimePicker.Width = 400;
			this.Controls.Add(dateTimePicker);
		}

		private void InitializeDataGridView()
		{
			addNewBtn.Text = "Add New";
			deleteBtn.Text = "Delete";
			addNewBtn.Click += AddNewRowBtnClick;
			deleteBtn.Click += DeleteRowBtnClick;
			this.btnPanel.Controls.Add(addNewBtn);
			this.btnPanel.Controls.Add(deleteBtn);
			this.btnPanel.Dock = DockStyle.Bottom;
			this.btnPanel.AutoSize = true;
			this.btnPanel.Height = 50;
			this.Controls.Add(btnPanel);

			DataTable dataTable = new DataTable();
			GetProductSource(dataTable);
			SetupDataGridView();
			PopulateDataGridView();
		}

		private void SetupDataGridView()
		{

			songsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
			songsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			songsDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font(songsDataGridView.Font, FontStyle.Bold);

			songsDataGridView.Name = "SongsDataGridView";
			songsDataGridView.Location = new Point(8, 8);
			songsDataGridView.Size = new Size(500, 250);
			songsDataGridView.GridColor = Color.Black;
			songsDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
			songsDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

			songsDataGridView.ColumnCount = 5;
			songsDataGridView.Columns[0].Name = "Release Date";
			songsDataGridView.Columns[1].Name = "Track";
			songsDataGridView.Columns[2].Name = "Title";
			songsDataGridView.Columns[3].Name = "Artist";
			songsDataGridView.Columns[4].Name = "Album";
			songsDataGridView.Columns[4].DefaultCellStyle.Font = new Font(songsDataGridView.DefaultCellStyle.Font, FontStyle.Italic);

			songsDataGridView.MultiSelect = false;
			songsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			songsDataGridView.Dock = DockStyle.Fill;
			songsDataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(CellFormatting);
			songsDataGridView.CellStateChanged += new DataGridViewCellStateChangedEventHandler(CellStateChange);
			songsDataGridView.CellValueChanged += new DataGridViewCellEventHandler(CellValueChange);
			this.Controls.Add(songsDataGridView);
		}

		private void PopulateDataGridView()
		{
			string[] row0 = { "11/22/1968", "29", "Revolution 9",
			"Beatles", "The Beatles [White Album]" };
			string[] row1 = { "1960", "6", "Fools Rush In",
			"Frank Sinatra", "Nice 'N' Easy" };
			string[] row2 = { "11/11/1971", "1", "One of These Days",
			"Pink Floyd", "Meddle" };
			string[] row3 = { "1988", "7", "Where Is My Mind?",
			"Pixies", "Surfer Rosa" };
			string[] row4 = { "5/1981", "9", "Can't Find My Mind",
			"Cramps", "Psychedelic Jungle" };
			string[] row5 = { "6/10/2003", "13",
			"Scatterbrain. (As Dead As Leaves.)",
			"Radiohead", "Hail to the Thief" };
			string[] row6 = { "6/30/1992", "3", "Dress", "P J Harvey", "Dry" };

			songsDataGridView.Rows.Add(row0);
			songsDataGridView.Rows.Add(row1);
			songsDataGridView.Rows.Add(row2);
			songsDataGridView.Rows.Add(row3);
			songsDataGridView.Rows.Add(row4);
			songsDataGridView.Rows.Add(row5);
			songsDataGridView.Rows.Add(row6);

			songsDataGridView.Columns[0].DisplayIndex = 3;
			songsDataGridView.Columns[1].DisplayIndex = 4;
			songsDataGridView.Columns[2].DisplayIndex = 0;
			songsDataGridView.Columns[3].DisplayIndex = 1;
			songsDataGridView.Columns[4].DisplayIndex = 2;
		}

		private void GetProductSource(DataTable table)
		{
			string[] columns = new string[3] { "ProductId", "ProductName", "ProductPrice" };

			for (int i = 0; i < columns.Length; i++)
			{
				DataColumn col = new DataColumn();
				col.AllowDBNull = false;
				col.ColumnName = columns[i];
				col.DataType = System.Type.GetType("System.String");
				table.Columns.Add(col);
			}
			for (int i = 0; i < 30; i++)
			{
				Random rand = new Random();
				DataRow row = table.NewRow();
				row[columns[0]] = rand.NextDouble().ToString();
				row[columns[1]] = rand.NextDouble().ToString();
				row[columns[2]] = rand.NextDouble().ToString();
				table.Rows.Add(row);
			}
		}

		private void AddNewRowBtnClick(object sender, EventArgs e)
		{
			this.songsDataGridView.Rows.Add();
		}

		private void DeleteRowBtnClick(object sender, EventArgs e)
		{
			if (songsDataGridView.SelectedRows.Count > 0 && songsDataGridView.SelectedRows[0].Index != songsDataGridView.Rows.Count - 1)
			{
				songsDataGridView.Rows.RemoveAt(songsDataGridView.SelectedRows[0].Index);
			}
		}

		private void AddButtonToPanel(string label, Panel panel)
		{
			Button button = new Button();
			button.Text = label;
			button.Width = 120;
			button.Height = 60;
			panel.Controls.Add(button);
		}

		// 格式化列表单元格
		private void CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e == null || e.Value == null)
			{
				return;
			}
			if (songsDataGridView.Columns[e.ColumnIndex].Name == "Release Date")
			{
				try
				{
					e.Value = DateTime.Parse(e.Value.ToString()).ToLongDateString();
					e.FormattingApplied = true;

					// 向单元格添加工具提示
					DataGridViewCell cell = songsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
					cell.ToolTipText = "Show me the money";
				}
				catch (Exception)
				{
					Console.WriteLine("{0} is not a valid date.", e.Value.ToString());
				}
			}
		}

		// 检测 DataGridView 单元格的状态更改
		private void CellStateChange(object sender, DataGridViewCellStateChangedEventArgs e)
		{
			// 单元格状态更改(例如单元格失去或获得焦点)时发生。
			DataGridViewElementStates state = e.StateChanged;
			string msg = String.Format("Row {0}, Column {1}, {2}", e.Cell.RowIndex, e.Cell.ColumnIndex, e.StateChanged);
			MessageBox.Show(msg, "Cell State Changed");
		}

		// 检测 DataGridView 单元格的值更改
		private void CellValueChange(object sender, DataGridViewCellEventArgs e)
		{
			string msg = String.Format("Cell at row {0}, column {1} value changed", e.RowIndex, e.ColumnIndex);
			MessageBox.Show(msg, "Cell Value Changed");
		}
	}
}
