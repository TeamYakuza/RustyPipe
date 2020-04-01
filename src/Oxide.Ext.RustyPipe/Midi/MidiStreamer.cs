using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MidiParser;

namespace Oxide.Ext.RustyPipe.Midi
{
    public class MidiStreamer
    {
        public MidiFile DownloadMidiFile(string url)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var data = client.DownloadData(url);
                   return new MidiFile(data);
                   
                }
            }
            catch (Exception)
            {
               
            }

            return null;
        }
    }
}
