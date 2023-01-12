using MassTransit;
using StateMachineSample.API.Consumers.Base;
using StateMachineSample.Events;

namespace StateMachineSample.API.Consumers;

public class OrderProcessInitializationFaultEventConsumer : ConsumerBase<Fault<OrderProcessInitializationEvent>>
{
    protected override Task ConsumeInternal(ConsumeContext<Fault<OrderProcessInitializationEvent>> context)
    {
        Console.WriteLine("Order Process Intialization faulted.");

        return Task.CompletedTask;
    }
}