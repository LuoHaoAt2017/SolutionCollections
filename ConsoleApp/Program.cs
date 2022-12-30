using Newtonsoft.Json;

// 通过委托类型来描述事件和事件处理程序

public class Program
{
	public static void Main()
	{
<<<<<<< HEAD
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
=======
		//Film film = new Film();
		//Console.WriteLine(film.GetType().ToString());
		//Console.WriteLine(typeof(Film));
		//int index;
		//MethodOut1(out index);
		//MethodRef1(ref index);
		//MethodOut2(out film);
		//MethodRef2(ref film);
    }
>>>>>>> 8a84eb44c25e6be2acfa29014a74f7f621165cdd

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