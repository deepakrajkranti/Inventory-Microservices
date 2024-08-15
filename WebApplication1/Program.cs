using MassTransit;
using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.AddConsumer<Consumer>();
    busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
    {
        busFactoryConfigurator.Host("localhost", hostConfigurator => { });
        busFactoryConfigurator.ReceiveEndpoint("example-queue", endpointConfigurator =>
        {
            // Configure consumers for this queue, if any
            endpointConfigurator.ConfigureConsumer<Consumer>(context);
        });
    });
    
    
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();