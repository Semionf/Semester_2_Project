using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyICommand.MyICommand;
using Utilities;

namespace MyICommand.MyICommand.UserMessageCommands
{
    public class PostMessage : CommandManager, ICommand
    {
        public PostMessage(Logger log) : base(log)
        {
        }

        public object ExecuteCommand(params object[] param)
        {
            throw new NotImplementedException();
        }
    }
}
