using Customer.Consumer.Messages;
using MediatR;


namespace Customer.Consumer.Handlers
{
    public class CustomerDeletedHandler : IRequestHandler<CustomerDeleted>
    {
        private readonly ILogger<CustomerCreated> _logger;
        public CustomerDeletedHandler(ILogger<CustomerCreated> logger)
        {
            _logger = logger;
        }
        public Task<Unit> Handle(CustomerDeleted request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(request.Id.ToString());
            return Unit.Task;
        }
       
    }
}
