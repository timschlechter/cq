using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQ.HttpApi;
using CQ.HttpApi.HttpStatusCodeResolving;
using CQ.HttpApi.JsonSerialization;

namespace CQ.Integration.WebApi.HttpMessageHandlers
{
    internal class QueryHttpMessageHandler : HttpMessageHandler
    {
        private readonly Func<object, object> _handleQuery;
        private readonly Type _queryType;
        private readonly IJsonSerializer _serializer;
        private readonly IHttpStatusCodeResolver _httpStatusCodeResolver;

        public QueryHttpMessageHandler(Type queryType, Func<object, object> handleQuery, IJsonSerializer serializer, IHttpStatusCodeResolver httpStatusCodeResolver)
        {
            _queryType = queryType;
            _handleQuery = handleQuery;
            _serializer = serializer;
            _httpStatusCodeResolver = httpStatusCodeResolver;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            try
            {
                request.GetDependencyScope();

                var expanded = ObjectHelper.ExpandQueryString(request.RequestUri.Query);
                var query = _serializer.MakeTyped(expanded, _queryType);
                var result = _handleQuery(query);

                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(_serializer.Serialize(result), Encoding.UTF8, "application/json")
                };
            }
            catch (Exception ex)
            {
                var statusCode = _httpStatusCodeResolver.Resolve(ex);

                response =  new HttpResponseMessage(HttpStatusCode.OK)
                {
                    StatusCode = statusCode,
                    Content = new StringContent(_serializer.Serialize(new Error {Code = statusCode.ToString(), Message = ex.Message}), Encoding.UTF8, "application/json")
                };
            }

            return Task.FromResult(response);
        }
    }
}