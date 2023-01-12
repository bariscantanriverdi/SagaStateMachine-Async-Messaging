using System.Runtime.CompilerServices;
using MassTransit;

namespace StateMachineSample.Events;

public class CheckProductStockEvent
{
    public Guid OrderId { get; set; }
    
    [ModuleInitializer]
    internal static  void Init()
    {
        GlobalTopology.Send.UseCorrelationId<CheckProductStockEvent>(x=> x.OrderId);
    }
    
    
}