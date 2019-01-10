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

        /// <summary>Initializes a new instance of the <see cref="TenMinuteMailNet"/> class. https://10minutemail.net - client</summary>
        public TenMinuteMailNet()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>Gets the mail response.</summary>
        /// <value>The mail response.</value>
        public MailResponse MailResponse => GetMailResponseAsync().Result;

        /// <summary>Gets the email address.</summary>
        /// <value>The email address.</value>
        public string EmailAddress => GetEmailAddressAsync().Result;

        /// <summary>Gets the seconds left.</summary>
        /// <value>The seconds left.</value>
        public long SecondsLeft => GetSecondsLeftAsync().Result;

        /// <summary>Gets the email count.</summary>
        /// <value>The email count.</value>
        public int EmailCount => GetEmailsCountAsync().Result;
        public MailContent LastEmail => GetLastEmailAsync().Result;

        /// <summary>Gets the response string.</summary>
        /// <param name="responseUrl">The response URL.</param>
        /// <returns></returns>
        private async Task<string> GetResponseString(string responseUrl)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}{responseUrl}");
            var responseMessage = _httpClient.SendAsync(httpRequest).Result;
            if (responseMessage.Headers.TryGetValues("set-cookie", out var cookies))
                foreach (var cookie in cookies)
                    _httpClient.DefaultRequestHeaders.Add("Cookie", cookie);
            return await responseMessage.Content.ReadAsStringAsync();
        }

        /// <summary>Gets the mail response asynchronous.</summary>
        /// <returns></returns>
        private async Task<MailResponse> GetMailResponseAsync()
        {
            var mailResponse = new MailResponse();
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(await GetResponseString("/address.api.php")));
            var ser = new DataContractJsonSerializer(mailResponse.GetType());
            mailResponse = ser.ReadObject(ms) as MailResponse;
            ms.Close();
            return mailResponse;
        }

        /// <summary>Gets the mail content asynchronous.</summary>
        /// <param name="mailId">The mail identifier.</param>
        /// <returns></returns>
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

        /// <summary>Gets the content of the mail.</summary>
        /// <param name="mailId">The mail identifier.</param>
        /// <returns></returns>
        public MailContent GetMailContent(string mailId)
        {
            return GetMailContentAsync(mailId).Result;
        }

        /// <summary>Gets the email address asynchronous.</summary>
        /// <returns></returns>
        public async Task<string> GetEmailAddressAsync()
        {
            return (await GetMailResponseAsync()).MailGetMail;
        }


        /// <summary>Gets the seconds left asynchronous.</summary>
        /// <returns></returns>
        public async Task<long> GetSecondsLeftAsync()
        {
            return (await GetMailResponseAsync()).MailLeftTime;
        }


        /// <summary>Gets the emails count asynchronous.</summary>
        /// <returns></returns>
        public async Task<int> GetEmailsCountAsync()
        {
            return (await GetMailResponseAsync()).MailList.Count;
        }


        /// <summary>Gets the emails asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<MailContent>> GetEmailsAsync()
        {
            var response = await GetMailResponseAsync();

            var mails = new MailContent[response.MailList.Count];

            for (var i = 0; i < response.MailList.Count; i++)
                mails[i] = await GetMailContentAsync(response.MailList[i].MailId);

            return mails.ToList();
        }

        /// <summary>Gets the emails.</summary>
        /// <value>The emails.</value>
        public List<MailContent> Emails => GetEmailsAsync().Result;

        public async Task<MailContent> GetLastEmailAsync()
        {
            var response = await GetMailResponseAsync();
            return await GetMailContentAsync(response.MailList[0].MailId);
        }

        /// <summary>Reset 10 minutes asynchronous.</summary>
        /// <returns></returns>
        public async Task Reset10MinutesAsync()
        {
            await GetResponseString("/more.html");
        }

        /// <summary>Reset 10 minutes.</summary>
        public void Reset10Minutes()
        {
            Reset10MinutesAsync().Wait();
        }

        /// <summary>Reset 100 minutes asynchronous.</summary>
        /// <returns></returns>
        public async Task Reset100MinutesAsync()
        {
            await GetResponseString("/more100.html");
        }

        /// <summary>Reset 100 minutes.</summary>
        public void Reset100Minutes()
        {
            Reset100MinutesAsync().Wait();
        }

        /// <summary>Generates the new email address asynchronous.</summary>
        /// <returns></returns>
        public async Task<string> GenerateNewEmailAddressAsync()
        {
            await GetResponseString("/new.html");
            return await GetEmailAddressAsync();
        }

        /// <summary>Generates the new email address.</summary>
        /// <returns></returns>
        public string GenerateNewEmailAddress()
        {
            return GenerateNewEmailAddressAsync().Result;
        }

        /// <summary>Recovers the expired email address asynchronous.</summary>
        /// <returns></returns>
        public async Task RecoverExpiredEmailAddressAsync()
        {
            await GetResponseString("/recover.html");
        }

        /// <summary>Recovers the expired email address.</summary>
        public void RecoverExpiredEmailAddress()
        {
            RecoverExpiredEmailAddressAsync().Wait();
        }
    }
}