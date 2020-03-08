using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Oxide.Ext.RustyPipe.Image
{
    internal class RustSkinLibrary
    {
        private const string SchemaURI = "http://s3.amazonaws.com/s3.playrust.com/icons/inventory/rust/schema.json";

        private Dictionary<int, RustSkinItem> Items { get; set; } = new Dictionary<int, RustSkinItem>();

        public RustSkinLibrary()
        {
        }

        internal string GetImageUrl(int skinId)
        {
            RustSkinItem rustSkinItem;
            string iconUrlLarge;
            if (!this.Items.TryGetValue(skinId, out rustSkinItem))
            {
                iconUrlLarge = null;
            }
            else
            {
                iconUrlLarge = rustSkinItem.icon_url_large;
            }
            return iconUrlLarge;
        }

        internal RustSkinItem GetSkinData(int skinId)
        {
            RustSkinItem rustSkinItem;
            RustSkinItem rustSkinItem1;
            if (!this.Items.TryGetValue(skinId, out rustSkinItem))
            {
                rustSkinItem1 = null;
            }
            else
            {
                rustSkinItem1 = rustSkinItem;
            }
            return rustSkinItem1;
        }

        public void Load()
        {
            RustyPipeDebug.Log("Downloading Rust Skin Database (Thanks to PlayRust.com for the Schema)", Array.Empty<object>());
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    foreach (RustSkinItem item in JsonConvert.DeserializeObject<RustSkinLibrary.RustSkinData>(webClient.DownloadString("http://s3.amazonaws.com/s3.playrust.com/icons/inventory/rust/schema.json")).items)
                    {
                        this.Items[item.itemdefid] = item;
                    }
                }
            }
            catch (Exception exception)
            {
                RustyPipeDebug.LogWarning("Unable to download database.", Array.Empty<object>());
            }
        }

        private class RustSkinData
        {
            public List<RustSkinItem> items
            {
                get;
                set;
            }

            public RustSkinData()
            {
            }
        }
    }
}
