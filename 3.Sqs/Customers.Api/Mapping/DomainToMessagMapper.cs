using Customers.Api.Contracts.Messages;
using Customers.Api.Domain;
using SqsPublisher;

namespace Customers.Api.Mapping
{
    public static class DomainToMessagMapper
    {
        public static CustomerCreated ToCustomerCreatedMessage(this Customer customer)
        {
            return new CustomerCreated
            {
                Id = customer.Id,
                Email = customer.Email,
                GitHubUsername = customer.GitHubUsername,
                FullName = customer.FullName,
                DateOfBirth = customer.DateOfBirth
            };
        }
        public static CusomerUpdated ToCustomerUpdatedMessage(this Customer customer)
        {
            return new CusomerUpdated
            {
                Id = customer.Id,
                Email = customer.Email,
                GitHubUsername = customer.GitHubUsername,
                FullName = customer.FullName,
                DateOfBirth = customer.DateOfBirth
            };
        }
    }
}
