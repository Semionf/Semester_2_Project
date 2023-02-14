using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoteIt.Entities;


namespace MyICommand.MyICommand
{
    public interface ICommand
    {
        object ExecuteCommand(params object[] param);
    }

}
