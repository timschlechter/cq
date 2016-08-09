using System;

namespace CQ.HttpApi.ExceptionHandling
{
    public interface IExceptionHandler
    {
        void Handle(Exception ex);
    }
}