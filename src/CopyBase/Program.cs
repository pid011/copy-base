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
using CopyBase.Commands;

namespace CopyBase
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand("You can work quickly overwrite the target file into the base file.")
            {
                new CopyCommand().GetCommand(),
                new AliasCommand().GetCommand()
            };
            return await rootCommand.InvokeAsync(args).ConfigureAwait(false);
        }
    }
}
