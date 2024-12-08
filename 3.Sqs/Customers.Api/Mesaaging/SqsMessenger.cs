using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Customers.Api.Mesaaging
{
    public class SqsMessenger : ISqsMessenger
    {
        private readonly IAmazonSQS _amazonSQS;
        private readonly IOptions<QueueSettings_> options;
        private string? _queueUrl;
        public SqsMessenger(IAmazonSQS amazonSQS, IOptions<QueueSettings_> options)
        {
            _amazonSQS = amazonSQS;
            this.options = options;
        }

        public async Task<SendMessageResponse> SendMessageAsync<T>(T message)
        {
            var queueUrll = await GetQueueUrlAsync<T>();

            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrll,
                MessageBody = JsonSerializer.Serialize(message),
                MessageAttributes = new Dictionary<string, MessageAttributeValue>
                {
                    {
                        "MessageType", new MessageAttributeValue
                        {
                            DataType = "String",
                            StringValue = typeof(T).Name
                        }
                    }
                }

            };

            return await _amazonSQS.SendMessageAsync(sendMessageRequest);
        }

        private async Task<string> GetQueueUrlAsync<T>()
        {
            if(_queueUrl is not null)
            {
                return _queueUrl;
            }
            var queueUrlResponse = await _amazonSQS.GetQueueUrlAsync(options.Value.Name);
            _queueUrl = queueUrlResponse.QueueUrl;
            return _queueUrl;
        }
    }
}
