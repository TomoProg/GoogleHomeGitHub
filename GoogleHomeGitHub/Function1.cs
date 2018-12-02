using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace GoogleHomeGitHub
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req
            , TraceWriter log
            )
        {
            log.Info("C# HTTP trigger function processed a request.");

            // DialogFlowから渡されるJsonを取得
            var model = await req.Content.ReadAsAsync<Models.DialogFlowResponseModelV1>();

            // アクションクラスを生成
            var factory = new Actions.ActionFactory();
            Actions.BaseAction action;
            try
            {
                log.Info($"intentname:{model.Result.Metadata.IntentName}");
                action = factory.Create(model, model.Result.Metadata.IntentName);
            }
            catch (Exception ex)
            {
                log.Error("Action Create Failed...", ex);
                action = new Actions.FailAction(model);
            }

            // GoogleHomeに喋らせたい文言を取得
            var say = await action.GetResponseTextAsync();

            var result = req.CreateResponse(HttpStatusCode.OK, new
            {
                // Google Home に喋らせたい文言を渡す
                speech = $"<speak>{say}</speak>",
                // Google Assistant のチャット画面上に出したい文字列
                displayText = $"「{say}」"
            });

            result.Headers.Add("ContentType", "application/json");
            return result;
        }
    }
}
