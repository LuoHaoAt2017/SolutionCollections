using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetADO.Models
{
	public class Machine
	{
		public string designer { get; set; }

		public string manufacturer { get; set; }

		public Location location { get; set; }
	}

	public class Location
	{
		public string province { get; set; }

		public string city { get; set; }

		public string region { get; set; }
	}
}
