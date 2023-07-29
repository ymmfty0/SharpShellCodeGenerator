using System;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace GenerateShellCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string filePath = args[0];
                try
                {
                    byte[] fileBytes = File.ReadAllBytes(filePath);
                    string encoded = Convert.ToBase64String(fileBytes);

                    string shellcode = "shellcode.txt";

                    byte[] encodedbytes = Encoding.ASCII.GetBytes(encoded);

                    StringBuilder sb = new StringBuilder();

                    sb.Append("byte[] buf =  new byte["+ encodedbytes.Length+"]{\n\t");
                    int c = 0;
                    for (int i = 0; i < encodedbytes.Length; i++)
                    {
                        if (c == 12) { c = 0; sb.Append("\n\t"); }
                        sb.Append("0x" + encodedbytes[i].ToString("X2") + ",");
                        c++;
                    }
                    sb.Remove(sb.ToString().Length - 1, 1);
                    sb.Append("\n\t};\n");

                    Console.WriteLine(sb.ToString());

                    File.WriteAllText(shellcode, sb.ToString());

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла ошибка: " + ex.Message);
                }

            }

        }
    }
}
