using MassTransit;
using StateMachineSample.Events;

namespace StateMachineSample.StateMachine;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public OrderStateMachine()
    {
        #region EventsDefinitions

        Event(() => OrderProcessInitializationEvent);
        Event(() => OrderProcessInitializationFaultEvent,
            x => x.CorrelateById(context => context.InitiatorId ?? context.Message.Message.OrderId));

        Event(() => CheckProductStockEvent);
        Event(() => CheckProductStockFaultEvent,
            x => x.CorrelateById(context => context.InitiatorId ?? context.Message.Message.OrderId));

        Event(() => TakePaymentEvent);
        Event(() => TakePaymentEventFaultEvent,
            x => x.CorrelateById(context => context.InitiatorId ?? context.Message.Message.OrderId));

        Event(() => CreateOrderEvent);
        Event(() => CreateOrderFaultEvent,
            x => x.CorrelateById(context => context.InitiatorId ?? context.Message.Message.OrderId));

        Event(() => OrderProcessFailedEvent);

        #endregion


        InstanceState(x => x.CurrentState);

        #region Flow

        During(Initial,
            When(OrderProcessInitializationEvent)
                .Then(x => x.Saga.OrderStartDate = DateTime.Now)
                .TransitionTo(OrderProcessInitializedState));

        During(OrderProcessInitializedState,
            When(CheckProductStockEvent)
                .TransitionTo(CheckProductStockState));

        During(CheckProductStockState,
            When(TakePaymentEvent)
                .TransitionTo(TakePaymentState));

        During(TakePaymentState,
            When(CreateOrderEvent)
                .TransitionTo(CreateOrderState));

        #endregion


        #region Fault-Companse State

        DuringAny(When(CreateOrderFaultEvent)
            .TransitionTo(CreateOrderFaultedState)
            .Then(context => context.Publish<Fault<TakePaymentEvent>>(new {context.Message})));


        DuringAny(When(TakePaymentEventFaultEvent)
            .TransitionTo(TakePaymentFaultedState)
            .Then(context => context.Publish<Fault<CheckProductStockEvent>>(new {context.Message})));

        DuringAny(When(CheckProductStockFaultEvent)
            .TransitionTo(CheckProductStockFaultedState)
            .Then(context => context.Publish<Fault<OrderProcessInitializationEvent>>(new {context.Message})));

        DuringAny(When(OrderProcessInitializationFaultEvent)
            .TransitionTo(OrderProcessInitializedFaultedState)
            .Then(context => context.Publish<OrderProcessFailedEvent>(new {OrderId = context.Saga.CorrelationId})));

        DuringAny(When(OrderProcessFailedEvent)
            .TransitionTo(OrderProcessFailedState));

        #endregion
    }

    #region Events

    public Event<OrderProcessInitializationEvent> OrderProcessInitializationEvent { get; }
    public Event<Fault<OrderProcessInitializationEvent>> OrderProcessInitializationFaultEvent { get; }

    public Event<CheckProductStockEvent> CheckProductStockEvent { get; }
    public Event<Fault<CheckProductStockEvent>> CheckProductStockFaultEvent { get; }

    public Event<TakePaymentEvent> TakePaymentEvent { get; }
    public Event<Fault<TakePaymentEvent>> TakePaymentEventFaultEvent { get; }

    public Event<CreateOrderEvent> CreateOrderEvent { get; }
    public Event<Fault<CreateOrderEvent>> CreateOrderFaultEvent { get; }

    public Event<OrderProcessFailedEvent> OrderProcessFailedEvent { get; }

    #endregion


    #region States

    public State OrderProcessInitializedState { get; }
    public State OrderProcessInitializedFaultedState { get; }

    public State CheckProductStockState { get; }
    public State CheckProductStockFaultedState { get; }

    public State TakePaymentState { get; }
    public State TakePaymentFaultedState { get; }

    public State CreateOrderState { get; }
    public State CreateOrderFaultedState { get; }

    public State OrderProcessFailedState { get; }

    #endregion
}