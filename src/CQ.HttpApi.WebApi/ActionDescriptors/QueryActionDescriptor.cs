using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace CQ.HttpApi.WebApi.ActionDescriptors
{
    internal class QueryActionDescriptor : HttpActionDescriptor
    {
        private readonly Type _queryType;

        public QueryActionDescriptor(HttpConfiguration httpConfiguration, Type queryType)
        {
            _queryType = queryType;
            ControllerDescriptor = new HttpControllerDescriptor(httpConfiguration, "Queries", typeof (object));
        }

        public override string ActionName => _queryType.Name;
        public override Type ReturnType => _queryType.GetResultType();

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