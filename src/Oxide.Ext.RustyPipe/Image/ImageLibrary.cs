using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Oxide.Ext.RustyPipe.Plugin;
using Steamworks;

namespace Oxide.Ext.RustyPipe.Image
{
    public class ImageLibrary
    {
        private ImageStore ItemThumbnails { get; set; }=new ImageStore();
        private CustomImages CustomImages { get; set; }=new CustomImages();
        private RustSkinLibrary RustSkinLibrary { get; set; } = new RustSkinLibrary();

        internal void Init()
        {
            CustomImages.Load();
        }
        internal void Load()
        {
            ItemThumbnails.Load();
            CustomImages.Prepare();
            RustSkinLibrary.Load();
        }

        /// <summary>
        /// Get a rust item or custom image added to the store.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns>The streaming identifier</returns>
        public uint GetImage(string identifier)
        {
            var r = CustomImages.GetImage(identifier);
            if (r == 0) r = ItemThumbnails.GetImage(identifier);
            if (r == 0) r = ItemThumbnails.GetImage("none");
            return r;
        }

        /// <summary>
        /// Add a custom image to the database. Once added it will automatically be stored even after you have restarted.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="url"></param>
        public void AddImage(string identifier, string url)
        {
            if (!CustomImages.AddImage(identifier, url))
            {
                RustyPipeDebug.LogWarning($"Unable to store custom image: {identifier}:{url}");
            }
        }
        public InventoryDef GetRustSkinData(int skinId)
        {
            return RustSkinLibrary.GetSkinData(skinId);
        }

        public string GetRustSkinUrl(int skinId)
        {
            return RustSkinLibrary.GetImageUrl(skinId);
        }
        public bool HasImage(string identifier)
        {
            if (CustomImages.GetImage(identifier) > 0) return true;
            return ItemThumbnails.GetImage(identifier) > 0;
        }
    }
}
