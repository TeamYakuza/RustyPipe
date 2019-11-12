using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Oxide.Ext.RustyPipe.Image
{
    public abstract class RustyPipeImageStore
    {
        [JsonIgnore]
        public bool ServerLoaded { get; set; } = false;
        [JsonIgnore]
        public abstract string Filename { get; }
        [JsonIgnore]

        public string FullPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "oxide", "data", Filename);
        private Dictionary<string,uint> RustIdentifiers { get; set; }=new Dictionary<string, uint>();

        public virtual void Save()
        {
            
            File.WriteAllText(FullPath, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
        public virtual void Load()
        {
            if (File.Exists(FullPath))
            {
                JsonConvert.PopulateObject(File.ReadAllText(FullPath), this);
            }
            else Save();
        }

       
        public virtual uint GetImage(string identifier)
        {
            if (!ServerLoaded)
                return 0;
            if (RustIdentifiers.TryGetValue(identifier, out var v))
                return v;
            return 0;
        }

        public virtual bool AddImage(string identifier, FileStorage.Type imageType, byte[] data)
        {
            if (!ServerLoaded)
                return false;
            var id = FileStorage.server.Store(data, imageType, CommunityEntity.ServerInstance.net.ID);
            if (id > 0)
            {
                RustIdentifiers[identifier] = id;
            }

            return id > 0;
        }

    }
}
