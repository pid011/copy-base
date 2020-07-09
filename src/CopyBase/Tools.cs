// Copyright (c) Sepi. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

            PrintMessage(builder.ToString());
        }

        public static bool UserChoiceProcess(string msg)
        {
            PrintMessage(msg);
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

        public static void PrintMessage()
        {
            Console.WriteLine();
        }

        public static void PrintMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        public static void PrintMessage(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            PrintMessage(msg);
            Console.ResetColor();
        }
    }
}
