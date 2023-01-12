using MassTransit;
using StateMachineSample.API.Consumers;
using StateMachineSample.API.Settings;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var messageBrokerQueueSettings = builder.Configuration.GetSection("MessageBroker:QueueSettings").Get<MessageBrokerQueueSettings>(); 

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context,cfg) =>
    {
        cfg.Host(messageBrokerQueueSettings.HostName, messageBrokerQueueSettings.VirtualHost, h => {
            h.Username(messageBrokerQueueSettings.UserName);
            h.Password(messageBrokerQueueSettings.Password);
        });

        cfg.ConfigureEndpoints(context);
    });

    x.AddConsumer<OrderProcessInitializationEventConsumer>();
    x.AddRequestClient<OrderProcessInitializationEventConsumer>();
    
    x.AddConsumer<OrderProcessInitializationFaultEventConsumer>();
    x.AddRequestClient<OrderProcessInitializationFaultEventConsumer>();
    
    x.AddConsumer<CheckProductStockEventConsumer>();
    x.AddRequestClient<CheckProductStockEventConsumer>();
    
    x.AddConsumer<CheckProductStockFaultEventConsumer>();
    x.AddRequestClient<CheckProductStockFaultEventConsumer>();

    x.AddConsumer<TakePaymentEventConsumer>();
    x.AddRequestClient<TakePaymentEventConsumer>();
    
    x.AddConsumer<TakePaymentFaultEventConsumer>();
    x.AddRequestClient<TakePaymentFaultEventConsumer>();
    
    x.AddConsumer<CreateOrderEventConsumer>();
    x.AddRequestClient<CreateOrderEventConsumer>();
    
    x.AddConsumer<CreateOrderFaultEventConsumer>();
    x.AddRequestClient<CreateOrderFaultEventConsumer>();
    
    x.AddConsumer<OrderProcessFailedEventConsumer>();
    x.AddRequestClient<OrderProcessFailedEventConsumer>();


});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();