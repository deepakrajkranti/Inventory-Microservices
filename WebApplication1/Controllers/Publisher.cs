using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

namespace WebApplication1.Controllers;


[ApiController]
[Route("[controller]")]
public class Publisher : ControllerBase
{
    public readonly IPublishEndpoint publishEndpoint;
    private readonly DataContext _data;

    public Publisher(IPublishEndpoint publishEndpoint,DataContext data)
    {
        this.publishEndpoint = publishEndpoint;
        _data = data;
    }
    
    [HttpPost]
    public async Task<IActionResult> Notify()
    {
        for (int i = 0; i <= 5; i++)
        {
            await publishEndpoint.Publish<ExampleMessage>(new ExampleMessage()
            {
                Id = i,
                Text = "Hello from consumer",
            });

            _data.Orders.Add(new ExampleMessage()
            {
                Text = "Hello from Producer",
            });

            await _data.SaveChangesAsync();
        }

        return Ok();
    }
}