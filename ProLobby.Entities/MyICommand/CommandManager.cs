using PromoteIt.Entities.ICommand.CampaignCommands;
using PromoteIt.Entities.ICommand.ProductCommands;
using PromoteIt.Entities.ICommand.RolesCommands;
using PromoteIt.Entities.ICommand.TweetsCommands;
using PromoteIt.Entities.ICommand.UserCommands;
using PromoteIt.Entities.ICommand.UserMessageCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using PromoteIt.Entities.ICommand;

namespace PromoteIt.Entities.ICommand
{
    public class CommandManager : BaseEntity
    {

        public static CommandManager Instance;
        public CommandManager(Logger log) : base(log)
        {

        }

        private Dictionary<string, ICommand> _CampaignCommand;

        public Dictionary<string, ICommand> CampaignCommand
        {
            get
            {
                if (_CampaignCommand == null) Init();
                return _CampaignCommand;
            }
        }

        private Dictionary<string, ICommand> _ProductCommand;

        public Dictionary<string, ICommand> ProductCommand
        {
            get
            {
                if (_ProductCommand == null) Init();
                return _ProductCommand;
            }
        }
        private Dictionary<string, ICommand> _RolesCommand;

        public Dictionary<string, ICommand> RolesCommand
        {
            get
            {
                if (_RolesCommand == null) Init();
                return _RolesCommand;
            }
        }
        private Dictionary<string, ICommand> _TweetsCommand;

        public Dictionary<string, ICommand> TweetsCommand
        {
            get
            {
                if (_TweetsCommand == null) Init();
                return _TweetsCommand;
            }
        }

        private Dictionary<string, ICommand> _UserCommand;

        public Dictionary<string, ICommand> UserCommand
        {
            get
            {
                if (_UserCommand == null) Init();
                return _UserCommand;
            }
        }

        private Dictionary<string, ICommand> _UserMessage;

        public Dictionary<string, ICommand> UserMessage
        {
            get
            {
                if (_UserMessage == null) Init();
                return _UserMessage;
            }
        }


        private void Init()
        {

            try
            {
                MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "Command List Initialization!" });

                _CampaignCommand = new Dictionary<string, ICommand>()
                {
                    { "GET", new InsertCampaign(MainManager.Instance.Logger)},
                    { "POST", new InsertCampaign(MainManager.Instance.Logger)},
                    { "LOAD", new InsertCampaign(MainManager.Instance.Logger)},
                };

                _ProductCommand = new Dictionary<string, ICommand>()
                {
                    { "BUSINESS", new LoadByBusiness(MainManager.Instance.Logger)},
                    { "GET", new GetProdDonated(MainManager.Instance.Logger)},
                    { "SUPPLIED", new ProductsSupplied(MainManager.Instance.Logger)},
                    { "NOTSUPPLIED", new ProductsNotSupplied(MainManager.Instance.Logger)},
                    { "BUY", new BuyAProduct(MainManager.Instance.Logger)},
                    { "POST", new DonateProduct(MainManager.Instance.Logger)},
                    { "SUPPLY", new SupplyProducts(MainManager.Instance.Logger)},
                    { "GETALL", new GetAll(MainManager.Instance.Logger)},
                };

                _RolesCommand = new Dictionary<string, ICommand>()
                {
                    { "GET", new GetUser(MainManager.Instance.Logger)},
                };

                _ProductCommand = new Dictionary<string, ICommand>()
                {
                    { "GETALL", new GetAllTweets(MainManager.Instance.Logger)},
                    { "GET", new GetTweetsByUser(MainManager.Instance.Logger)},
                    { "POST", new PostATweet(MainManager.Instance.Logger)},
                };

                _UserCommand = new Dictionary<string, ICommand>()
                {
                    { "POST", new AddUser(MainManager.Instance.Logger)},
                    { "BALANCE", new GetBalance(MainManager.Instance.Logger)},
                    { "GET", new GetUsers(MainManager.Instance.Logger)},
                    { "CHECK", new IsExists(MainManager.Instance.Logger)},
                };

                _UserMessage = new Dictionary<string, ICommand>()
                {
                    { "BUSINESS", new PostMessage(MainManager.Instance.Logger)},
                };
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.AddToLog(new LogItem { exception = ex });
            }

        }

    }
}
