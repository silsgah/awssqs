
using Amazon.SQS;
using Amazon.SQS.Model;
using Customer.Consumer.Messages;
using MediatR;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json;

namespace Customer.Consumer
{
    public class QueueConsumerService : BackgroundService
    {
        private readonly IAmazonSQS _sqs;
        private readonly IOptions<QueueSettings> _queueSettings;
        private readonly IMediator _mediator;
        private readonly ILogger<QueueConsumerService> _logger;

        public QueueConsumerService(IAmazonSQS sqs, IOptions<QueueSettings> queueSettings, IMediator mediator, ILogger<QueueConsumerService> logger)
        {
            _sqs = sqs;
            _queueSettings = queueSettings;
            _mediator = mediator;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queueUrlResponse = await _sqs.GetQueueUrlAsync(_queueSettings.Value.Name, stoppingToken);
            _logger.LogWarning("_queueSettings.Value.Name: {_queueSettings.Value.Name}", _queueSettings.Value.Name);
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = queueUrlResponse.QueueUrl,
                AttributeNames = new List<string> { "All" },
                MessageAttributeNames = new List<string> { "All" },
                MaxNumberOfMessages = 1
            };
            _logger.LogWarning("queueUrlResponse.QueueUrl: {queueUrlResponse.QueueUrl}", queueUrlResponse.QueueUrl);
            if (string.IsNullOrEmpty(queueUrlResponse.QueueUrl))
            {
                throw new InvalidOperationException("Failed to retrieve QueueUrl.");
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                var response = await _sqs.ReceiveMessageAsync(receiveMessageRequest, stoppingToken);
                if (response.Messages == null || !response.Messages.Any())
                {
                    _logger.LogWarning("No messages retrieved from the queue.");
                    await Task.Delay(1000, stoppingToken);
                    continue;
                }

                foreach (var message in response.Messages)
                {
                    if (!message.MessageAttributes.TryGetValue("MessageType", out var messageTypeAttribute) || string.IsNullOrEmpty(messageTypeAttribute.StringValue))
                    {
                        _logger.LogWarning("Message does not have a valid 'MessageType' attribute.");
                        continue;
                    }

                    var messageType = message.MessageAttributes["MessageType"].StringValue;
                    //var assembly = Assembly.Load("Customer.Consumer.Messages");
                    //var type = assembly.GetType($"Customer.Consumer.Messages.{messageType}");
                    //_logger.LogWarning("assembly NAME {assembly}", assembly);
                    var type = Type.GetType($"Customer.Consumer.Messages.{messageType}, Customer.Consumer");
                    _logger.LogWarning("assembly NAME {type}", type);
                    if (type is null)
                    {
                        _logger.LogWarning("Unknown message type: {MessageType}", messageType);
                        continue;
                    }

                    var typedMessage = (ISqsMessage)JsonSerializer.Deserialize(message.Body, type)!;

                    try
                    {
                        await _mediator.Send(typedMessage, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Message failed during processing");
                        continue;
                    }

                    await _sqs.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle, stoppingToken);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}