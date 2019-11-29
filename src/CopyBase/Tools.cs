using System;
using System.Text;

namespace CopyBase
{
    internal static class Tools
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

        public static bool UserChoiceProcess(string msg)
        {
            Console.WriteLine(msg);
            bool chooseYes = false;
            bool chooseNo = false;

            while (!chooseYes && !chooseNo)
            {
                Console.Write("(y/n): ");
                var choose = Console.ReadLine().ToLower();
                switch (choose)
                {
                    case "y":
                    case "yes":
                        chooseYes = true;
                        break;

                    case "n":
                    case "no":
                        chooseNo = true;
                        break;
                }
            }
            return chooseYes;
        }
    }
}