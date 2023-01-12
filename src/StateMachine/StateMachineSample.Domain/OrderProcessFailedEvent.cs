using System.Runtime.CompilerServices;
using MassTransit;

namespace StateMachineSample.Events;

public class OrderProcessFailedEvent
{
    public Guid OrderId { get; set; }
    
    [ModuleInitializer]
    internal static  void Init()
    {
        GlobalTopology.Send.UseCorrelationId<OrderProcessFailedEvent>(x=> x.OrderId);
    }
}