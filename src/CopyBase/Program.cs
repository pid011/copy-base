using System.IO;
using System.Threading.Tasks;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine;
using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using CopyBase.Settings;

namespace CopyBase
{
    class Program
    {
        static void Main(string[] args)
        {
            var copyOption = new Option(new string[] { "-copy", "-c" }, description: "overwrite the target file into the base file.");

            var rootCommand = new RootCommand("You can work quickly overwrite the target file into the base file.");
            rootCommand.Add()
        }
    }
}
