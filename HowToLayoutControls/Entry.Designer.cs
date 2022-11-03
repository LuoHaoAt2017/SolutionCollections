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
			ProductTable();
		}

		private void ProductTable()
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

		#endregion
	}
}

