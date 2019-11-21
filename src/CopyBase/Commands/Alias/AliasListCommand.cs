using CopyBase.Settings;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;

namespace CopyBase.Commands.Alias
{
    class AliasListCommand : ICommand
    {
        public Command GetCommand()
        {
            var command = new Command("list", description: "Show alias list.");
            command.Handler = CommandHandler.Create(HandleSetupCommand);
            return command;
        }

        private int HandleSetupCommand()
        {
            CopyBaseSettings.LoadFromFile();
            StringBuilder aliasesOuput = new StringBuilder();
            CopyBaseSettings.Items.ForEach(item => aliasesOuput.AppendLine(item.ToString()));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(aliasesOuput.ToString());
            Console.ResetColor();
            return 0;
        }
    }
}
