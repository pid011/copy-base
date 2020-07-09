// Copyright (c) Sepi. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.CommandLine;

namespace CopyBase.Commands
{
    internal interface ICommand
    {
        public Command GetCommand();
    }
}
