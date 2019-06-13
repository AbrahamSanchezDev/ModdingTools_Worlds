using System.Collections.Generic;
using UnityEngine;

namespace Worlds.Monsters
{
    [System.Serializable]
    public class MonsterBaseData : ObjecIdentifier
    {
        public StatsGroup BaseStats;
        public float Height = 2;
        public float Width = 0.5f;
        public int ExpYield;
        [Range(0, 100)]
        public int CatchRate = 100;
        public Element Element1;
        public Element Element2;
        public Evolution MyEvolution;
        public float ProjectileZ = 1;
        public float ProjectileY = 1;
        public float CastingDelay;
        public float MeleeDelay;
        public float ProjectileDelay;
        public List<DeathModStat> OnDeathModStats;
        public int MinSpawnLevel;
        public int MaxSpawnLevel = 100;
    }
}