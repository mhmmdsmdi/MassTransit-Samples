using System.Reflection;
using MassTransit;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(Assembly.GetEntryAssembly());

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

public class Worker : BackgroundService
{
    private readonly IBus _bus;
    public static int Count = 1;

    public Worker(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _bus.Publish(new TestMessage("This Message Is Published " + Count), stoppingToken);
            Count++;

            await Task.Delay(200, stoppingToken);
        }
    }
}