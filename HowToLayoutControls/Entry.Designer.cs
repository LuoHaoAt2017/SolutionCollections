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
			this.Text = "入口";
			CreateMyListView();
			ProductTable();
		}

		private void CreateMyListView()
		{
			// Create a new ListView control.
			ListView listView1 = new ListView();
			listView1.Bounds = new Rectangle(new Point(10, 10), new Size(300, 200));

			//// Set the view to show details.
			listView1.View = View.Details;
			//// Allow the user to edit item text.
			//listView1.LabelEdit = true;
			//// Allow the user to rearrange columns.
			//listView1.AllowColumnReorder = true;
			//// Display check boxes.
			listView1.CheckBoxes = true;
			//// Select the item and subitems when selection is made.
			//listView1.FullRowSelect = true;
			//// Display grid lines.
			//listView1.GridLines = true;
			//// Sort the items in the list in ascending order.
			//listView1.Sorting = SortOrder.Ascending;

			// Create three items and three sets of subitems for each item.
			ListViewItem item1 = new ListViewItem("item1", 0);
			// Place a check mark next to the item.
			item1.Checked = true;
			item1.SubItems.Add("1");
			item1.SubItems.Add("2");
			item1.SubItems.Add("3");
			ListViewItem item2 = new ListViewItem("item2", 1);
			item2.SubItems.Add("4");
			item2.SubItems.Add("5");
			item2.SubItems.Add("6");
			ListViewItem item3 = new ListViewItem("item3", 0);
			// Place a check mark next to the item.
			item3.Checked = true;
			item3.SubItems.Add("7");
			item3.SubItems.Add("8");
			item3.SubItems.Add("9");

			// Create columns for the items and subitems.
			// Width of -2 indicates auto-size.
			listView1.Columns.Add("Item Column", -2, HorizontalAlignment.Left);
			listView1.Columns.Add("Column 2", -2, HorizontalAlignment.Left);
			listView1.Columns.Add("Column 3", -2, HorizontalAlignment.Left);
			listView1.Columns.Add("Column 4", -2, HorizontalAlignment.Center);

			//Add the items to the ListView.
			listView1.Items.AddRange(new ListViewItem[] { item1, item2, item3 });

			//// Create two ImageList objects.
			//ImageList imageListSmall = new ImageList();
			//ImageList imageListLarge = new ImageList();

			//// Initialize the ImageList objects with bitmaps.
			//imageListSmall.Images.Add(Bitmap.FromFile("C:\\MySmallImage1.bmp"));
			//imageListSmall.Images.Add(Bitmap.FromFile("C:\\MySmallImage2.bmp"));
			//imageListLarge.Images.Add(Bitmap.FromFile("C:\\MyLargeImage1.bmp"));
			//imageListLarge.Images.Add(Bitmap.FromFile("C:\\MyLargeImage2.bmp"));

			////Assign the ImageList objects to the ListView.
			//listView1.LargeImageList = imageListLarge;
			//listView1.SmallImageList = imageListSmall;

			// Add the ListView to the control collection.
			this.Controls.Add(listView1);
		}

		private void ProductFilter()
		{

		}

		private void ProductTable()
		{
			ListView listView = new ListView();
			listView.View = View.Details;
			listView.CheckBoxes = true;
			listView.FullRowSelect = true;
			listView.Bounds = new Rectangle(new Point(400, 10), new Size(400, 400));

			DataTable dataTable = GetProductSource();
			for(int i = 0; i < dataTable.Rows.Count; i++)
			{
				ListViewItem item = new ListViewItem();
			}
			ListViewItem[] items = new ListViewItem[3];
			ListViewItem item1 = new ListViewItem("0001");
			ListViewItem item2 = new ListViewItem("0002");
			ListViewItem item3 = new ListViewItem("0003");

			item1.SubItems.Add("0001");
			item1.SubItems.Add("沃尔沃");
			item1.SubItems.Add("45");
			item1.Checked = false;

			item2.SubItems.Add("0001");
			item2.SubItems.Add("沃尔沃");
			item2.SubItems.Add("45");
			item2.Checked = false;

			item3.SubItems.Add("0001");
			item3.SubItems.Add("沃尔沃");
			item3.SubItems.Add("45");
			item3.Checked = false;

			items[0] = item1;
			items[1] = item2;
			items[2] = item3;

			listView.Items.AddRange(items);

			string[] columns = new string[4] { "Selected", "ProductId", "ProductName", "ProductPrice" };

			foreach (var col in columns)
			{
				listView.Columns.Add(col, -2, HorizontalAlignment.Center);
			}

			this.Controls.Add(listView);
		}

		private DataTable GetProductSource()
		{
			string[] columns = new string[3] { "ProductId", "ProductName", "ProductPrice" };
			DataTable dataTable = new DataTable();
			for (int i = 0; i < columns.Length; i++)
			{
				DataColumn col = new DataColumn();
				col.AllowDBNull = false;
				col.ColumnName = columns[i];
				col.DataType = System.Type.GetType("System.String");
				dataTable.Columns.Add(col);
			}
			for (int i = 0; i < 30; i++)
			{
				Random rand = new Random();
				DataRow row = dataTable.NewRow();
				row[columns[i]] = rand.Next().ToString();
			}
			return dataTable;
		}

		private void ProductForm()
		{

		}
		#endregion
	}
}

