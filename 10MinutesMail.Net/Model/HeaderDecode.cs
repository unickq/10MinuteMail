using System.Collections.Generic;
using System.Runtime.Serialization;

namespace _10MinuteMail.Net
{
    [DataContract]
    public class HeaderDecode
    {
        [DataMember(Name = "from")] public List<From> From { get; set; }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}