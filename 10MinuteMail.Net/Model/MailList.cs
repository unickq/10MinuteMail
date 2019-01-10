using System.Runtime.Serialization;

namespace _10MinuteMail.Net
{
    [DataContract]
    public class MailList
    {
        [DataMember(Name = "mail_id")] public string MailId { get; set; }

        [DataMember(Name = "from")] public string From { get; set; }

        [DataMember(Name = "subject")] public string Subject { get; set; }

        [DataMember(Name = "datetime")] public string Datetime { get; set; }

        [DataMember(Name = "datetime2")] public string Datetime2 { get; set; }

        [DataMember(Name = "timeago")] public long Timeago { get; set; }

        [DataMember(Name = "isread")] public bool Isread { get; set; }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}