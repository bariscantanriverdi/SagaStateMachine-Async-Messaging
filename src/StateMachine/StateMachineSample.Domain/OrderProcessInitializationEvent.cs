using System.Runtime.CompilerServices;
using MassTransit;

namespace StateMachineSample.Events;

public class OrderProcessInitializationEvent
{
    public Guid OrderId { get; set; }
    
    [ModuleInitializer]
    internal static  void Init()
    {
        GlobalTopology.Send.UseCorrelationId<OrderProcessInitializationEvent>(x=> x.OrderId);
    }

}