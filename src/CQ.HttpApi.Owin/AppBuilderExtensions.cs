using Owin;

namespace CQ.HttpApi.Owin
{
    public static class AppBuilderExtensions
    {
        public static CQAppBuilderDecorator UseCQ(this IAppBuilder app, HttpApiSettings settings = null)
        {
            return new CQAppBuilderDecorator(app, settings);
        }
    }
}