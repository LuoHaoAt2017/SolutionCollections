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
		private bool shortMode;
		private bool boringRecipe;
		private string thirdColumnHeader = "Main Ingredients";
		private string boringMeatloaf = "ground beef";
		private string boringMeatloafRanking = "*";

		private DataGridView dataGridView;
		private FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
		private DataGridView songsDataGridView = new DataGridView();
		private Panel btnPanel = new Panel();
		private Button addNewBtn = new Button();
		private Button deleteBtn = new Button();
		Button Button1 = new Button();
		Button Button2 = new Button();
		Button Button3 = new Button();
		Button Button4 = new Button();
		Button Button5 = new Button();
		Button Button6 = new Button();
		Button Button7 = new Button();
		Button Button8 = new Button();
		Button Button9 = new Button();
		Button Button10 = new Button();

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
			// ProductDataGridView();
			DataGridViewBandDemo();
		}

		private void DataGridViewBandDemo()
		{

			flowLayoutPanel.Height = 50;
			flowLayoutPanel.Dock = DockStyle.Bottom;
			
			AddButton(Button1, "Reset", new EventHandler(Button1_Click));
			AddButton(Button2, "Change Column 3 Header", new EventHandler(Button2_Click));
			AddButton(Button3, "Change Meatloaf Recipe", new EventHandler(Button3_Click));
			this.Controls.Add(flowLayoutPanel);
			AddAdditionalButtons();
			InitializeDataGridView();
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
			addNewBtn.Click += new EventHandler(AddNewRowBtnClick);

			deleteBtn.Text = "Delete Row";
			deleteBtn.Location = new Point(100, 10);
			deleteBtn.Click += new EventHandler(DeleteRowBtnClick);

			this.Controls.Add(btnPanel);
		}

		void AddButton(Button button, string buttonLabel, EventHandler handler)
		{
			flowLayoutPanel.Controls.Add(button);
			button.TabIndex = btnPanel.Controls.Count;
			button.Text = buttonLabel;
			button.AutoSize = true;
			button.Click += handler;
		}

		private void AddAdditionalButtons()
		{
			AddButton(Button4, "Freeze First Row", new EventHandler(Button4_Click));
			AddButton(Button5, "Freeze Second Column", new EventHandler(Button5_Click));
			AddButton(Button6, "第四行隐藏", new EventHandler(Button6_Click));
			AddButton(Button7, "Disable First Column Resizing", new EventHandler(Button7_Click));
			AddButton(Button8, "单元只读", new EventHandler(Button8_Click));
			AddButton(Button9, "Style Using Tag", new EventHandler(Button9_Click));
		}


		private static void Toggle(ref bool toggleThis)
		{
			toggleThis = !toggleThis;
		}

		// 重置的逻辑：删除，销毁，重建，添加。
		private void Button1_Click(object sender, System.EventArgs e)
		{
			this.Controls.Remove(dataGridView);
			dataGridView.Dispose(); // 释放资源
			InitializeDataGridView();
		}

		// 改变表格头
		private void Button2_Click(object sender, System.EventArgs e)
		{
			Toggle(ref shortMode);
			if (shortMode)
			{ dataGridView.Columns[2].HeaderText = "S"; }
			else
			{ dataGridView.Columns[2].HeaderText = thirdColumnHeader; }
		}

		// Change the meatloaf recipe.
		private void Button3_Click(object sender, System.EventArgs e)
		{
			Toggle(ref boringRecipe);
			if (boringRecipe)
			{
				SetMeatloaf(boringMeatloaf, boringMeatloafRanking);
			}
			else
			{
				string greatMeatloafRecipe =
					"1 lb. lean ground beef, " +
					"1/2 cup bread crumbs, 1/4 cup ketchup," +
					"1/3 tsp onion powder, " +
					"1 clove of garlic, 1/2 pack onion soup mix " +
					" dash of your favorite BBQ Sauce";
				SetMeatloaf(greatMeatloafRecipe, "***");
			}
		}

		// 通过行和列来改变单元格的值
		private void SetMeatloaf(string recipe, string rating)
		{
			dataGridView.Rows[0].Cells[2].Value = recipe;
			dataGridView.Rows[0].Cells[3].Value = rating;
		}

		private void InitializeDataGridView()
		{
			dataGridView = new DataGridView();
			this.Controls.Add(dataGridView);
			dataGridView.Size = new Size(300, 200);
			// dataGridView.Dock = DockStyle.Fill;
			// Create an unbound DataGridView by declaring a
			// column count.
			dataGridView.ColumnCount = 4;
			AdjustDataGridViewSizing();

			// Set the column header style.
			DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
			columnHeaderStyle.BackColor = Color.Aqua;
			columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
			dataGridView.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

			// Set the column header names.
			dataGridView.Columns[0].Name = "Recipe";
			dataGridView.Columns[1].Name = "Category";
			dataGridView.Columns[2].Name = thirdColumnHeader;
			dataGridView.Columns[3].Name = "Rating";

			// Populate the rows.
			string[] row1 = new string[] { "Meatloaf", "Main Dish", boringMeatloaf, boringMeatloafRanking };
			string[] row2 = new string[] { "Key Lime Pie", "Dessert", "lime juice, evaporated milk", "****" };
			string[] row3 = new string[] { "Orange-Salsa Pork Chops", "Main Dish", "pork chops, salsa, orange juice", "****" };
			string[] row4 = new string[] { "Black Bean and Rice Salad", "Salad", "black beans, brown rice", "****" };
			string[] row5 = new string[] { "Chocolate Cheesecake", "Dessert", "cream cheese", "***" };
			string[] row6 = new string[] { "Black Bean Dip", "Appetizer", "black beans, sour cream", "***" };
			string[] row7 = new string[] { "Black Bean and Rice Salad", "Salad", "black beans, brown rice", "****" };
			string[] row8 = new string[] { "Chocolate Cheesecake", "Dessert", "cream cheese", "***" };
			string[] row9 = new string[] { "Black Bean Dip", "Appetizer", "black beans, sour cream", "***" };
			object[] rows = new object[] { row1, row2, row3, row4, row5, row6, row7, row8, row9 };

			foreach (string[] rowArray in rows)
			{
				dataGridView.Rows.Add(rowArray);
			}

			PostRowCreation();

			shortMode = false;
			boringRecipe = true;
		}

		private void AdjustDataGridViewSizing()
		{
			dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		}

		private void PostRowCreation()
		{
			SetBandColor(dataGridView.Columns[0], Color.Red);
			SetBandColor(dataGridView.Rows[1], Color.Green);
			SetBandColor(dataGridView.Columns[2], Color.Blue);
		}
		// DataGridViewBand 操作某一行或者某一列，某一行或者某一列就是一个条块。
		private static void SetBandColor(DataGridViewBand band, Color color)
		{
			band.Tag = color;
		}

		private void Button4_Click(object sender, System.EventArgs e)
		{

			FreezeBand(dataGridView.Rows[0]);
		}

		private void Button5_Click(object sender, System.EventArgs e)
		{

			FreezeBand(dataGridView.Columns[1]);
		}

		private static void FreezeBand(DataGridViewBand band)
		{
			band.Frozen = true;
			DataGridViewCellStyle style = new DataGridViewCellStyle();
			style.BackColor = Color.WhiteSmoke;
			band.DefaultCellStyle = style;
		}

		// Hide a band of cells.
		private void Button6_Click(object sender, System.EventArgs e)
		{

			DataGridViewBand band = dataGridView.Rows[3];
			band.Visible = false;
		}

		// Turn off user's ability to resize a column.
		private void Button7_Click(object sender, EventArgs e)
		{

			DataGridViewBand band = dataGridView.Columns[0];
			band.Resizable = DataGridViewTriState.False;
		}

		// Make the entire DataGridView read only.
		private void Button8_Click(object sender, System.EventArgs e)
		{
			// DataGridView 控件中操作区段，一行或者一列都是区段
			foreach (DataGridViewBand band in dataGridView.Columns)
			{
				band.ReadOnly = true;
			}
		}

		// Color the bands by the value stored in their tag.
		// 获取或设置包含与带区关联的数据的对象。
		private void Button9_Click(object sender, System.EventArgs e)
		{
			// PostRowCreation
			foreach (DataGridViewBand band in dataGridView.Columns)
			{
				if (band.Tag != null)
				{
					band.DefaultCellStyle.BackColor = (Color)band.Tag;
				}
			}

			foreach (DataGridViewBand band in dataGridView.Rows)
			{
				if (band.Tag != null)
				{
					band.DefaultCellStyle.BackColor = (Color)band.Tag;
				}
			}
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

		#endregion
	}
}

