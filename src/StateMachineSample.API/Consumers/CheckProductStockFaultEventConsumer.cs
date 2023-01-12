using MassTransit;
using StateMachineSample.API.Consumers.Base;
using StateMachineSample.Events;

namespace StateMachineSample.API.Consumers;

public class CheckProductStockFaultEventConsumer : ConsumerBase<Fault<CheckProductStockEvent>>
{
    protected override Task ConsumeInternal(ConsumeContext<Fault<CheckProductStockEvent>> context)
    {
        Console.WriteLine("Product Stock check Faulted.");

        return Task.CompletedTask;
    }
}