using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Ionic.Zip;

namespace Oxide.Ext.RustyPipe.Image
{
    class ImageStore:RustyPipeImageStore,IDisposable
    {
        public Dictionary<string, ZipEntry> ImageEntries { get; set; } = new Dictionary<string, ZipEntry>();
        public override string Filename => "RustyPipe_ImageStore.zip";
        private ZipFile _imageFile;
        public override void Save()
        {

        }

        public override void Load()
        {
            ServerLoaded = true;
            try
            {
                RustyPipeDebug.Log("Downloading Rust Item Image Thumbnails.");
                try
                {
                    using (var client = new WebClient())
                    {
                        var data = client.DownloadData("https://github.com/TeamYakuza/RustyPipe/raw/master/ItemImages.zip");
                        File.WriteAllBytes(FullPath, data);
                    }
                }
                catch (Exception)
                {

                }

                _imageFile = new ZipFile(FullPath);
                int totalCount = ItemManager.itemList.Count;
                foreach (var e in _imageFile.Entries)
                {
                    var fname = Path.GetFileNameWithoutExtension(e.FileName);
                    ImageEntries.Add(fname, e);
                    using (var strm = new MemoryStream())
                    {
                        e.Extract(strm);
                        AddImage(fname, FileStorage.Type.png, strm.ToArray());
                    }
                }
                RustyPipeDebug.Log($"Imported: {_imageFile.Entries.Count} of {totalCount+1} Item Images, Missing: {totalCount+1 - _imageFile.Entries.Count}");

            }
            catch (Exception)
            {
               
            }
        }

        public void Dispose()
        {
            _imageFile?.Dispose();
        }
    }
}
