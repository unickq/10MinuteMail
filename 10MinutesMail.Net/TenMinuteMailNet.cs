using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace _10MinuteMail.Net
{
    public class TenMinuteMailNet 
    {
        private const string BaseUrl = "https://10minutemail.net";
        private readonly HttpClient _httpClient;

        public TenMinuteMailNet()
        {
            _httpClient = new HttpClient();
        }

        public MailResponse MailResponse => GetMailResponseAsync().Result;

        public string EmailAddress => GetEmailAddressAsync().Result;

        public long SecondsLeft => GetSecondsLeftAsync().Result;

        public int EmailCount => GetEmailsCountAsync().Result;
        public MailContent LastEmail => GetLastEmailAsync().Result;

        private async Task<string> GetResponseString(string responseUrl)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}{responseUrl}");
            var responseMessage = _httpClient.SendAsync(httpRequest).Result;
            if (responseMessage.Headers.TryGetValues("set-cookie", out var cookies))
                foreach (var cookie in cookies)
                    _httpClient.DefaultRequestHeaders.Add("Cookie", cookie);
            return await responseMessage.Content.ReadAsStringAsync();
        }

        private async Task<MailResponse> GetMailResponseAsync()
        {
            var mailResponse = new MailResponse();
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(await GetResponseString("/address.api.php")));
            var ser = new DataContractJsonSerializer(mailResponse.GetType());
            mailResponse = ser.ReadObject(ms) as MailResponse;
            ms.Close();
            return mailResponse;
        }

        public async Task<MailContent> GetMailContentAsync(string mailId)
        {
            var mailContent = new MailContent();
            var ms = new MemoryStream(
                Encoding.UTF8.GetBytes(await GetResponseString($"/mail.api.php?mailid={mailId}")));
            var ser = new DataContractJsonSerializer(mailContent.GetType());
            mailContent = ser.ReadObject(ms) as MailContent;
            ms.Close();
            return mailContent;
        }

        public MailContent GetMailContent(string mailId)
        {
            return GetMailContentAsync(mailId).Result;
        }

        public async Task<string> GetEmailAddressAsync()
        {
            return (await GetMailResponseAsync()).MailGetMail;
        }


        public async Task<long> GetSecondsLeftAsync()
        {
            return (await GetMailResponseAsync()).MailLeftTime;
        }


        public async Task<int> GetEmailsCountAsync()
        {
            return (await GetMailResponseAsync()).MailList.Count;
        }


        public async Task<List<MailContent>> GetEmailsAsync()
        {
            var response = await GetMailResponseAsync();

            var mails = new MailContent[response.MailList.Count];

            for (var i = 0; i < response.MailList.Count; i++)
                mails[i] = await GetMailContentAsync(response.MailList[i].MailId);

            return mails.ToList();
        }

        public List<MailContent> Emails => GetEmailsAsync().Result;

        public async Task<MailContent> GetLastEmailAsync()
        {
            var response = await GetMailResponseAsync();
            return await GetMailContentAsync(response.MailList[0].MailId);
        }

        public async Task Reset10MinutesAsync()
        {
            await GetResponseString("/more.html");
        }

        public void Reset10Minutes()
        {
            Reset10MinutesAsync().Wait();
        }

        public async Task Reset100MinutesAsync()
        {
            await GetResponseString("/more100.html");
        }

        public void Reset100Minutes()
        {
            Reset100MinutesAsync().Wait();
        }

        public async Task<string> GenerateNewEmailAddressAsync()
        {
            await GetResponseString("/new.html");
            return await GetEmailAddressAsync();
        }

        public string GenerateNewEmailAddress()
        {
            return GenerateNewEmailAddressAsync().Result;
        }

        public async Task RecoverExpiredEmailAddressAsync()
        {
            await GetResponseString("/recover.html");
        }

        public void RecoverExpiredEmailAddress()
        {
            RecoverExpiredEmailAddressAsync().Wait();
        }
    }
}