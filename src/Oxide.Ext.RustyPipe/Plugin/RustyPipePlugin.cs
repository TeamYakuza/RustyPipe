using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oxide.Core.Plugins;
using Oxide.Ext.RustyPipe.Image;
using Oxide.Plugins;
using UnityEngine;

namespace Oxide.Ext.RustyPipe.Plugin
{
    
    public class RustyPipePlugin:CSPlugin
    {
        public RustyPipePlugin()
        {
            Author = "Team Yakuza";
            Name = "RustyPipe";
            Description = "Internal plugin for the RustyPipe Extension.";
        }

        [HookMethod("OnServerInitialized")]
        void OnServerInitialize()
        {
            RustyPipe.ImageLibrary.Load();
        }

        [HookMethod("OnEntitySpawned")]
        void OnEntitySpawned(BaseNetworkable entity)
        {
            var heli = entity as BaseHelicopter;
            if (heli != null)
            {
                if (!RustyPipe.World.PatrolHelicopters.Contains(heli))
                    RustyPipe.World.PatrolHelicopters.Add(heli);
            }

            var chinook = entity as CH47Helicopter;
            if (chinook != null)
            {
                if (!RustyPipe.World.ChinookHelicopters.Contains(chinook))
                    RustyPipe.World.ChinookHelicopters.Add(chinook);
            }

            var cargoShip = entity as CargoShip;
            if (cargoShip != null)
            {
                if (!RustyPipe.World.CargoShips.Contains(cargoShip))
                    RustyPipe.World.CargoShips.Add(cargoShip);
            }

            var cargoPlane = entity as CargoPlane;
            if (cargoPlane != null)
            {
                if (!RustyPipe.World.CargoPlanes.Contains(cargoPlane))
                    RustyPipe.World.CargoPlanes.Add(cargoPlane);
            }
        }

        [HookMethod("OnEntityKill")]
        void OnEntityKill(BaseNetworkable entity)
        {
            if (entity is BaseHelicopter heli)
                RustyPipe.World.PatrolHelicopters.Remove(heli);

            if (entity is CH47Helicopter chinook)
                RustyPipe.World.ChinookHelicopters.Remove(chinook);

            if (entity is CargoPlane cargoPlane)
                RustyPipe.World.CargoPlanes.Remove(cargoPlane);

            if (entity is CargoShip cargoShip)
                RustyPipe.World.CargoShips.Remove(cargoShip);
        }
    }
}
