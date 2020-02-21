using CopyBase.Settings;

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;

namespace CopyBase.Commands.Alias
{
    internal class AliasListCommand : ICommand
    {
        public Command GetCommand()
        {
            var command = new Command("list", description: "Show alias list.");
            command.Handler = CommandHandler.Create(HandleSetupCommand);
            return command;
        }

        private int HandleSetupCommand()
        {
            // TODO: 생성된 alias가 없을 경우 따로 메시지 출력
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