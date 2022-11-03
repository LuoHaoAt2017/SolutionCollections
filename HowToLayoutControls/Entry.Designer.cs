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
		private int ratingColumn = 3;
		private bool boringRecipe;
		private string thirdColumnHeader = "Main Ingredients";
		private string boringMeatloaf = "ground beef";
		private string boringMeatloafRanking = "*";

		private DataGridView dataGridView;
		private FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
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
		Button Button11 = new Button();
		Button Button12 = new Button();
		Button Button13 = new Button();
		Button Button14 = new Button();
		Button Button15 = new Button();
		Button Button16 = new Button();
		Button Button17 = new Button();
		Button Button18 = new Button();
		Button Button19 = new Button();
		Button Button20 = new Button();
		Button Button21 = new Button();
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
		}

		private void DataGridViewBandDemo()
		{

			flowLayoutPanel.Height = 120;
			flowLayoutPanel.Dock = DockStyle.Bottom;
			AddButton(Button1, "Reset", new EventHandler(Button1_Click));
			AddButton(Button2, "Change Column 3 Header", new EventHandler(Button2_Click));
			AddButton(Button3, "Change Meatloaf Recipe", new EventHandler(Button3_Click));
			this.Controls.Add(flowLayoutPanel);
			AddAdditionalButtons();
			CreateDataGridView();
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
			AddButton(Button10, "Set Row Two Minimum Height", new EventHandler(Button10_Click));
			AddButton(Button11, "Set Row One Height", new EventHandler(Button11_Click));
			AddButton(Button12, "Label Rows", new EventHandler(Button12_Click));
			AddButton(Button13, "Turn on Extra Edge", new EventHandler(Button13_Click));
			AddButton(Button14, "Give Cheesecake an Excellent Rating", new EventHandler(Button14_Click));

			AddButton(Button15, "Set Width of Column One", new EventHandler(Button15_Click));
			AddButton(Button16, "Autosize Third Column", new EventHandler(Button16_Click));
			AddButton(Button17, "Add Thick Vertical Edge", new EventHandler(Button17_Click));
			AddButton(Button18, "Style and Number Columns", new EventHandler(Button18_Click));
			AddButton(Button19, "Change Column Header Text", new EventHandler(Button19_Click));
			AddButton(Button20, "Swap First and Last Columns", new EventHandler(Button20_Click));
			AddButton(Button21, "Set Minimum Width of Column Two", new EventHandler(Button21_Click));
		}

		private static void Toggle(ref bool toggleThis)
		{
			toggleThis = !toggleThis;
		}

		// 通过行和列来改变单元格的值
		private void SetMeatloaf(string recipe, string rating)
		{
			dataGridView.Rows[0].Cells[2].Value = recipe;
			dataGridView.Rows[0].Cells[3].Value = rating;
		}

		private void CreateDataGridView()
		{
			dataGridView = new DataGridView();
			this.Controls.Add(dataGridView);
			dataGridView.Size = new Size(300, 200);
			dataGridView.Location = new Point(400, 100);
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

		private static void FreezeBand(DataGridViewBand band)
		{
			band.Frozen = true;
			DataGridViewCellStyle style = new DataGridViewCellStyle();
			style.BackColor = Color.WhiteSmoke;
			band.DefaultCellStyle = style;
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

		private void Button4_Click(object sender, System.EventArgs e)
		{

			FreezeBand(dataGridView.Rows[0]);
		}

		private void Button5_Click(object sender, System.EventArgs e)
		{

			FreezeBand(dataGridView.Columns[1]);
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

		private void Button10_Click(object sender, System.EventArgs e)
		{
			DataGridViewRow row = dataGridView.Rows[1];
			row.MinimumHeight = 40;
		}

		private void Button11_Click(object sender, System.EventArgs e)
		{
			DataGridViewRow row = dataGridView.Rows[0];
			row.Height = 15;
		}

		private void Button12_Click(object sender, System.EventArgs e)
		{
			int rowNumber = 1;
			foreach (DataGridViewRow row in dataGridView.Rows)
			{
				if (row.IsNewRow) continue;
				row.HeaderCell.Value = "Row " + rowNumber;
				rowNumber = rowNumber + 1;
			}
			dataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
		}

		private void Button13_Click(object sender, System.EventArgs e)
		{
			int secondRow = 1;
			DataGridViewRow row = dataGridView.Rows[secondRow];
			row.DividerHeight = 10;
		}

		private void Button14_Click(object sender, System.EventArgs e)
		{
			UpdateStars(dataGridView.Rows[4], "******************");
		}

		private void Button15_Click(object sender, System.EventArgs e)
		{
			DataGridViewColumn column = dataGridView.Columns[0];
			column.Width = 60;
		}

		private void Button16_Click(object sender, System.EventArgs e)
		{
			DataGridViewColumn column = dataGridView.Columns[2];
			column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
		}

		private void Button17_Click(object sender, System.EventArgs e)
		{
			DataGridViewColumn column = dataGridView.Columns[2];
			column.DividerWidth = 10;
		}

		private void Button18_Click(object sender, System.EventArgs e)
		{
			DataGridViewCellStyle style = new DataGridViewCellStyle();
			style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			style.ForeColor = Color.IndianRed;
			style.BackColor = Color.Ivory;

			foreach (DataGridViewColumn column in dataGridView.Columns)
			{
				column.HeaderCell.Value = column.Index.ToString();
				column.HeaderCell.Style = style;
			}
		}

		private void Button19_Click(object sender, System.EventArgs e)
		{
			foreach (DataGridViewColumn column in dataGridView.Columns)
			{

				column.HeaderText = String.Concat("Column ", column.Index.ToString());
			}
		}

		private void Button20_Click(object sender, System.EventArgs e)
		{
			DataGridViewColumnCollection columnCollection = dataGridView.Columns;

			DataGridViewColumn firstVisibleColumn = columnCollection.GetFirstColumn(DataGridViewElementStates.Visible);
			DataGridViewColumn lastVisibleColumn = columnCollection.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None);

			int firstColumn_sIndex = firstVisibleColumn.DisplayIndex;
			firstVisibleColumn.DisplayIndex = lastVisibleColumn.DisplayIndex;
			lastVisibleColumn.DisplayIndex = firstColumn_sIndex;
		}

		private void Button21_Click(object sender, System.EventArgs e)
		{
			DataGridViewColumn column = dataGridView.Columns[1];
			column.MinimumWidth = 40;
		}

		private void UpdateStars(DataGridViewRow row, string stars)
		{

			row.Cells[ratingColumn].Value = stars;

			// Resize the column width to account for the new value.
			row.DataGridView.AutoResizeColumn(ratingColumn, DataGridViewAutoSizeColumnMode.DisplayedCells);
		}

		#endregion
	}
}

