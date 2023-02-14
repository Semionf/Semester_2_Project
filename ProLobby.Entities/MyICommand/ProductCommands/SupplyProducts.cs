using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PromoteIt.Entities.ICommand.ProductCommands
{
    public class SupplyProducts : CommandManager, ICommand
    {
        public SupplyProducts(Logger log) : base(log)
        {
        }

        public object ExecuteCommand(params object[] param)
        {
            throw new NotImplementedException();
        }
    }
}
