using UnityEditor;
namespace Worlds.Monsters
{
    [System.Serializable]
    public class MonstersDataCreator : DataCreatorEditorSingleObject<MonsterBaseData>
    {
        [MenuItem("Worlds/Mods/Monsters Data Creator")]
        public static void ShowMonstersDataCreator()
        {
            GetWindow(typeof(MonstersDataCreator));
        }
    }
}