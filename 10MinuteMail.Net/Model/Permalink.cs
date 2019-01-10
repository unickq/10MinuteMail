using System;
using System.Runtime.Serialization;

namespace _10MinuteMail.Net
{
    [DataContract]
    public class Permalink
    {
        [DataMember(Name = "host")] public string Host { get; set; }

        [DataMember(Name = "mail")] public string Mail { get; set; }

        [DataMember(Name = "url")] public Uri Url { get; set; }

        [DataMember(Name = "key")] public string Key { get; set; }

        [DataMember(Name = "time")] public long Time { get; set; }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}