using System.ServiceModel;
using GrpcProtos.Models;
using ProtoBuf.Grpc;

namespace GrpcProtos.ServiceContracts
{
    [ServiceContract]
    public interface ICustomersService
    {
        ValueTask<CustomerModel> GetCustomerInfoAsync(CustomerLookupModel request, CallContext context = default);
        IAsyncEnumerable<CustomerModel> GetCustomerListAsync(CallContext context = default);
    }
}
