using CopyBase.Settings;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;

namespace CopyBase.Commands
{
    class CopyCommand : ICommand
    {
        public Command GetCommand()
        {
            var command = new Command("copy");
            command.AddArgument(new Argument<string>("alias"));
            command.Handler = CommandHandler.Create<string>(HandleCommand);

            return command;
        }

        public int HandleCommand(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                Console.WriteLine("Cannot specific alias name.");
                return -1;
            }

            CopyBaseSettings.LoadFromFile();
            var setting = CopyBaseSettings.Items.Find(item => item.Alias == alias);
            if (setting == null)
            {
                Console.WriteLine("No alias matched");
                return -1;
            }

            var baseFilePath = setting.Element.BaseFilePath;
            var targetFilePath = setting.Element.TargetFilePath;
            // TODO: Overwrite file

            return 0;
        }
    }
}
