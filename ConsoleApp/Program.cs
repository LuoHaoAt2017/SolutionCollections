using Newtonsoft.Json;

public class Program
{
	public static void Main()
	{
		string jsonData = "[{\"Status\":\"trayEmpty\"},{\"Alerts\":[{\"Tray\":\"All\",\"Status\":\"trayEmpty\"},{\"Tray\":\"Tray1\",\"Status\":\"trayEmpty\"},{\"Tray\":\"Tray2\",\"Status\":\"trayEmpty\"}]}]";
		Response[] newJson = JsonConvert.DeserializeObject<Response[]>(jsonData);
		foreach(Response item in newJson)
		{
			if (item.Alerts != null)
			{
				foreach (Alert alert in item.Alerts)
				{
					Console.WriteLine($"Tray: {alert.Tray}, Status: {alert.Status}");
				}
			}
			if (item.Status != null)
			{
				Console.WriteLine($"Status: {item.Status}");
			}
		}
	}

	class Response
	{
		public Alert[]? Alerts;
		public string? Status;
	}

	class Alert
	{
		public string Tray { get; set; }
		public string Status { get; set; }
	}
}