using System.CommandLine;

namespace CopyBase.Commands
{
    internal interface ICommand
    {
        public Command GetCommand();
    }
}