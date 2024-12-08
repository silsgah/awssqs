using Customer.Consumer.Messages;
using MediatR;

namespace Customer.Consumer.Handlers
{
    public class CustomerCreatedHandler : IRequestHandler<CustomerCreated>
    {
        private readonly ILogger<CustomerCreated> _logger;

        public CustomerCreatedHandler(ILogger<CustomerCreated> logger)
        {
            _logger = logger;
        }

        public Task<Unit> Handle(CustomerCreated request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(request.FullName);
            //throw new Exception("Something went broke");
            return Unit.Task;
        }
      
    }
}
