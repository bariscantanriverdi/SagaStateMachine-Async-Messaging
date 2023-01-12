using MassTransit;
using StateMachineSample.API.Consumers.Base;
using StateMachineSample.Events;

namespace StateMachineSample.API.Consumers;

public class CreateOrderFaultEventConsumer : ConsumerBase<Fault<CreateOrderEvent>>
{
    protected override Task ConsumeInternal(ConsumeContext<Fault<CreateOrderEvent>> context)
    {
        Console.WriteLine("Order Create Faulted.");

        return Task.CompletedTask;
    }
}