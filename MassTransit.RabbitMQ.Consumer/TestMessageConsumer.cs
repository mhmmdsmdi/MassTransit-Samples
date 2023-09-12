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

public class OrderCreatedConsumer : IConsumer<OrderCreated>
{
    public Task Consume(ConsumeContext<OrderCreated> context)
    {
        Console.WriteLine(context.Message.OrderId);
        return Task.CompletedTask;
    }
}