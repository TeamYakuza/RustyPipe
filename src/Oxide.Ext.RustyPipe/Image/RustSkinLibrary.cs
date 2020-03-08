using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Steamworks;

namespace Oxide.Ext.RustyPipe.Image
{
    internal class RustSkinLibrary
    {
        
        public RustSkinLibrary()
        {
        }

        internal string GetImageUrl(int skinId)
        {
            InventoryDef rustSkinItem =
                Steamworks.SteamInventory.Definitions.FirstOrDefault(inventoryDef => inventoryDef.Id == skinId);
            if (rustSkinItem != null)
            {
                return rustSkinItem.IconUrlLarge;
            }
            return null;
        }

        internal InventoryDef GetSkinData(int skinId)
        {
            return Steamworks.SteamInventory.Definitions.FirstOrDefault(inventoryDef => inventoryDef.Id == skinId);
        }

        public void Load()
        {
           
        }

    }
}
