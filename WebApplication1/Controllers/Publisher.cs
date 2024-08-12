using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;


[ApiController]
[Route("[controller]")]
public class Publisher : ControllerBase
{
    public readonly IPublishEndpoint publishEndpoint;

    public Publisher(IPublishEndpoint publishEndpoint)
    {
        this.publishEndpoint = publishEndpoint;
    }
    
    [HttpPost]
    public async Task<IActionResult> Notify()
    {
        await publishEndpoint.Publish<ExampleMessage>(  new ExampleMessage(){
            Text = "Hello from consumer",
        });

        return Ok();
    }
}