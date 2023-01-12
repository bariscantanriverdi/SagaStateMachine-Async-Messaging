using MassTransit;
using StateMachineSample.API.Consumers.Base;
using StateMachineSample.Events;
using StateMachineSample.Events.Responses;

namespace StateMachineSample.API.Consumers;

public class CreateOrderEventConsumer : ConsumerBase<CreateOrderEvent>
{
    protected override Task ConsumeInternal(ConsumeContext<CreateOrderEvent> context)
    {
        Console.WriteLine("Order Created");

        context.RespondAsync(new CreateOrderEventDto
        {
            OrderId = context.Message.OrderId
        });
        
        //to see what's going on when we got an error.
        throw new Exception("Transition Fault State");

        return Task.CompletedTask;
    }
}