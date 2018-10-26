using MassTransit;
using System;
using System.Threading.Tasks;

namespace Common
{
    public class TestConsumerClient : IConsumer<Client>
    {
        public async Task Consume(ConsumeContext<Client> context)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Out.WriteLineAsync($"Receive message: {context.Message.Id}, {context.Message.Name}, {context.Message.Birthdate.ToString()}");
            Console.ResetColor();
        }
    }
}
