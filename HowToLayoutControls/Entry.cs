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

		public Entry()
		{
			InitializeComponent();
			// InitializeDataGridView();
			// InitializeDateTimePicker();
			// InitializeDomainUpDown();
			InitializeErrorProvider();
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
