using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Worlds.Monsters;

namespace Worlds
{
    public class DropsMultiEdit : MultiDataEditorObj<MonsterDropData>
    {
        private static EditorWindow theWindow;
        [MenuItem("Worlds/Mods/DropsMultiEdit")]
        public static void ShowDropsMultiEdit()
        {
            theWindow =  GetWindow(typeof(DropsMultiEdit));
        }

        protected override string FixName()
        {
            return "_Drops";
        }

        protected override void SetCurWindow()
        {
            CurWindow = theWindow;
        }
        //protected override bool AddObj(FileInfo theFileInfo)
        //{
        //    if (!theFileInfo.Name.Contains("_Drops") && !theFileInfo.Name.Contains("_Skills") && theFileInfo.Name.Contains("_"))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        protected override string FolderSearch
        {
            get { return "MonstersMod"; }
        }
    }
}