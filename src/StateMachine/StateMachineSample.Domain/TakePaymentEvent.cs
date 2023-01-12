using System.Runtime.CompilerServices;
using MassTransit;

namespace StateMachineSample.Events;

public class TakePaymentEvent
{
    public Guid OrderId { get; set; }
    
    [ModuleInitializer]
    internal static  void Init()
    {
        GlobalTopology.Send.UseCorrelationId<TakePaymentEvent>(x=> x.OrderId);
    }
}