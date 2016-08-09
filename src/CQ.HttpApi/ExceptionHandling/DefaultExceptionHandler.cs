using System;

namespace CQ.HttpApi.ExceptionHandling
{
    public class DefaultExceptionHandler : IExceptionHandler
    {
        public void Handle(Exception ex)
        {
            throw ex;
        }
    }
}