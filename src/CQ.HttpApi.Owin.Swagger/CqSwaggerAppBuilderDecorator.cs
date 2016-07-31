using System;
using System.IO;
using System.Linq;
using System.Text;
using CQ.HttpApi.Owin.Swagger.Model;
using Owin;

namespace CQ.HttpApi.Owin.Swagger
{
    public class CqSwaggerAppBuilderDecorator : CQAppBuilderDecorator
    {
        public CqSwaggerAppBuilderDecorator(CQAppBuilderDecorator decoratedApp, string routeTemplate) : base(decoratedApp, decoratedApp.Settings)
        {
            decoratedApp.Use(async (context, next) =>
            {
                var path = context.Request.Path.HasValue ? context.Request.Path.Value : null;
                if (string.Equals(path, routeTemplate, StringComparison.InvariantCultureIgnoreCase))
                {
                    var document = new RootDocument
                    {
                        paths = decoratedApp.CommandTypes
                            .ToDictionary(
                                type => decoratedApp.Settings.CommandRouteResolver.ResolveRoutePath(type),
                                type => new PathItem
                                {
                                    post = new Operation()
                                })
                    };

                    using (var writer = new StreamWriter(context.Response.Body, Encoding.UTF8, 1024, true))
                    {
                        context.Response.ContentType = "application/json";
                        writer.Write(SimpleJson.SimpleJson.SerializeObject(document));
                    }
                }
                await next();
            });
        }
    }
}