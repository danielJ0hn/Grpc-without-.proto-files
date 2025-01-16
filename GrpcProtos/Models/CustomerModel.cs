
using System.Runtime.Serialization;
using ProtoBuf;

namespace GrpcProtos.Models
{
    [DataContract]
    public class CustomerModel
    {
        [DataMember(Order = 1)]
        public string firstName { get; set; } = string.Empty;
        [DataMember(Order = 2)]
        public string lastName { get; set; } = string.Empty;
        [DataMember(Order = 3)]
        public List<String> emailAddress { get; set; } = new List<string>();
        [DataMember(Order = 4)]
        public bool isAlive { get; set; }
        [DataMember(Order = 5)]
        public int age { get; set; }
    }
}
