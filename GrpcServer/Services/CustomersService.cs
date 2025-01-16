
using GrpcProtos.Models;
using GrpcProtos.ServiceContracts;
using ProtoBuf.Grpc;

namespace GrpcServer.Services
{
    public class CustomersService(ILogger<CustomersService> logger): ICustomersService
    {
        public ValueTask<CustomerModel> GetCustomerInfoAsync(CustomerLookupModel request, CallContext context = default)
        {
            CustomerModel customer = new CustomerModel();

            if (request.userId == 1)
            {
                customer.firstName = "abc";
                customer.lastName = "def";
            }
            else
            {
                customer.firstName = "tuv";
                customer.lastName = "xyz";
            }

            return ValueTask.FromResult(customer);
        }

        public async IAsyncEnumerable<CustomerModel> GetCustomerListAsync(CallContext context = default)
        {
            List<CustomerModel> customerModels = new List<CustomerModel> {
            new CustomerModel {
                firstName = "abc",
                lastName = "def"
            },
            new CustomerModel {
                firstName = "tuv",
                lastName = "xyz"
            },
            new CustomerModel {
                firstName = "ghijkl",
                lastName = "mno"
            },
            new CustomerModel {
                firstName = "asdjhgasd",
                lastName = "sdfgdrfg"
            }};

            foreach (var customers in customerModels)
            {
                await Task.Delay(1000);
                yield return customers;
            }

        }
    }
}
