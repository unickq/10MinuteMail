using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace _10MinuteMail.Net
{
    public static class JsonHelper
    {
        public static string ToJson(object o)
        {
            var ms = new MemoryStream();
            var ser = new DataContractJsonSerializer(o.GetType());
            ser.WriteObject(ms, o);
            var json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }
    }
}