using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Worlds.Monsters
{
    [System.Serializable]
    public class MonsterVariantsDataCreator : DataCreatorEditorSingleObject<MonsterVariantsData>
    {
        [MenuItem("Worlds/Mods/Monster Variants Creator")]
        public static void ShowMonsterVariantsCreator()
        {
            GetWindow(typeof(MonsterVariantsDataCreator));
        }
    }
}