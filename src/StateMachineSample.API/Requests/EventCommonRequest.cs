namespace StateMachineSample.API.Requests;

public class EventCommonRequest
{
    public Guid OrderId { get; set; }
    
    public bool IsRequestResponsePattern { get; set; }
}