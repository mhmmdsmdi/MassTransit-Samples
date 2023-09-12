using Shared;

namespace MassTransit.RabbitMQ.Consumer;

public class TestMessageConsumer : IConsumer<TestMessage>
{
    public Task Consume(ConsumeContext<TestMessage> context)
    {
        Console.WriteLine(context.Message.Message);
        return Task.CompletedTask;
    }
}