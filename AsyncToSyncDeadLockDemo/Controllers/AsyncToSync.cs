using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncToSyncDeadLockDemo.Controllers
{
    public class AsyncToSync
    {
        public static string GetString()
        {
            var url = "https://blog.stephencleary.com/2012/07/dont-block-on-async-code.html";
            using (var client = new WebClient())
            {
                string html = client.DownloadString(url);
                return html;
            }
        }

        public static async Task<string> GetStringAsync()
        {
            var url = "https://blog.stephencleary.com/2012/07/dont-block-on-async-code.html";
            using (var client = new HttpClient())
            {
                var html = await client.GetStringAsync(new Uri(url));
                return html;
            }
        }
    }
}