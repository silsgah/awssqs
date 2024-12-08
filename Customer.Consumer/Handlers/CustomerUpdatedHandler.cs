using Customer.Consumer.Messages;
using MediatR;

namespace Customer.Consumer.Handlers
{
    public class CustomerUpdatedHandler : IRequestHandler<CustomerUpdated>
    {
        private readonly ILogger<CustomerCreated> _logger;

        public CustomerUpdatedHandler(ILogger<CustomerCreated> logger)
        {
            _logger = logger;
        }

        public Task<Unit> Handle(CustomerUpdated request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(request.FullName);
            return Unit.Task;
        }
    }
}
