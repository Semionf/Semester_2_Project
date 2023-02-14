using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoteIt.Entities.ICommand.TweetsCommands
{
    public class PostATweet : CommandManager, ICommand
    {
        public PostATweet(Logger log) : base(log)
        {
        }

        public object ExecuteCommand(params object[] param)
        {
            throw new NotImplementedException();
        }
    }
}
