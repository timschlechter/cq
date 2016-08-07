using System;
using System.Linq;
using System.Threading.Tasks;
using CQ.HttpApi.JsonSerialization;
using CQ.HttpApi.RouteResolving;
using RestSharp;

namespace CQ.HttpApi.Client
{
    public class HttpApiClient
    {
        private readonly HttpApiConfig _config;
        private readonly string _rootUrl;
        private readonly Lazy<RestClient> _restClient;

        public HttpApiClient(string rootUrl, Action<HttpApiConfig> configure = null)
        {
            _rootUrl = rootUrl;

            _config = new HttpApiConfig();

            configure?.Invoke(_config);

            _restClient = new Lazy<RestClient>(() =>
            {
                var client = new RestClient(_rootUrl);
                var serializer = new RestSharpJsonSerializer(_config.JsonSerializer);
                client.AddHandler("application/json", serializer);
                client.AddHandler("text/json", serializer);
                client.AddHandler("text/x-json", serializer);
                client.AddHandler("text/javascript", serializer);
                client.AddHandler("*+json", serializer);

                return client;
            });
        }

        protected virtual RestClient RestClient => _restClient.Value;

        public virtual Task ExecuteCommand<TCommand>(TCommand command)
        {
            var path = _config.CommandRouteResolver.ResolvePath(command);
            var req = new RestRequest(path, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            req.AddParameter(new Parameter
            {
                Name = "application/json",
                Value = _config.JsonSerializer.Serialize(command),
                Type = ParameterType.RequestBody
            });

            return ExecuteRequest<object>(req).ContinueWith(task => { });
        }

        public virtual Task<TResult> ExecuteQuery<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var path = _config.QueryRouteResolver.ResolvePath(query);
            var req = new RestRequest(path, Method.GET);

            var parameters = ObjectHelper.Flatten(query)
                .SelectMany(group =>
                    group.Select(value => new Parameter
                    {
                        Name = group.Key,
                        Value = value,
                        Type = ParameterType.QueryString
                    }));

            req.Parameters.AddRange(parameters);

            return ExecuteRequest<TResult>(req);
        }

        protected virtual Task<TResult> ExecuteRequest<TResult>(IRestRequest req)
        {
            return RestClient.ExecuteTaskAsync<TResult>(req).ContinueWith(task =>
            {
                var response = task.Result;

                var status = (int) response.StatusCode;
                if (status < 200 && status >= 400)
                {
                    throw new HttpApiException(response.StatusDescription) {StatusCode = response.StatusCode};
                }

                return response.Data;
            });
        }
    }
}