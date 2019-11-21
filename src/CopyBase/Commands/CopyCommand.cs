using CopyBase.Settings;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
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

        private int HandleCommand(string alias)
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

            Console.WriteLine(setting.ToString());
            bool choose = Tools.UserChoiceProcess("Are you sure you want to copy the base code?");
            if (choose)
            {
                File.Copy(setting.Element.BaseFilePath, setting.Element.TargetFilePath, overwrite: true);
                Console.WriteLine("Copy complete.");
            }
            else
            {
                Console.WriteLine("Copy canceled.");
            }

            return 0;
        }
    }
}
