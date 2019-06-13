using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Worlds.Monsters
{
    public class DropsDataCreator : DataCreatorEditorSingleObject<MonsterDropData>
    {
        [MenuItem("Worlds/Mods/Drops Data Creator")]
        public static void ShowMonstersDataCreator()
        {
            GetWindow(typeof(DropsDataCreator));
        }
    }
}