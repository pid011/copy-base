using CopyBase.Settings;

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;

using static CopyBase.Tools;

namespace CopyBase.Commands
{
    internal class CopyCommand : ICommand
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
            // TODO: -y 옵션 추가
            if (string.IsNullOrWhiteSpace(alias))
            {
                PrintMessage("Cannot specific alias name.", ConsoleColor.Red);
                return -1;
            }

            CopyBaseSettings.LoadFromFile();
            var setting = CopyBaseSettings.Items.Find(item => item.Alias == alias);
            if (setting == null)
            {
                PrintMessage("No alias matched", ConsoleColor.Red);
                return -1;
            }
            PrintMessage(setting.ToString(), ConsoleColor.Yellow);

            bool choose = UserChoiceProcess("Are you sure you want to copy the base code?");
            if (choose)
            {
                File.Copy(setting.Element.BaseFilePath, setting.Element.TargetFilePath, overwrite: true);
                PrintMessage("Copy complete.");
            }
            else
            {
                PrintMessage("Copy canceled.");
            }

            return 0;
        }
    }
}