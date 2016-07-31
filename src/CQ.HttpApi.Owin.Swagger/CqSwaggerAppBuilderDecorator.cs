using Owin;

namespace CQ.HttpApi.Owin.Swagger
{
    public class CqSwaggerAppBuilderDecorator : CQAppBuilderDecorator
    {
        public CqSwaggerAppBuilderDecorator(CQAppBuilderDecorator decoratedApp) : base(decoratedApp, decoratedApp.Settings)
        {
            decoratedApp.Use(async (context, next) =>
            {
                await next();
            });
        }
    }
}