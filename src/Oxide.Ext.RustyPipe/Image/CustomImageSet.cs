using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Ionic.Zip;
using Newtonsoft.Json;

namespace Oxide.Ext.RustyPipe.Image
{
    public class CustomImageSet:RustyPipeImageStore
    {
        public override string Filename => "RustyPipe_CustomImageSets.json";
        public List<string> ImageSets { get; set; }=new List<string>();

        public override void Save()
        {

            File.WriteAllText(FullPath, JsonConvert.SerializeObject(ImageSets, Formatting.Indented));
        }
        public override void Load()
        {
            if (File.Exists(FullPath))
            {
                JsonConvert.PopulateObject(File.ReadAllText(FullPath), ImageSets);
            }
            else Save();
        }
        internal void Prepare()
        {
            ServerLoaded = true;
            foreach (var url in ImageSets)
            {
                if (!DownloadAndAdd(url))
                {
                    RustyPipeDebug.LogWarning($"Unable to download: {Path.GetFileNameWithoutExtension(url)}");
                }
            }
        }

        
        public bool AddImage(string url)
        {
            if (!ServerLoaded)
            {
                return true;
            }

            if (!ImageSets.Contains(url))
            {
                ImageSets.Add(url);
                Save();
                RustyPipeDebug.Log($"Adding Custom Image Set: {Path.GetFileName(url)}");
                return DownloadAndAdd(url);
            }
            else
            {
                return true;
            }

        }

        bool DownloadAndAdd(string url)
        {
            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "oxide", "data","ImageSets", Path.GetFileName(url));
                if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "oxide", "data",
                    "ImageSets")))
                    Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "oxide", "data",
                        "ImageSets"));
                try
                {
                    using (var client = new WebClient())
                    {
                        var data = client.DownloadData(url);
                        File.WriteAllBytes(path, data);
                    }
                }
                catch (Exception)
                {

                }

                using (var _imageFile = new ZipFile(path))
                {
                    foreach (var e in _imageFile.Entries)
                    {
                        var fname = Path.GetFileNameWithoutExtension(e.FileName);
                        using (var strm = new MemoryStream())
                        {
                            e.Extract(strm);
                            RustyPipeDebug.Log($"Imported image {fname} from set {Path.GetFileName(path)}");
                            AddImage(fname, FileStorage.Type.png, strm.ToArray());
                            
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}