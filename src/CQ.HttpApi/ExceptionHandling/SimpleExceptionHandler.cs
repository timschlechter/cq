using System;

namespace CQ.HttpApi.ExceptionHandling
{
    public class SimpleExceptionHandler : IExceptionHandler
    {
        public void Handle(Exception ex)
        {
            throw ex;
        }
    }
}