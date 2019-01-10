using System.Collections.Generic;
using System.Runtime.Serialization;

namespace _10MinuteMail.Net
{
    [DataContract]
    public class MailResponse
    {
        [DataMember(Name = "mail_get_user")] public string MailGetUser { get; set; }

        [DataMember(Name = "mail_get_mail")] public string MailGetMail { get; set; }

        [DataMember(Name = "mail_get_host")] public string MailGetHost { get; set; }

        [DataMember(Name = "mail_get_time")] public long MailGetTime { get; set; }

        [DataMember(Name = "mail_get_duetime")]
        public long MailGetDuetime { get; set; }

        [DataMember(Name = "mail_server_time")]
        public long MailServerTime { get; set; }

        [DataMember(Name = "mail_get_key")] public string MailGetKey { get; set; }

        [DataMember(Name = "mail_left_time")] public long MailLeftTime { get; set; }

        [DataMember(Name = "mail_recovering_key")]
        public string MailRecoveringKey { get; set; }

        [DataMember(Name = "mail_recovering_mail")]
        public string MailRecoveringMail { get; set; }

        [DataMember(Name = "session_id")] public string SessionId { get; set; }

        [DataMember(Name = "permalink")] public Permalink Permalink { get; set; }

        [DataMember(Name = "mail_list")] public List<MailList> MailList { get; set; }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}