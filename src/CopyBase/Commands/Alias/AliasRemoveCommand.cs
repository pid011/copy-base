﻿using CopyBase.Settings;

using System;
using System.CommandLine;
using System.CommandLine.Invocation;

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
                Console.WriteLine($"No alias matches [{alias}].");
                return -1;
            }

            CopyBaseSettings.Items.RemoveAt(idx);
            CopyBaseSettings.SaveToFile();

            Console.WriteLine("Alias remove completed!");
            return 0;
        }
    }
}