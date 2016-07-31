namespace CQ.HttpApi.Owin.Swagger
{
    public static class AppBuilderExtensions
    {
        public static CqSwaggerAppBuilderDecorator EnableSwagger(this CQAppBuilderDecorator app)
        {
            return new CqSwaggerAppBuilderDecorator(app);
        }
    }
}