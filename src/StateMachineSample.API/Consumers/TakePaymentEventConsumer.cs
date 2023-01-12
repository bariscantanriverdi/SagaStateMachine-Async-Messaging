using MassTransit;
using StateMachineSample.API.Consumers.Base;
using StateMachineSample.Events;
using StateMachineSample.Events.Responses;

namespace StateMachineSample.API.Consumers;

public class TakePaymentEventConsumer : ConsumerBase<TakePaymentEvent>
{
    protected override Task ConsumeInternal(ConsumeContext<TakePaymentEvent> context)
    {
        Console.WriteLine("Payment Taken");

        context.RespondAsync(new TakePaymentEventDto()
        {
            OrderId = context.Message.OrderId
        });
        
        return Task.CompletedTask;
    }
}