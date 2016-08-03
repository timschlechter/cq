using System;
using Owin;

namespace CQ.Integration.Owin
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseCQ(this IAppBuilder app, Action<OwinConfig> config)
        {
            config(new OwinConfig(app));

            return app;
        }
    }
}