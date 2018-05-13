using System;
using System.Threading.Tasks;
using Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;

namespace ClientUI
{
    class Program
    {
        static ILog log = LogManager.GetLogger<Program>();
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        #region first demo
        //static async Task AsyncMain()
        //{

        //    Console.Title = "CLientUI";//Give the console app a name

        //    //EndpointConfiguration ius where we define all the settings that determine how our endpoint
        //    //will operate. ClientUI is the endpoint name
        //    var endpointConfiguration = new EndpointConfiguration("ClientUI");

        //    //This setting defines the transport that NServiceBus will use to send and receive messages.
        //    //We are using the LearningTransport, which is bundled within the NServiceBus core library
        //    //as a starter transport to learn how to use NServiceBus without any additional dependencies. All other transports are provided using different NuGet packages.
        //    var transport = endpointConfiguration.UseTransport<LearningTransport>();

        //    var endPointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
        //    Console.WriteLine("Please Enter To Exit...");
        //    Console.ReadLine();

        //    //In this tutorial we will always use.ConfigureAwait(false) when awaiting tasks,
        //    //in order to avoid capturing and restoring the SynchronizationContext.
        //    await endPointInstance.Stop().ConfigureAwait(false);
        //}
        #endregion

        #region second demo
        static async Task AsyncMain()
        {

            Console.Title = "CLientUI";//Give the console app 
            //EndpointConfiguration ius where we define all the settings that determine how our endpoint
            //will operate. ClientUI is the endpoint name
            var endpointConfiguration = new EndpointConfiguration("ClientUI");

            //This setting defines the transport that NServiceBus will use to send and receive messages.
            //We are using the LearningTransport, which is bundled within the NServiceBus core library
            //as a starter transport to learn how to use NServiceBus without any additional dependencies. All other transports are provided using different NuGet packages.
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");
            var endPointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            await RunLoop(endPointInstance).ConfigureAwait(false);
            await endPointInstance.Stop().ConfigureAwait(false);

            //In this tutorial we will always use.ConfigureAwait(false) when awaiting tasks,
            //in order to avoid capturing and restoring the SynchronizationContext.
            await endPointInstance.Stop().ConfigureAwait(false);
        }

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                log.Info("Press 'P' to place an order, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        // Instantiate the command
                        var command = new PlaceOrder
                        {
                            OrderId = Guid.NewGuid().ToString()
                        };

                        // Send the command to the local endpoint
                        log.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");
                        await endpointInstance.Send(command).ConfigureAwait(false);

                        break;

                    case ConsoleKey.Q:
                        return;

                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }

        #endregion

    }
}
