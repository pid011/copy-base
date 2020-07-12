// Copyright (c) Sepi. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;

using CopyBase.Settings;

using static CopyBase.Tools;

namespace CopyBase.Commands.Alias
{
    internal class AliasAddCommand : ICommand
    {
        public Command GetCommand()
        {
            var command = new Command("add", description: "Add alias.");
            command.AddArgument(new Argument<string>("alias"));
            command.AddArgument(new Argument<string>("base-file-path"));
            command.AddArgument(new Argument<string>("target-file-path"));
            command.Handler = CommandHandler.Create<string, string, string>(HandleSetupCommand);

            return command;
        }

        private int HandleSetupCommand(string alias, string baseFilePath, string targetFilePath)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                PrintMessage("Cannot specify alias.", ConsoleColor.Red);
                return -1;
            }
            if (string.IsNullOrWhiteSpace(baseFilePath) || !File.Exists(baseFilePath))
            {
                PrintMessage("The base file doesn't exist.", ConsoleColor.Red);
                return -1;
            }
            if (string.IsNullOrWhiteSpace(targetFilePath) || !File.Exists(targetFilePath))
            {
                PrintMessage("The target file doesn't exist.", ConsoleColor.Red);
                return -1;
            }

            CopyBaseSettings.LoadFromFile();
            CopyBasePathItem list = CopyBaseSettings.Items.Find(x => x.Alias == alias);
            if (list != null)
            {
                PrintMessage("An alias with the same name already exists.\n", ConsoleColor.Red);
                PrintMessage(list.ToString(), ConsoleColor.Yellow);
                return -1;
            }

            CopyBaseSettings.Items.Add(new CopyBasePathItem()
            {
                Alias = alias,
                Element = new CopyBasePathItem.FilePathElement()
                {
                    BaseFilePath = baseFilePath,
                    TargetFilePath = targetFilePath
                }
            });
            CopyBaseSettings.SaveToFile();

            PrintMessage("Alias add completed!");

            return 0;
        }
    }
}
