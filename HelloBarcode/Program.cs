using System.Drawing;
using System.Drawing.Imaging;
using BarcodeLib;

public class Program
{
	public static void Main(string[] args)
	{
		try
		{
			string accessNo = "1280228";
			string filePath = AppDomain.CurrentDomain.BaseDirectory + "Sources/images/" + accessNo + ".jpg";
			Barcode barcode = new Barcode();
			barcode.BackColor = Color.White;
			barcode.ForeColor = Color.Black;
			barcode.IncludeLabel = true;
			barcode.LabelPosition = LabelPositions.BOTTOMCENTER;
			barcode.ImageFormat = ImageFormat.Jpeg;
			barcode.LabelFont = new Font("verdana", 18f);
			barcode.Width = 400;
			barcode.Height = 100;

			Image image = barcode.Encode(TYPE.CODE128, accessNo);
			image.Save(filePath);
		}
		catch(Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
}