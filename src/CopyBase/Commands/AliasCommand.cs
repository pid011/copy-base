using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;
using CopyBase.Commands.Alias;

namespace CopyBase.Commands
{
    class AliasCommand : ICommand
    {
        public Command GetCommand()
        {
            var command = new Command("alias");
            command.AddCommand(new AliasAddCommand().GetCommand());
            command.AddCommand(new AliasRemoveCommand().GetCommand());
            command.AddCommand(new AliasListCommand().GetCommand());

            return command;
        }
    }
}
