using MassTransit;
using StateMachineSample.API.Consumers.Base;
using StateMachineSample.Events;
using StateMachineSample.Events.Responses;

namespace StateMachineSample.API.Consumers;

public class OrderProcessInitializationEventConsumer : ConsumerBase<OrderProcessInitializationEvent>
{
    protected override Task ConsumeInternal(ConsumeContext<OrderProcessInitializationEvent> context)
    {
        Console.WriteLine("Order process Initialized");

        context.RespondAsync(new OrderProcessInitiazationDto
        {
            OrderId = context.Message.OrderId
        });
        
        return Task.CompletedTask;
    }
}