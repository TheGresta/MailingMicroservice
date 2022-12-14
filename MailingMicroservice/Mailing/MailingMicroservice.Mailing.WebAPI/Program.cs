using MailingMicroservice.Mailing.Application;
using MailingMicroservice.Mailing.Application.Consumer;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();

builder.Services.AddMassTransit(opt => {
    opt.AddConsumer<CreateMemeMailCommandConsumer>();

    opt.UsingRabbitMq((context, cfg) =>
    {
        // Default Port : 5672
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint(builder.Configuration.GetSection("EndPoints:MemeMail").Value, e =>
        {
            e.ConfigureConsumer<CreateMemeMailCommandConsumer>(context);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
