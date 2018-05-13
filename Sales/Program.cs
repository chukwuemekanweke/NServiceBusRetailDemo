using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Sales
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Sales";
            AsyncMain().GetAwaiter().GetResult();
        }


        static async Task AsyncMain()
        {
            var endpointConfiguration = new EndpointConfiguration("Sales");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }

    }
}
