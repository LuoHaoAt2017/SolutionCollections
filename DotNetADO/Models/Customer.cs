using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetADO.Models
{
	public class Customer
	{
		public string customerId { get; set; }

		public string customerName { get; set; }

		public string contactName { get; set; }

		public string address { get; set; }

		public string city { get; set; }

		public string postalCode { get; set; }

		public string country { get; set; }
	}
}
