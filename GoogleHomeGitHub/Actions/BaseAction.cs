using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GoogleHomeGitHub.Actions
{
    class BaseAction
    {
        protected Models.DialogFlowResponseModelV1 Model { get; private set; }
        protected GitHubClient Client { get; private set; }
        protected string Repository { get; private set; }
        protected string Account { get; private set; }

        public BaseAction(Models.DialogFlowResponseModelV1 model)
        {
            Model = model;
            Repository = ConfigurationManager.AppSettings["Repository"];
            Account = ConfigurationManager.AppSettings["Account"];
            Client = new GitHubClient(new ProductHeaderValue(Repository));
        }

        public virtual async Task<string> GetResponseTextAsync()
        {
            return await new Task<string>(()=>"");
        }
    }
}
