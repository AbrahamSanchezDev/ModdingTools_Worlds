using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Worlds
{
    [CreateAssetMenu(fileName = "MonstersToRemove",menuName = "Worlds/Mods/MonstersToRemove")]
    public class MonstersToRemove : ScriptableObject
    {
        public List<int> MonstersIds = new List<int>(); 
    }
}