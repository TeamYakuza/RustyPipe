using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Oxide.Ext.RustyPipe
{
    public class RustyPipeDownloader
    {
        public string DownloadString(string url)
        {
            using (var c = new WebClient())
            {
                try
                {
                    return c.DownloadString(url);
                }
                catch (Exception)
                {
                   //e
                }
            }

            return null;
        }

        public byte[] DownloadData(string url)
        {
            using (var c = new WebClient())
            {
                try
                {
                    return c.DownloadData(url);
                }
                catch (Exception)
                {
                    //e
                }
            }

            return null;
        }

        public T DownloadJson<T>(string url)
        {
            using (var c = new WebClient())
            {
                try
                {
                    var json= c.DownloadString(url);
                    return JsonConvert.DeserializeObject<T>(json);
                }
                catch (Exception)
                {
                    //e
                }
            }

            return default(T);
        }

        public string HttpGet(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("user-agent",
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.162 Safari/537.36");
                    var data = client.GetStringAsync(url).GetAwaiter().GetResult();
                    return data;
                }
            }
            catch (Exception)
            {

            }

            return null;
        }
        public T HttpGetJson<T>(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("user-agent",
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.162 Safari/537.36");
                    var data = client.GetStringAsync(url).GetAwaiter().GetResult();
                    var pluginData = JsonConvert.DeserializeObject<T>(data);
                }
            }
            catch (Exception)
            {
               
            }

            return default(T);
        }

    }
}
