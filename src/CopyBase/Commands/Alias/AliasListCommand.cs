// Copyright (c) Sepi. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;
using CopyBase.Settings;
using static CopyBase.Tools;

namespace CopyBase.Commands.Alias
{
    internal class AliasListCommand : ICommand
    {
        public Command GetCommand()
        {
            var command = new Command("list", description: "Show alias list.")
            {
                Handler = CommandHandler.Create(HandleSetupCommand)
            };
            return command;
        }

        private int HandleSetupCommand()
        {
            CopyBaseSettings.LoadFromFile();

            if (CopyBaseSettings.Items.Count == 0)
            {
                PrintMessage("There is no alias. First, you need to add alias.", ConsoleColor.Yellow);
                return 0;
            }

            var aliasesOuput = new StringBuilder();
            CopyBaseSettings.Items.ForEach(item => aliasesOuput.AppendLine(item.ToString()));
            PrintMessage(aliasesOuput.ToString(), ConsoleColor.Yellow);
            return 0;
        }
    }
}
