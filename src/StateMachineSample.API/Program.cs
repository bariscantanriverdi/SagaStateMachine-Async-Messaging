using MassTransit;
using StateMachineSample.API.Consumers;
using StateMachineSample.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context,cfg) =>
    {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();