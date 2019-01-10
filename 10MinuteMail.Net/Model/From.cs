using System.Runtime.Serialization;

namespace _10MinuteMail.Net
{
    [DataContract]
    public class From
    {
        [DataMember(Name = "name")] public string Name { get; set; }

        [DataMember(Name = "address")] public string Address { get; set; }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}