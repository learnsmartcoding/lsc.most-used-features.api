using System.Text;

ReadOnlySpan<byte> u8 = "Hello world in UTF-8"u8;

Console.WriteLine(Encoding.UTF8.GetString(u8));