using CopyBase.Settings;

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;

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
            //TODO: 중복 이름 처리
            if (string.IsNullOrWhiteSpace(alias))
            {
                Console.WriteLine("Cannot specify alias.");
                return -1;
            }
            if (string.IsNullOrWhiteSpace(baseFilePath) || !File.Exists(baseFilePath))
            {
                Console.WriteLine("The base file doesn't exist.");
                return -1;
            }
            if (string.IsNullOrWhiteSpace(targetFilePath) || !File.Exists(targetFilePath))
            {
                Console.WriteLine("The target file doesn't exist.");
                return -1;
            }

            CopyBaseSettings.LoadFromFile();
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

            Console.WriteLine("Alias add completed!");

            return 0;
        }
    }
}