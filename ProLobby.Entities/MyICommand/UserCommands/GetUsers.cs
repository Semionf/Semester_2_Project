using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoteIt.Entities.ICommand.UserCommands
{
    public class GetUsers : CommandManager, ICommand
    {
        public GetUsers(Logger log) : base(log)
        {
        }

        public object ExecuteCommand(params object[] param)
        {
            throw new NotImplementedException();
        }
    }
}
