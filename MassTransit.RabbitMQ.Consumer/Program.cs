using MassTransit;
using MassTransit.RabbitMQ.Consumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);

        cfg.ReceiveEndpoint("Data-Current", e =>
        {
            // configure any required middleware components next
            e.UseMessageRetry(r => r.Interval(5, 1000));

            // configure the consumer last
            e.Consumer<TestMessageConsumer>();
        });
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();