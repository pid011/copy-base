using CopyBase.Settings;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Text;

namespace CopyBase.Commands.Alias
{
    class AliasAddCommand : ICommand
    {
        public Command CreateCommand()
        {
            var command = new Command("add", description: "Add alias.");
            var aliasOption = new Option(new string[] { "-alias", "-a" }, description: "Alias name")
            {
                Argument = new Argument<string>()
            };
            var baseFilePathOption = new Option(new string[] { "-base", "-b" }, description: "Base file path")
            {
                Argument = new Argument<string>()
            };
            var targetFilePathOption = new Option(new string[] { "-target", "-t" }, description: "Target file path")
            {
                Argument = new Argument<string>()
            };
            command.AddOption(aliasOption);
            command.AddOption(baseFilePathOption);
            command.AddOption(targetFilePathOption);
            command.Handler = CommandHandler.Create<string, string, string>(HandleSetupCommand);

            return command;
        }

        public int HandleSetupCommand(string alias, string baseFilePath, string targetFilePath)
        {
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
