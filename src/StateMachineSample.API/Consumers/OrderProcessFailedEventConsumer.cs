using MassTransit;
using StateMachineSample.API.Consumers.Base;
using StateMachineSample.Events;

namespace StateMachineSample.API.Consumers;

public class OrderProcessFailedEventConsumer : ConsumerBase<OrderProcessFailedEvent>
{
    protected override Task ConsumeInternal(ConsumeContext<OrderProcessFailedEvent> context)
    {
        Console.WriteLine("Order Process Failed !");

        return Task.CompletedTask;
    }
}