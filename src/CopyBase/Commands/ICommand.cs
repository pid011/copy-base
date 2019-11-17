using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace CopyBase.Commands
{
    interface ICommand
    {
        public Command CreateCommand();
    }
}
