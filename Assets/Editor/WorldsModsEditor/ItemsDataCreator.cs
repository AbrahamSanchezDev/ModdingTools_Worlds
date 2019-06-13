using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Worlds.Monsters;

namespace Worlds.InventorySystem
{
    public class ItemsDataCreator : DataCreatorEditorSingleObject<ItemBaseData>
    {
        [MenuItem("Worlds/Mods/Items Data Creator")]
        public static void ShowItemsDataCreator()
        {
            GetWindow(typeof(ItemsDataCreator));
        }
    }
}