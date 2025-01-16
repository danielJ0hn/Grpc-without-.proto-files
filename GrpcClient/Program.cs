// See https://aka.ms/new-console-template for more information

using Grpc.Net.Client;
using GrpcProtos.Models;
using GrpcProtos.ServiceContracts;
using ProtoBuf.Grpc.Client;

namespace GrpcClient
{
    class GrpcClientClass
    {
        static async Task Main(string[] args)
        {
            var request = new CustomerLookupModel { userId = 1 };

            //var channel = GrpcChannel.ForAddress("https://localhost:7062");
            //var client = new Customer.CustomerClient(channel);

            //var reply = await client.GetCustomerInfoAsync(request);

            //Console.WriteLine($"{reply.FirstName} {reply.LastName}");

            //using (var call = client.GetNewCustomers(new NewCustomerRequest()))
            //{
            //    while (await call.ResponseStream.MoveNext())
            //    {
            //        var current = call.ResponseStream.Current;
            //        Console.WriteLine($"{current.FirstName} {current.LastName}: {current.EmailAddress}");
            //    }
            //}

            using (var channel = GrpcChannel.ForAddress("https://localhost:7062"))
            {
                var customerService = channel.CreateGrpcService<ICustomersService>();
                var result = await customerService.GetCustomerInfoAsync(request);

                Console.WriteLine($"{result.firstName} {result.lastName}");

                await foreach (var customerInfo in customerService.GetCustomerListAsync())
                {
                    Console.WriteLine($"{customerInfo.firstName} {customerInfo.lastName}");
                }
            }

            Console.Read();
        }
    }
}