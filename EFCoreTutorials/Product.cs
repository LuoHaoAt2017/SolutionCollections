using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreTutorials
{
	internal class Product
	{
		public int ProductId { get; set; }
		public string? Name { get; set; }
		public int CategoryId { get; set; }
		// Products类上的Category属性称为“导航”。 在 EF Core 中，导航定义两种实体类型之间的关系。 
		public virtual Category Category { get; set; } = null;
	}
}
