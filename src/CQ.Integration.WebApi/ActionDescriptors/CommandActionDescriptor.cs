using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace CQ.Integration.WebApi.ActionDescriptors
{
    internal class CommandActionDescriptor : HttpActionDescriptor
    {
        private readonly Type _commandType;

        public CommandActionDescriptor(HttpConfiguration httpConfiguration, Type commandType)
        {
            _commandType = commandType;
            ControllerDescriptor = new HttpControllerDescriptor(httpConfiguration, "Commands", typeof(object));
        }

        public override string ActionName => _commandType.Name;
        public override Type ReturnType => null;

        public override Collection<HttpParameterDescriptor> GetParameters()
        {
            throw new NotImplementedException();
        }

        public override Task<object> ExecuteAsync(HttpControllerContext controllerContext, IDictionary<string, object> arguments, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}