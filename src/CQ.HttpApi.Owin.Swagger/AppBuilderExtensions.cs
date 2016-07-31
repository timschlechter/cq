namespace CQ.HttpApi.Owin.Swagger
{
    public static class AppBuilderExtensions
    {
        public static CqSwaggerAppBuilderDecorator EnableSwagger(this CQAppBuilderDecorator app, string routeTemplate)
        {
            return new CqSwaggerAppBuilderDecorator(app, routeTemplate);
        }
    }
}