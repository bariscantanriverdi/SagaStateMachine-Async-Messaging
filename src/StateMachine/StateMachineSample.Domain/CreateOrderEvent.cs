using System.Runtime.CompilerServices;
using MassTransit;

namespace StateMachineSample.Events;

public class CreateOrderEvent
{
    public Guid OrderId { get; set; }
    
    [ModuleInitializer]
    internal static  void Init()
    {
        GlobalTopology.Send.UseCorrelationId<CreateOrderEvent>(x=> x.OrderId);
    }
}