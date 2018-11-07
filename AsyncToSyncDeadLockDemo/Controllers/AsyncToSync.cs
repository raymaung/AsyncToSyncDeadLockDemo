using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;

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
                return html.Length.ToString();
            }
        }

        public static async Task<string> GetStringAsync(int duration = 1000)
        {
            Log.Info($"GetStringAsync.duration = #{duration}");
            var url = "https://blog.stephencleary.com/2012/07/dont-block-on-async-code.html";
            using (var client = new HttpClient())
            {
                Thread.Sleep(duration);
                var html = await client.GetStringAsync(new Uri(url));
                return html.Length.ToString();
            }
        }
    }
}