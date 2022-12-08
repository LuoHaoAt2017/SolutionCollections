using Spire.Pdf;
using Spire.Pdf.Graphics;
using BarcodeLib;
using System.Drawing;
using System.Drawing.Printing;
using System;
using System.Drawing.Imaging;

public class Program
{
	public static void Main(string[] args)
	{

		try
		{
			string accessionNo = "1280228";
			string patientName = "刘虎";
			string patientAge = "27";
			string reportTitle = "传统胶片条形码";
			string filePath = AppDomain.CurrentDomain.BaseDirectory + "Sources/images/" + accessionNo + ".jpg";
			Barcode barcode = new Barcode();
			barcode.BackColor = Color.White;
			barcode.ForeColor = Color.Black;
			barcode.IncludeLabel = true;
			barcode.LabelPosition = LabelPositions.BOTTOMCENTER;
			barcode.ImageFormat = ImageFormat.Jpeg;
			barcode.LabelFont = new Font("verdana", 18f);
			barcode.Width = 400;
			barcode.Height = 100;

			Image image = barcode.Encode(TYPE.CODE128, accessionNo);
			image.Save(filePath);

			PdfDocument pdf = new PdfDocument();
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			pdf.LoadFromFile($"{basePath}/Sources/template/template.pdf");

			PdfPageBase page = pdf.Pages[0];
			PdfTrueTypeFont font = new PdfTrueTypeFont(new Font("宋体", 24F, FontStyle.Regular), true);
			page.Canvas.DrawString(reportTitle, font, PdfBrushes.Black, new Point(250, 100));
			page.Canvas.DrawImage(PdfImage.FromFile(filePath), new Point(125, 160), new Size(400, 100));
			page.Canvas.DrawString(patientName, font, PdfBrushes.Black, new Point(200, 300));
			page.Canvas.DrawString(patientAge, font, PdfBrushes.Black, new Point(400, 300));
			page.Canvas.Save();
			pdf.SaveToFile($"{basePath}/Sources/pdf/{accessionNo}.pdf", FileFormat.PDF);
		}
		catch(Exception ex)
		{
			Console.WriteLine($"Message: {ex.Message}");
		}
	}
}