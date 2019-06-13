using UnityEditor;

namespace Worlds.Monsters
{
    public class VisualEquipmentCreator : DataCreatorEditorSingleObject<EquipmentVisualData>
    {
        [MenuItem("Worlds/Mods/Visual Equipment Data Creator")]
        public static void ShowEquipmentDataCreator()
        {
            GetWindow(typeof(VisualEquipmentCreator));
        }
    }
}