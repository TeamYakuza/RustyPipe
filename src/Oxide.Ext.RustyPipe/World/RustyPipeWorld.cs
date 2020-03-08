using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rust;
using UnityEngine;
using Server = ConVar.Server;

namespace Oxide.Ext.RustyPipe.World
{
    public class RustyPipeWorld
    {
        /// <summary>
        /// Set the world time.
        /// </summary>
        /// <param name="time"></param>
        public void SetTime(DateTime time)
        {
            if (TOD_Sky.Instance != null)
            {
                TOD_Sky.Instance.Cycle.DateTime = time;
            }
        }
        /// <summary>
        /// Set the world time by the hour.
        /// </summary>
        /// <param name="hour"></param>
        public void SetTime(int hour)
        {
            if (TOD_Sky.Instance != null)
            {
                TOD_Sky.Instance.Cycle.Hour = hour;
            }
        }

        public float GetHour() => (TOD_Sky.Instance?.Cycle!=null) ? TOD_Sky.Instance.Cycle.Hour : 0;
        public float GetMonth() => (TOD_Sky.Instance?.Cycle != null) ? TOD_Sky.Instance.Cycle.Month : 0;
        public DateTime GetTime() => (TOD_Sky.Instance?.Cycle != null) ? TOD_Sky.Instance.Cycle.DateTime : new DateTime(2001,1,1);
        public bool IsNight => TOD_Sky.Instance != null && TOD_Sky.Instance.IsNight;
        public bool IsDay => TOD_Sky.Instance != null && TOD_Sky.Instance.IsDay;

        public bool HasPatrolHelicopter => PatrolHelicopters != null && PatrolHelicopters.Count > 0;

        public BaseHelicopter[] PatrolHelicoptersAlive => PatrolHelicopters.Where(helicopter => !helicopter.IsDestroyed && helicopter.IsAlive()).ToArray();

        public List<BaseHelicopter> PatrolHelicopters =new List<BaseHelicopter>();

        public List<CH47Helicopter> ChinookHelicopters { get; set; } = new List<CH47Helicopter>();
        public bool HasChinookHelicopter => ChinookHelicopters != null && ChinookHelicopters.Count > 0;
        public CH47Helicopter[] ChinookHelicoptersAlive => ChinookHelicopters.Where(helicopter => !helicopter.IsDestroyed && helicopter.IsAlive()).ToArray();

        public List<CargoPlane> CargoPlanes { get; set; } = new List<CargoPlane>();
        public bool HasCargoPlane => CargoPlanes != null && CargoPlanes.Count > 0;

        public List<CargoShip> CargoShips { get; set; } = new List<CargoShip>();
        public bool HasCargoShip => CargoShips != null && CargoShips.Count > 0;

        /// <summary>
        /// Spawns the patrol helicopter
        /// </summary>
        /// <returns></returns>
        public BaseHelicopter SpawnPatrolHelicopter()
        {
           var heli=(BaseHelicopter)GameManager.server.CreateEntity("assets/prefabs/npc/patrol helicopter/patrolhelicopter.prefab",GetRandomSpawn()+new Vector3(0,500,0));
           if (!heli.isSpawned)
               heli.Spawn();
           return heli;
        }
        /// <summary>
        /// Spawns the patrol helicopter and sends it to the specified location.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public BaseHelicopter SpawnPatrolHelicopter(Vector3 target,float speed=0f,float maxSpeed=0f)
        {
            var heli = SpawnPatrolHelicopter();
            if (heli != null)
            {
                var ai = heli.gameObject.GetComponent<PatrolHelicopterAI>();
                if (ai != null)
                {
                    if (maxSpeed > 0)
                        ai.maxSpeed = maxSpeed;
                    if (speed > 0)
                        ai.moveSpeed = speed;
                    ai.destination = target;
                }
            }
            return heli;
        }
        /// <summary>
        /// Spawns the Chinook
        /// </summary>
        /// <returns></returns>
        public CH47Helicopter SpawnChinook()
        {
            var heli = (CH47Helicopter)GameManager.server.CreateEntity("assets/prefabs/npc/ch47/ch47scientists.entity.prefab", GetRandomSpawn() + new Vector3(0, 500, 0));
            if (!heli.isSpawned)
            {
                heli.Spawn();
            }
            return heli;
        }

        /// <summary>
        /// Spawns the Chinook and sets its target to the specified location
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public CH47Helicopter SpawnChinook(Vector3 target)
        {
            var heli = SpawnChinook();
            if (heli != null)
            {
                var ai = heli.gameObject.GetComponent<CH47HelicopterAIController>();
                if (ai != null)
                {
                    ai.landingTarget = target;
                }
            }
            return heli;
        }

        Vector3 GetRandomSpawn()
        {
            float x = TerrainMeta.Collider.bounds.min.x;
            float y = TerrainMeta.Collider.bounds.min.z;
            switch (UnityEngine.Random.Range(0, 4))
            {
                case 0:
                {
                    x = UnityEngine.Random.Range(TerrainMeta.Collider.bounds.min.x, TerrainMeta.Collider.bounds.max.x);

                    break;
                }
                case 1:
                {
                    y = UnityEngine.Random.Range(TerrainMeta.Collider.bounds.min.z, TerrainMeta.Collider.bounds.max.z);
                    break;
                }
                case 2:
                {
                    x = UnityEngine.Random.Range(TerrainMeta.Collider.bounds.min.x, TerrainMeta.Collider.bounds.max.x);
                    y = TerrainMeta.Collider.bounds.max.z;
                    break;
                }
                case 3:
                {
                    y = UnityEngine.Random.Range(TerrainMeta.Collider.bounds.min.z, TerrainMeta.Collider.bounds.max.z);
                    x = TerrainMeta.Collider.bounds.max.x;
                    break;
                }
            }
            return new Vector3(x,0,y);
        }

        /// <summary>
        /// Spawns the Cargo shop
        /// </summary>
        /// <returns></returns>
        public CargoShip SpawnCargoShip()
        {
            

            var cargo = StringPool.Get(3234960997);
            var entity = (CargoShip)GameManager.server.CreateEntity(cargo, GetRandomSpawn());
            if (!entity.isSpawned)
            {
                entity.Spawn();
            }

            return entity;
        }


        /// <summary>
        /// Spawns an Airdrop in a random position, but heads towards the target vector to drop the supplies.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public CargoPlane SpawnAirdrop(Vector3 target)
        {
            var plane = SpawnAirdrop();
            plane.dropPosition = target;
            return plane;
        }
        /// <summary>
        /// Spawns a random Airdrop
        /// </summary>
        /// <returns></returns>
        public CargoPlane SpawnAirdrop()
        {
            var plane= (CargoPlane)GameManager.server.CreateEntity("assets/prefabs/npc/cargo plane/cargo_plane.prefab", GetRandomSpawn() + new Vector3(0, 500, 0));
            if (!plane.isSpawned)
            {
                plane.Spawn();
            }
            return plane;
        }
        internal void Init()
        {
            
        }
    }
}
