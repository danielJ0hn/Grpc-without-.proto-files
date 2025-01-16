using System.Runtime.Serialization;

namespace GrpcProtos.Models
{
    [DataContract]
    public class CustomerLookupModel
    {
        [DataMember(Order = 1)]
        public int userId { get; set; }
    }
}
