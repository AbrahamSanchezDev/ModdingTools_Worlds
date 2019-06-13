using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Worlds.Monsters
{
    public class DataCreatorEditorSingleObject<Z> : EditorWindow where Z : ObjecIdentifier
    {
        public Z Data;
        private const float Height = 700;
        private const float StartPos1 = 100;
        private const float StartPos = 30;
        private const float DataWidth = 800;
        private Rect DataPosition1 = new Rect(200, StartPos1, DataWidth, Height);
        private Rect ListDataRect = new Rect(200, StartPos, 400, 70);
        private Vector2 scrollpos2;
        private string loadedName;
        private SerializedObject serialized;
        protected SerializedObject SerializedObj
        {
            get
            {
                if (serialized == null)
                {
                    serialized = new SerializedObject(this);
                }
                return serialized;
            }
            set { serialized = value; }
        }

        protected SerializedProperty Property
        {
            get
            {
                return SerializedObj.FindProperty("Data");
            }
        }
        protected void OnGUI()
        {
            Options();
            if (Data == null)
            {
                return;
            }
            DisplayData();
        }
        protected void DisplayData()
        {
            GUILayout.BeginArea(DataPosition1);
            scrollpos2 = EditorGUILayout.BeginScrollView(scrollpos2, false, false, GUILayout.Width(DataPosition1.width), GUILayout.Height(DataPosition1.height));

            if (Property != null)
            {
                EditorGUILayout.PropertyField(Property, true);

                SerializedObj.ApplyModifiedProperties();

                GUILayout.Space(50);
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
        protected void Options()
        {
            GUILayout.BeginArea(ListDataRect);
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            if (Data != null)
            {
                if (!string.IsNullOrEmpty(loadedName))
                    GUILayout.Label(loadedName);
                if (GUILayout.Button("Save"))
                {
                    SaveLocalizationData(Data);
                }
            }
            if (GUILayout.Button("Load"))
            {
                Data = LoadData();
            }
            if (GUILayout.Button("Create New"))
            {
                if (EditorUtility.DisplayDialog("Create New", "You sure want to Create New File?", "Yes", "Cancel"))
                {
                    Data = Activator.CreateInstance<Z>();
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
        protected void DeSelectInput()
        {
            GUI.FocusControl(GUI.GetNameOfFocusedControl());
        }

        protected virtual string SavePath()
        {
            return "";
        }
        private Z LoadData()
        {
            Z data = Activator.CreateInstance<Z>();
            string filePath = EditorUtility.OpenFilePanel("Select MyData To Edit", SavePath() != "" ? Application.streamingAssetsPath + SavePath() : Application.streamingAssetsPath, "json");

            if (!string.IsNullOrEmpty(filePath))
            {
                var fin = filePath.Split("/"[0]);
                loadedName = fin[fin.Length - 1];
                string dataAsJason = File.ReadAllText(filePath);
                data = JsonUtility.FromJson<Z>(dataAsJason);
            }
            return data;
        }
        private void SaveLocalizationData(Z data)
        {
            string filePath = EditorUtility.SaveFilePanel("Save MyData File", SavePath() != "" ? Application.streamingAssetsPath + SavePath() : Application.streamingAssetsPath, "", "json");

            if (!string.IsNullOrEmpty(filePath))
            {
                string dataAsJason = JsonUtility.ToJson(data, true);
                File.WriteAllText(filePath, dataAsJason);

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();
            }
        }
    }
}