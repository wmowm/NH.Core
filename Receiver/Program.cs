using Common;
using MassTransit;
using System;

namespace Receiver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "MassTransit Server";

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost:15672/EDCVHOST"), hst =>
                {
                    hst.Username("tibos");
                    hst.Password("123456");
                });

                cfg.ReceiveEndpoint(host, "Qka.MassTransitTest", e =>
                {
                    e.Consumer<TestConsumerClient>();
                    e.Consumer<TestConsumerAgent>();
                });
            });

            bus.Start();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            bus.Stop();
        }
    }
}
