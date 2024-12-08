using Amazon.SQS.Model;

namespace Customers.Api.Mesaaging
{
    public interface ISqsMessenger
    {
        Task<SendMessageResponse> SendMessageAsync<T>(T message);
    }
}
