// Copyright (c) Sepi. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CopyBase.Settings;

using System;
using System.CommandLine;
using System.CommandLine.Invocation;

using static CopyBase.Tools;

namespace CopyBase.Commands.Alias
{
    internal class AliasRemoveCommand : ICommand
    {
        public Command GetCommand()
        {
            var command = new Command("remove", description: "Remove alias.");
            command.AddArgument(new Argument<string>("alias"));
            command.Handler = CommandHandler.Create<string>(HandleSetupCommand);
            return command;
        }

        private int HandleSetupCommand(string alias)
        {
            CopyBaseSettings.LoadFromFile();
            int idx = CopyBaseSettings.Items.FindIndex(x => x.Alias == alias);
            if (idx == -1)
            {
                PrintMessage($"No alias matches [{alias}].", ConsoleColor.Red);
                return -1;
            }

            CopyBaseSettings.Items.RemoveAt(idx);
            CopyBaseSettings.SaveToFile();

            PrintMessage("Alias remove completed!");
            return 0;
        }
    }
}