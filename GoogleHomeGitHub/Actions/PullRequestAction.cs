using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleHomeGitHub.Actions
{
    internal class PullRequestAction : BaseAction
    {
        public PullRequestAction(Models.DialogFlowResponseModelV1 model) : base(model) { }
        public override async Task<string> GetResponseTextAsync()
        {
            IReadOnlyList<PullRequest> prs = await Client.PullRequest.GetAllForRepository(Account, Repository);
            return $"プルリクは{string.Join("<break time=\"500ms\"/>", prs.Select(pr => $"<p>{pr.Title}</p>"))}です";
        }
        
    }
}
