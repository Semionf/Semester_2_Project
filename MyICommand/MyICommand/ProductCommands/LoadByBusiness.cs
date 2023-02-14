using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyICommand.MyICommand;
using Utilities;

namespace MyICommand.MyICommand.ProductCommands
{
    public class LoadByBusiness : CommandManager, ICommand
    {
        public LoadByBusiness(Logger log) : base(log)
        {
        }

        public object ExecuteCommand(params object[] param)
        {
            throw new NotImplementedException();
        }
    }
}
