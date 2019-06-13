using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Worlds
{
    [System.Serializable]
    public class MonsterSkillBasicData
    {
        [SkillIdPropertyAtribute]
        public int Id;
        [Range(1, 100)]
        public int Level;
    }

    [System.Serializable]
    public class MonsterSkillData : ObjecIdentifier
    {
        public List<MonsterSkillBasicData> SkillBank = new List<MonsterSkillBasicData>();
    }
    [System.Serializable]
    public class MonsterDrop
    {
        [ItemsIdPropertyAtribute]
        public int Id;
        [Range(1, 100)]
        public float DropChance;
    }
    [System.Serializable]
    public class MonsterDropByLevel : MonsterDrop
    {
        [Range(1, 100)]
        public int MinLevelToDrop;
    }
    [System.Serializable]
    public class MonsterDropData : ObjecIdentifier
    {
        public List<MonsterDrop> Drops = new List<MonsterDrop>();
        public List<MonsterDropByLevel> DropsByLevel = new List<MonsterDropByLevel>();
    }
}