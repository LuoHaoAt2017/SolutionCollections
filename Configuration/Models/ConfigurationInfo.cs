namespace Configuration.Models
{
	public class ConfigurationInfo
	{
		public Machine machine { get; set; }
	}

	public class Machine
	{
		public string designer { set; get; }
		public string manufacturer { set; get; }
		public Location location { set; get; }
	}

	public class Location
	{
		public string province { set; get; }
		public string city { set; get; }
		public string region { set; get; }
	}
}
