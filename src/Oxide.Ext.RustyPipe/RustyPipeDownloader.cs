using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

    }
}
