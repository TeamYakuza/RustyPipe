using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oxide.Ext.RustyPipe.Image;
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
        internal static void Init()
        {
            World = new RustyPipeWorld();
            World.Init();
            ImageLibrary=new ImageLibrary();
            ImageLibrary.Init();
            Ui = new RustyPipeUI();
        }
    }
}
