using MassTransit;
using StateMachineSample.API.Consumers.Base;
using StateMachineSample.Events;

namespace StateMachineSample.API.Consumers;

public class TakePaymentFaultEventConsumer : ConsumerBase<Fault<TakePaymentEvent>>
{
    protected override Task ConsumeInternal(ConsumeContext<Fault<TakePaymentEvent>> context)
    {
        Console.WriteLine("Payment Taken Faulted");

        return Task.CompletedTask;
        
    }
}