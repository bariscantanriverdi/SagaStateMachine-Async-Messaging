using MassTransit;
using StateMachineSample.API.Consumers.Base;
using StateMachineSample.Events;
using StateMachineSample.Events.Responses;

namespace StateMachineSample.API.Consumers;

public class CheckProductStockEventConsumer : ConsumerBase<CheckProductStockEvent>
{
    protected override Task ConsumeInternal(ConsumeContext<CheckProductStockEvent> context)
    {
        Console.WriteLine("Product Stock checked.");


        context.RespondAsync(new CheckProductStockEventDto
        {
            OrderId = context.Message.OrderId
        });
        
        return Task.CompletedTask;
    }
}