

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/user/[controller]")]
public class ChatController (IChatService chatService) : ControllerBase
{
    
}