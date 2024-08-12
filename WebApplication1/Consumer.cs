namespace WebApplication1;
using MassTransit;
using System.Threading.Tasks;

public class Consumer : IConsumer<ExampleMessage>
{
    public Task Consume(ConsumeContext<ExampleMessage> context)
    {
        var message = context.Message;
        // Handle the message here
        Console.WriteLine($"Received message: {message.Text}");
        return Task.CompletedTask;
    }
}

public class ExampleMessage
{
    public string? Text { get; set; }
}
