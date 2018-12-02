using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleHomeGitHub.Actions
{
    class ActionFactory
    {
        public BaseAction Create(Models.DialogFlowResponseModelV1 model, string intent)
        {
            var type = Type.GetType($"GoogleHomeGitHub.Actions.{intent}Action");
            return Activator.CreateInstance(type, model) as BaseAction;
        }
    }
}
