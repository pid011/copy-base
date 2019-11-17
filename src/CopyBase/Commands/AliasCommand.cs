using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;
using CopyBase.Commands.Alias;

namespace CopyBase.Commands
{
    class AliasCommand : ICommand
    {
        public Command CreateCommand()
        {
            var command = new Command("alias");
            command.AddCommand(new AliasAddCommand().CreateCommand());
            command.AddCommand(new AliasRemoveCommand().CreateCommand());
            command.AddCommand(new AliasListCommand().CreateCommand());

            return command;
        }
    }
}
