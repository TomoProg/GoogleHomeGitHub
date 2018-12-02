using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleHomeGitHub.Actions
{
    class FailAction : BaseAction
    {
        public FailAction(Models.DialogFlowResponseModelV1 model) : base(model) { }
        public override async Task<string> GetResponseTextAsync()
        {
            return await base.GetResponseTextAsync();
        }
    }
}
