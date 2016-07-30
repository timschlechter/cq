using Owin;

namespace CQ.HttpApi.Owin
{
    public static class AppBuilderExtensions
    {
        public static CQAppBuilder UseCQ(this IAppBuilder app, HttpApiSettings settings = null)
        {
            return new CQAppBuilder(app, settings);
        }
    }
}