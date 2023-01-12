using MassTransit;
using Microsoft.AspNetCore.Mvc;
using StateMachineSample.API.Requests;
using StateMachineSample.Events;
using StateMachineSample.Events.Responses;

namespace StateMachineSample.API.Controllers;

public class EventsController
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IRequestClient<OrderProcessInitializationEvent> _orderProcessInitializationEventRequestClient;
    private readonly IRequestClient<CheckProductStockEvent> _checkProductStockEventRequestClient;
    private readonly IRequestClient<TakePaymentEvent> _takePaymentEventRequestClient;
    private readonly IRequestClient<CreateOrderEvent> _createOrderEventRequestClient;
        
    public EventsController(
        IPublishEndpoint publishEndpoint,
        IRequestClient<OrderProcessInitializationEvent> orderProcessInitializationEventRequestClient,
        IRequestClient<CheckProductStockEvent> checkProductStockEventRequestClient,
        IRequestClient<TakePaymentEvent> takePaymentEventRequestClient,
        IRequestClient<CreateOrderEvent> createOrderEventRequestClient)
    {
        _publishEndpoint = publishEndpoint;
        _orderProcessInitializationEventRequestClient = orderProcessInitializationEventRequestClient;
        _checkProductStockEventRequestClient = checkProductStockEventRequestClient;
        _takePaymentEventRequestClient = takePaymentEventRequestClient;
        _createOrderEventRequestClient = createOrderEventRequestClient;
    }

    [HttpPost("initialize/order")]
    public async Task<IActionResult> OrderProcessInitializedEvent([FromBody] EventCommonRequest request)
    {
        if (!request.IsRequestResponsePattern)
        {
            await _publishEndpoint.Publish<OrderProcessInitializationEvent>(new {request.OrderId});
            return new NoContentResult();
        }

        var result = await _orderProcessInitializationEventRequestClient.GetResponse<OrderProcessInitiazationDto>(new {request.OrderId});

        return new NoContentResult();
    }

    [HttpPost("check/product")]
    public async Task<IActionResult> CheckProductStockEvent([FromBody] EventCommonRequest request)
    {
        if (!request.IsRequestResponsePattern)
        {
            await _publishEndpoint.Publish<CheckProductStockEvent>(new {request.OrderId});
            return new NoContentResult();
        }
        
        var result = await _checkProductStockEventRequestClient.GetResponse<CheckProductStockEventDto>(new {request.OrderId});

        return new NoContentResult();
    }

    [HttpPost("take/payment")]
    public async Task<IActionResult> TakePaymentEvent([FromBody] EventCommonRequest request)
    {
        if (!request.IsRequestResponsePattern)
        {
            await _publishEndpoint.Publish<TakePaymentEvent>(new {request.OrderId});
        }

        var result = await _takePaymentEventRequestClient.GetResponse<TakePaymentEventDto>(new {request.OrderId});

        return new NoContentResult();
    }

    [HttpPost("create/order")]
    public async Task<IActionResult> CreateOrderEvent([FromBody] EventCommonRequest request)
    {
        if (!request.IsRequestResponsePattern)
        {
            await _publishEndpoint.Publish<CreateOrderEvent>(new {request.OrderId});
        }

        var result = await _createOrderEventRequestClient.GetResponse<CreateOrderEventDto>(new {request.OrderId});

        return new NoContentResult();
    }
}