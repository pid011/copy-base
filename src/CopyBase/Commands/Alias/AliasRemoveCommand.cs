using CopyBase.Settings;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;

namespace CopyBase.Commands.Alias
{
    class AliasRemoveCommand : ICommand
    {
        public Command CreateCommand()
        {
            var command = new Command("remove", description: "Remove alias.");
            command.AddArgument(new Argument<string>("alias"));
            command.Handler = CommandHandler.Create<string>(HandleSetupCommand); 
            return command;
        }

        public int HandleSetupCommand(string alias)
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
