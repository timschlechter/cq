using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CQ.HttpApi;
using CQ.HttpApi.JsonSerialization;
using CQ.HttpApi.PathResolving;
using RestSharp;

namespace CQ.Client
{
    public class HttpApiClient
    {
        private readonly string _rootUrl;
        private readonly HttpApiSettings _settings;

        public HttpApiClient(string rootUrl, HttpApiSettings settings = null)
        {
            _rootUrl = rootUrl;
            _settings = settings ?? new HttpApiSettings();

            _settings.PathResolver = _settings.PathResolver ?? new SimplePathResolver();
            _settings.JsonSerializer = _settings.JsonSerializer ?? new SimpleJsonSerializer();
        }

        public Task ExecuteCommand<TCommand>(TCommand command)
        {
            var path = _settings.PathResolver.GetCommandPath(command);
            var req = new RestRequest(path)
            {
                Method = Method.POST
            };

            req.AddJsonBody(command);

            return ExecuteRequest<object>(req)
                .ContinueWith(task => { });
        }

        public Task<TResult> ExecuteQuery<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var path = _settings.PathResolver.GetQueryPath(query);
            var req = new RestRequest(path)
            {
                Method = Method.GET
            };

            var parameters = ToDictionary(query)
                .Select(kvp => new Parameter
                {
                    Name = kvp.Key,
                    Value = kvp.Value,
                    Type = ParameterType.QueryString
                }).ToList();

            req.Parameters.AddRange(parameters);

            return ExecuteRequest<TResult>(req);
        }

        public static IDictionary<string, object> ToDictionary(object value)
        {
            return value.GetType()
                .GetProperties()
                .Select(pi => new {pi.Name, Value = pi.GetValue(value, null)})
                .Union(
                    value.GetType()
                        .GetFields()
                        .Select(fi => new {fi.Name, Value = fi.GetValue(value)})
                )
                .ToDictionary(ks => ks.Name, vs => vs.Value);
        }

        protected virtual Task<TResult> ExecuteRequest<TResult>(IRestRequest req)
        {
            var client = new RestClient(_rootUrl);

            return client.ExecuteTaskAsync(req)
                .ContinueWith(task =>
                {
                    var response = task.Result;

                    var status = (int) response.StatusCode;
                    if (status < 200 && status >= 400)
                    {
                        throw new HttpApiException(response.StatusDescription) {StatusCode = response.StatusCode};
                    }

                    using (var stream = new MemoryStream())
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            writer.Write(response.Content);
                            writer.Flush();
                            stream.Position = 0;

                            return (TResult) _settings.JsonSerializer.Deserialize(stream, typeof (TResult));
                        }
                    }
                });
        }
    }

    public class CommandResult
    {
    }
}