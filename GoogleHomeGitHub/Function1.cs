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

            // DialogFlow����n�����Json���擾
            var model = await req.Content.ReadAsAsync<Models.DialogFlowResponseModelV1>();

            // �A�N�V�����N���X�𐶐�
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

            // GoogleHome�ɒ��点�����������擾
            var say = await action.GetResponseTextAsync();

            var result = req.CreateResponse(HttpStatusCode.OK, new
            {
                // Google Home �ɒ��点����������n��
                speech = $"<speak>{say}</speak>",
                // Google Assistant �̃`���b�g��ʏ�ɏo������������
                displayText = $"�u{say}�v"
            });

            result.Headers.Add("ContentType", "application/json");
            return result;
        }
    }
}
