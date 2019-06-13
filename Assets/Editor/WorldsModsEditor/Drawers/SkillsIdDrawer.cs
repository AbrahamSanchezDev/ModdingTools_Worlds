using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace Worlds
{
    [CustomPropertyDrawer(typeof(SkillIdPropertyAtribute))]
    public class SkillsIdDrawer : PropertyDrawer
    {
        private GameObjectsDb theDb;
        private GameObjectsDb TheDb
        {
            get
            {
                if (theDb != null)
                {
                    return theDb;
                }
                theDb = new GameObjectsDb();
                theDb.Load("SkillsDb");
                return theDb;
            }
        }
        private string[] myPlaths;
        public string[] MyPaths
        {
            get
            {
                if (myPlaths != null)
                    return myPlaths;

                myPlaths = new string[TheDb.Objects.Count];
                for (int x = 0; x < TheDb.Objects.Count; x++)
                {
                    myPlaths[x] = TheDb.Objects[x].Id + "_" + TheDb.Objects[x].Name;
                }
                return myPlaths;
            }
        }


        private int[] skillIds;
        private int[] SkillIds
        {
            get
            {
                if (skillIds != null)
                    return skillIds;
              
                skillIds = new int[TheDb.Objects.Count];
                for (int i = 0; i < TheDb.Objects.Count; i++)
                {
                    skillIds[i] = TheDb.Objects[i].Id;
                }
                return skillIds;
            }
        }

        int index;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            EditorGUI.BeginChangeCheck();
            index = System.Array.IndexOf(SkillIds, property.intValue);
            index = EditorGUI.Popup(position, index, MyPaths);
            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = SkillIds[index];
            }
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property);
        }
    }
    [CustomPropertyDrawer(typeof(ItemsIdPropertyAtribute))]
    public class ItemsIdDrawer : PropertyDrawer
    {
        private GameObjectsDb theDb;
        private GameObjectsDb TheDb
        {
            get
            {
                if (theDb != null)
                {
                    return theDb;
                }
                theDb = new GameObjectsDb();
                theDb.Load("ItemsDb");
                return theDb;
            }
        }
        private string[] myPlaths;
        public string[] MyPaths
        {
            get
            {
                if (myPlaths != null)
                    return myPlaths;

                myPlaths = new string[TheDb.Objects.Count];
                for (int x = 0; x < TheDb.Objects.Count; x++)
                {
                    myPlaths[x] = TheDb.Objects[x].Id + "_" + TheDb.Objects[x].Name;
                }
                return myPlaths;
            }
        }


        private int[] skillIds;
        private int[] SkillIds
        {
            get
            {
                if (skillIds != null)
                    return skillIds;

                skillIds = new int[TheDb.Objects.Count];
                for (int i = 0; i < TheDb.Objects.Count; i++)
                {
                    skillIds[i] = TheDb.Objects[i].Id;
                }
                return skillIds;
            }
        }

        int index;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            EditorGUI.BeginChangeCheck();
            index = System.Array.IndexOf(SkillIds, property.intValue);
            index = EditorGUI.Popup(position, index, MyPaths);
            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = SkillIds[index];
            }
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property);
        }
    }
}