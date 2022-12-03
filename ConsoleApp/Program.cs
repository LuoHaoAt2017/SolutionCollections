
using ConsoleApp.Models;

public class Program
{
	public static void Main()
	{
		Film film;
		int index;
		//MethodOut1(out index);
		//MethodRef1(ref index);
		//MethodOut2(out film);
		//MethodRef2(ref film);
	}

	public static Film Go()
	{
		Film film = new Film();
		film.FilmGuid = DateTime.Now.ToString();
		film.FilmName = "复仇三部曲";
		return film;
	}

	public static void Method(int index)
	{

	}

	public static void MethodOut1(out int index)
	{
		index = 0;
	}

	public static void MethodRef1(ref int index)
	{

	}

	public static void MethodOut2(out Film film)
	{
		film = new Film();
		film.FilmGuid = DateTime.Now.ToString();
		film.FilmName = "复仇三部曲";
	}

	public static void MethodRef2(ref Film film)
	{
		film.FilmGuid = DateTime.Now.ToString();
		film.FilmName = "寻仇三部曲";
	}
}