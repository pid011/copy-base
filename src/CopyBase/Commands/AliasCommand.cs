﻿// Copyright (c) Sepi. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CopyBase.Commands.Alias;

using System.CommandLine;

namespace CopyBase.Commands
{
    internal class AliasCommand : ICommand
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