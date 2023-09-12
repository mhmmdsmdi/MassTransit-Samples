using MassTransit;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomMass();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/publish", async (IBus bus) =>
    {
        await bus.Publish(new TestMessage("This Message Is Published " + Counter.Count));

        Counter.Count++;
    })
    .WithOpenApi();

app.MapGet("/Order", async (IBus bus) =>
    {
        await bus.Publish(new OrderCreated(Counter.Count));
    })
    .WithOpenApi();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

public static class Counter
{
    public static int Count = 1;
}