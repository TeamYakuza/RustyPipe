using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidiParser;
using Oxide.Ext.RustyPipe.Image;
using Oxide.Ext.RustyPipe.Midi;
using Oxide.Ext.RustyPipe.UI;
using Oxide.Ext.RustyPipe.World;

namespace Oxide.Ext.RustyPipe
{
    public static class RustyPipe
    {
        /// <summary>
        /// Easy Access to World Data
        /// </summary>
        public static RustyPipeWorld World { get; private set; } = new RustyPipeWorld();
        /// <summary>
        /// Easy Access to image data & streaming images from the server to the client
        /// </summary>
        public static ImageLibrary ImageLibrary { get; private set; }=new ImageLibrary();
        /// <summary>
        /// A easier user interface system. It wraps around CUI
        /// </summary>
        public static RustyPipeUI Ui { get; private set; }

        /// <summary>
        /// A easier method to download a midi file and read it.
        /// </summary>
        public static MidiStreamer MidiStreamer { get; private set; }


        /// <summary>
        /// Easy method for downloading a file,string or json.
        /// </summary>
        public static RustyPipeDownloader Downloader { get; private set; }
        internal static void Init()
        {
            World = new RustyPipeWorld();
            World.Init();
            ImageLibrary=new ImageLibrary();
            ImageLibrary.Init();
            Ui = new RustyPipeUI();
            MidiStreamer=new MidiStreamer();
            Downloader=new RustyPipeDownloader();
        }
    }
}
