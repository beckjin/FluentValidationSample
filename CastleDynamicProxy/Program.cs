using System;
using System.Collections.Generic;
using CastleDynamicProxy.Extensions;
using CastleDynamicProxy.Infrastructure;
using CastleDynamicProxy.Requests;
using CastleDynamicProxy.Services;

namespace CastleDynamicProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            DependencyResolver.Initialize();

            ValidatorExtension.Initialize(typeof(Program).Assembly);

            var demoService = DependencyResolver.For<IDemoService>();

            var request = new TestRequest
            {
                Name = "beck",
                Ids = new List<string> { "id" }
            };

            var result = demoService.TestAsync(request).GetAwaiter().GetResult();

            Console.WriteLine(result.Data);

            Console.ReadLine();
        }
    }
}
