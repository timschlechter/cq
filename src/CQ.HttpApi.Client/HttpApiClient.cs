using System.Linq;
using System.Threading.Tasks;
using CQ.HttpApi;
using CQ.HttpApi.JsonSerialization;
using CQ.HttpApi.RouteResolving;
using RestSharp;

namespace CQ.Client
{
    public class HttpApiClient
    {
        private readonly HttpApiConfig _config;
        private readonly string _rootUrl;

        public HttpApiClient(string rootUrl, HttpApiConfig config = null)
        {
            _rootUrl = rootUrl;

            _config = config ?? HttpApiConfig.Default;
            _config.JsonSerializer = _config.JsonSerializer ?? HttpApiConfig.Default.JsonSerializer;
            _config.CommandRouteResolver = _config.CommandRouteResolver ?? HttpApiConfig.Default.CommandRouteResolver;
            _config.QueryRouteResolver = _config.QueryRouteResolver ?? HttpApiConfig.Default.QueryRouteResolver;
            _config.CommandGroupKeyResolver = _config.CommandGroupKeyResolver ?? HttpApiConfig.Default.CommandGroupKeyResolver;
            _config.QueryGroupKeyResolver = _config.QueryGroupKeyResolver ?? HttpApiConfig.Default.QueryGroupKeyResolver;
        }

        public Task ExecuteCommand<TCommand>(TCommand command)
        {
            var path = _config.CommandRouteResolver.ResolvePath(command);
            var req = new RestRequest(path, Method.POST);

            req.AddJsonBody(command);

            return ExecuteRequest<object>(req)
                .ContinueWith(task => { });
        }

        public Task<TResult> ExecuteQuery<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
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
            var client = new RestClient(_rootUrl);

            return client.ExecuteTaskAsync(req).ContinueWith(task =>
            {
                var response = task.Result;

                var status = (int) response.StatusCode;
                if (status < 200 && status >= 400)
                {
                    throw new HttpApiException(response.StatusDescription) {StatusCode = response.StatusCode};
                }

                return _config.JsonSerializer.Deserialize<TResult>(response.Content);
            });
        }
    }
}