using UnityEngine;

namespace Worlds.Monsters
{
    [System.Serializable]
    public class MonsterVariantsData : ObjecIdentifier
    {
        public MonsterVariation Variation = MonsterVariation.Normal;
        [Range(0, 100)]
        public int FusionChance = 30;
        public Element OtherMainElement;
    }
}