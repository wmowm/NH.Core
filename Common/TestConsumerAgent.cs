using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class TestConsumerAgent : IConsumer<Agent>
    {
        public async Task Consume(ConsumeContext<Agent> context)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Out.WriteLineAsync($"Receive message: {context.Message.AgentCode}, {context.Message.AgentName}, {context.Message.AgentRole}");
            Console.ResetColor();
        }
    }
}
