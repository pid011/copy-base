using System;
using System.Text;
using Figgle;

namespace CopyBase
{
    class Program
    {

        static void Main(string[] args)
        {
            var figgle = FiggleFonts.Standard.Render("CopyBase");
            Console.WriteLine(figgle);
        }

        private static void DisplayTextLine(int repeat)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < repeat; i++)
            {
                builder.Append('=');
            }

            Console.WriteLine(builder.ToString());
        }
    }
}
