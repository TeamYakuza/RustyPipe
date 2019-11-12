using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Oxide.Ext.RustyPipe.Image
{

    class CustomImages:RustyPipeImageStore
    {
        public string _comment =>
            "The format is \"image_identifier\":\"http://www.google.com/myimage.png\". Example: \"gold_image\"http://google.com/goldimage.png\"".Replace("\\", "");
        public override string Filename => "RustyPipe_CustomImages.json";
        public Dictionary<string, string> CustomImageUrls { get; set; } = new Dictionary<string, string>();

        
        internal void Prepare()
        {
            ServerLoaded = true;
            foreach (var url in CustomImageUrls)
            {
                RustyPipeDebug.Log($"Adding Custom Image: {url.Key}");

                if (!DownloadAndAdd(url.Key, url.Value))
                {
                    RustyPipeDebug.LogWarning($"Unable to download: {url.Key}:{url.Value}");
                }
            }
        }
        public bool AddImage(string identifier, string url)
        {
            if (!ServerLoaded)
            {
                CustomImageUrls[identifier] = url;
                Save();
                return true;
            }

            return DownloadAndAdd(identifier, url);

        }

        bool DownloadAndAdd(string identifier, string url)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var data = client.DownloadData(url);
                    FileStorage.Type type = FileStorage.Type.png;
                    if (Path.GetExtension(url) == "jpg")
                    {
                        type = FileStorage.Type.jpg;
                    }

                    return AddImage(identifier, type, data);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
