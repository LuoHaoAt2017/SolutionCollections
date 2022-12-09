using Spire.Pdf;
using Spire.Pdf.Graphics;
using BarcodeLib;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;

public class Program
{

	public struct ReportInfo
	{
		public string accessionNo;
		public string patientName;
		public string patientAge;
		public string reportTitle;
	}

	public static void Main(string[] args)
	{
		ReportInfo report = new ReportInfo();
		report.accessionNo = "1280228";
		report.patientName = "刘虎";
		report.patientAge = "27";
		report.reportTitle = "传统胶片条形码";
		string pdfPath = CreatePdfDoc(report);
		if (!string.IsNullOrEmpty(pdfPath))
		{
			PrintPdfDoc(pdfPath);
		}
		
	}

	public static void PrintPdfDoc(string pdfPath)
	{
		PdfDocument pdf = new PdfDocument();
		pdf.LoadFromFile(pdfPath);
		PrintDocument document = new PrintDocument();

		// 设置纸张规格
		foreach(PaperSize ps in document.PrinterSettings.PaperSizes)
		{
			if (ps.PaperName =="A4")
			{
				pdf.PrintSettings.PaperSize = ps;
			}
		}
		
		// 指定打印机名称
		pdf.PrintSettings.PrinterName = "";

		// 设置静默打印
		pdf.PrintSettings.PrintController = new StandardPrintController();

		// 执行打印
		try
		{
			pdf.Print();
		}
		catch(Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
		finally
		{
			pdf.Close();
		}
	}

	public static string CreatePdfDoc(ReportInfo report)
	{
		try
		{
			PdfDocument pdf = new PdfDocument();
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			pdf.LoadFromFile($"{basePath}/Sources/template/template.pdf");
			PdfPageBase page = pdf.Pages[0];
			PdfTrueTypeFont font = new PdfTrueTypeFont(new Font("宋体", 24F, FontStyle.Regular), true);
			page.Canvas.DrawString(report.reportTitle, font, PdfBrushes.Black, new Point(250, 100));
			page.Canvas.DrawString(report.patientName, font, PdfBrushes.Black, new Point(200, 300));
			page.Canvas.DrawString(report.patientAge, font, PdfBrushes.Black, new Point(400, 300));

			string filePath = CreateBarcode(report);
			if (string.IsNullOrEmpty(filePath))
			{
				page.Canvas.DrawImage(PdfImage.FromFile(filePath), new Point(125, 160), new Size(400, 100));
			}

			page.Canvas.Save();

			string path = $"{basePath}/Sources/pdf/{report.accessionNo}.pdf";
			pdf.SaveToFile(path, FileFormat.PDF);
			return path;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Message: {ex.Message}");
			return "";
		}
	}

	public static string CreateBarcode(ReportInfo report)
	{
		string filePath;
		try
		{
			filePath = AppDomain.CurrentDomain.BaseDirectory + "Sources/images/" + report.accessionNo + ".jpg";
			Barcode barcode = new Barcode();
			barcode.BackColor = Color.White;
			barcode.ForeColor = Color.Black;
			barcode.IncludeLabel = true;
			barcode.LabelPosition = LabelPositions.BOTTOMCENTER;
			barcode.ImageFormat = ImageFormat.Jpeg;
			barcode.LabelFont = new Font("verdana", 18f);
			barcode.Width = 400;
			barcode.Height = 100;

			Image image = barcode.Encode(TYPE.CODE128, report.accessionNo);
			image.Save(filePath);
		}
		catch(Exception ex)
		{
			Console.WriteLine(ex.Message);
			filePath = "";
		}
		return filePath;
	}
}