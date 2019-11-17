using System;
using System.Collections.Generic;
using System.Text;

namespace CopyBase
{
    static class Tools
    {
        public static void DisplayTextLine(int repeat)
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
