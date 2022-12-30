using System;
using System.IO;
using System.Text;

namespace FileReadWrite
{
    /// <summary>
    /// Read、 Write、 CopyTo和 Flush
    /// ReadAsync、 WriteAsync、 CopyToAsync和 FlushAsync
    /// </summary>
    class MainClass
    {
        public static void Main(string[] args)
        {
            string path = "test.txt";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (FileStream stream = File.Create(path))
            {
                for(int i = 0; i < 9; i++)
                {
                    AddText(stream, "Hello World!\n");
                }
            }

            using (FileStream stream = File.OpenRead(path))
            {
                ReadTxt(stream);
            }
        }

        public static void AddText(FileStream stream, string text)
        {
            UTF8Encoding encode = new UTF8Encoding(true);
            byte[] bytes = encode.GetBytes(text);
            stream.Write(bytes, 0, bytes.Length);
        }

        public static void ReadTxt(FileStream stream)
        {
            byte[] bytes = new byte[1024];
            UTF8Encoding encode = new UTF8Encoding(true);
            int readLen;
            do
            {
                readLen = stream.Read(bytes, 0, bytes.Length);
                string line = encode.GetString(bytes, 0, readLen);
                Console.WriteLine(line);
            }
            while (readLen > 0);
        }
    }
}
