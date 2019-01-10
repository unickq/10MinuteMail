using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace _10MinuteMail.Net
{
    [DataContract]
    public class MailContent
    {
        [DataMember(Name = "from")] public string From { get; set; }

        [DataMember(Name = "gravatar")] public Uri Gravatar { get; set; }

        [DataMember(Name = "to")] public string To { get; set; }

        [DataMember(Name = "subject")] public string Subject { get; set; }

        [DataMember(Name = "datetime")] public string Datetime { get; set; }

        [DataMember(Name = "timestamp")] public long Timestamp { get; set; }

        [DataMember(Name = "datetime2")] public string Datetime2 { get; set; }

        [DataMember(Name = "urls")] public List<object> Urls { get; set; }

        [DataMember(Name = "body")] public object Body { get; set; }

        [DataMember(Name = "attachment")] public List<object> Attachment { get; set; }

        [DataMember(Name = "header_decode")] public HeaderDecode HeaderDecode { get; set; }

        [DataMember(Name = "html")] public List<string> HtmlList { get; set; }
        public string Html => HtmlList.First();

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}