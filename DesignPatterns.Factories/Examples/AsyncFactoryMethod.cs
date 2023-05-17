using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Factories.Examples
{
    public class Example
    {
        private Example()
        {
            // Something
        }

        private async Task<Example> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        public static async Task<Example> CreateAsync()
        {
            var result = new Example();
            return await result.InitAsync();
        }
    }

    public class AsyncFactoryMethod
    {
        public static async Task Start(string[] args)
        {
            Example ex = await Example.CreateAsync();
        }
    }
}
