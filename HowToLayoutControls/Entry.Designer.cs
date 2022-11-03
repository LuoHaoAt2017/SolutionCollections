using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HowToLayoutControls
{
	partial class Entry
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		private int W = 1200;

		private int H = 600;

		private DataGridView songsDataGridView = new DataGridView();
		private Panel btnPanel = new Panel();
		private Button addNewBtn = new Button();
		private Button deleteBtn = new Button();

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
			this.ClientSize = new System.Drawing.Size(this.W, this.H);
			ProductDataGridView();
		}

		private void ProductListView()
		{
			ListView listView = new ListView();
			listView.View = View.Details;
			listView.Location = new Point(10, 10);
			listView.Dock = DockStyle.Fill; // listView.Size = new Size(this.W, this.H);
			listView.Padding = new Padding(50, 50, 50, 50); // 失效
			listView.Font = new Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			listView.GridLines = true;
			listView.ShowItemToolTips = true;
			listView.Alignment = ListViewAlignment.Left;
			listView.BackColor = Color.Orange;
			listView.ForeColor = Color.White;
			listView.BorderStyle = BorderStyle.FixedSingle;
			listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;


			DataTable dataTable = new DataTable();
			GetProductSource(dataTable);

			foreach (DataColumn col in dataTable.Columns)
			{
				listView.Columns.Add(col.ColumnName, -2, HorizontalAlignment.Left);
			}

			foreach (DataRow row in dataTable.Rows)
			{
				ListViewItem item = new ListViewItem(new String[3]
				{
					(string)row[0],
					(string)row[1],
					(string)row[2],
				});
				item.ToolTipText = "ToolTipText";
				listView.Items.Add(item);
			}

			this.Controls.Add(listView);
		}

		private void ProductDataGridView()
		{
			SetupLayout();
			SetupDataGridView();
			PopulateDataGridView();
		}

		private void SetupLayout()
		{
			addNewBtn.Text = "Add Row";
			addNewBtn.Location = new Point(10, 10);
			addNewBtn.Click += new EventHandler(addNewRowBtnClick);

			deleteBtn.Text = "Delete Row";
			deleteBtn.Location = new Point(100, 10);
			deleteBtn.Click += new EventHandler(deleteRowBtnClick);

			btnPanel.Controls.Add(addNewBtn);
			btnPanel.Controls.Add(deleteBtn);
			btnPanel.Height = 50;
			btnPanel.Dock = DockStyle.Bottom;

			this.Controls.Add(btnPanel);
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

		private void addNewRowBtnClick(object sender, EventArgs e)
		{
			this.songsDataGridView.Rows.Add();
		}

		private void deleteRowBtnClick(object sender, EventArgs e)
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
				catch(Exception)
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

		#endregion
	}
}

