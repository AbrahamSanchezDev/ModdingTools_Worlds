using UnityEngine;

namespace Worlds.Monsters
{
    [System.Serializable]
    public class DeathModStat
    {
        public ModStat Stat;
        [Range(0, 4)] public int Amount = 1;
    }
}