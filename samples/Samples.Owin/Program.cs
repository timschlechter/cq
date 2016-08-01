using System;
using Microsoft.Owin.Hosting;

namespace Sample.Owin.SelfHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var rootUrl = "http://localhost:12345";

            using (WebApp.Start<Startup>(rootUrl))
            {
                Console.WriteLine("OWIN host running at {0}. Press ENTER to shutdown", rootUrl);
                Console.ReadLine();
            }
        }
    }
}