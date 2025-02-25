using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

[Route("api/notification")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationController(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpGet]
    public IActionResult Test()
    {
        return Ok("Notification API is running!");
    }
    
    [HttpPost]
    public async Task<IActionResult> SendNotification([FromBody] Dictionary<string, string> request)
    {
        if (!request.ContainsKey("message") || string.IsNullOrEmpty(request["message"]))
        {
            return BadRequest("Message cannot be empty.");
        }

        await _hubContext.Clients.All.SendAsync("ReceiveNotification", request["message"]);
        return Ok(new { Status = "Message Sent", Message = request["message"] });
    }
}