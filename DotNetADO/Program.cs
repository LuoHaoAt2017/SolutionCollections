using System;
using System.Windows.Forms;
// DataSet 类基本上是内存中的数据库，包含了所有表，关系，约束。
// DataSet 和相关的类已经被 Entity Framework 代替。

// SelectCommand
// InsertCommand
// DeleteCommand
// UpdateCommand

// 对频繁使用的命令采用存储过程来执行
// 对不常用的命令直接用SQL命令来执行

namespace DotNetADO
{
	internal static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new NorthWind());
		}
	}
}
