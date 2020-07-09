// Copyright (c) Sepi. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CopyBase.Commands;

using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace CopyBase
{
    internal class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand("You can work quickly overwrite the target file into the base file.")
            {
                new CopyCommand().GetCommand(),
                new AliasCommand().GetCommand()
            };

            rootCommand.Name = "copybase";
            return await rootCommand.InvokeAsync(args).ConfigureAwait(false);
        }
    }
}
