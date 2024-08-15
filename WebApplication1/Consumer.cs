using WebApplication1.Data;

namespace WebApplication1;
using MassTransit;
using System.Threading.Tasks;

public class Consumer : IConsumer<ExampleMessage>
{
    private readonly DataContext _data;

    public Consumer(DataContext data)
    {
        _data = data;
    }
    public Task Consume(ConsumeContext<ExampleMessage> context)
    {
        var message = context.Message;
        // Handle the message here
        Console.WriteLine($"Received message: {message.Text}");

        _data.Orders.Add(new ExampleMessage()
        {
            Text = "Hello from consumer",
        });
        _data.SaveChangesAsync();
        return Task.CompletedTask;
    }
}

public class ExampleMessage
{
    public int Id { get; set; }
    public string? Text { get; set; }
}
