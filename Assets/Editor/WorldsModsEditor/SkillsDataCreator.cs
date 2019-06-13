using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Worlds.Monsters;
namespace Worlds
{
    public class SkillsDataCreator : DataCreatorEditorSingleObject<MonsterSkillData>
    {
        [MenuItem("Worlds/Mods/Monsters Skills Data Creator")]
        public static void ShowMonstersDataCreator()
        {
            GetWindow(typeof(SkillsDataCreator));
        }
    }
}