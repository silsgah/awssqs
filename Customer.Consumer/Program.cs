using Amazon.SQS;
using Customer.Consumer;
using MediatR;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<QueueSettings>(builder.Configuration.GetSection(QueueSettings.Key));
builder.Services.AddSingleton<IAmazonSQS, AmazonSQSClient>();
builder.Services.AddHostedService<QueueConsumerService>();
builder.Services.AddMediatR(typeof(Program).Assembly);

var app = builder.Build();


app.Run();

