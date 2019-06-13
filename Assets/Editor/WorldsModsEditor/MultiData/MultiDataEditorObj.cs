using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Worlds
{
    public class MultiDataEditorObj<Z> : EditorWindow where Z : ObjecIdentifier
    {
        private class ZDatas
        {
            public string TheName;// TheObj;
            public string Path;
            public Z TheData;
        }
        private List<ZDatas> Paths = new List<ZDatas>();
        public Z Data;
        private const float Height = 700;
        private const float StartPos1 = 100;
        private const float Pos2Start = 270;
        private const float StartPos = 30;
        private const float DataWidth = 600;
        protected static Rect DataPosition1 = new Rect(Pos2Start, StartPos1, dataWidth1, Height);
        private Rect ListDataRect = new Rect(200, StartPos, 400, 70);
        private Vector2 scrollpos2;
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
        }

        protected SerializedProperty Property
        {
            get
            {
                return SerializedObj.FindProperty("Data");
            }
        }

        private JsonRefsGo curRefsGo;

        protected void OnGUI()
        {
            Options();
            if (Data == null)
            {
                return;
            }
            //Debug.Log(Data.Name);
            DisplayData();
            
            DisplayList();
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
                if (GUILayout.Button("Save"))
                {
                    //SaveLocalizationData(Data);
                }
            }
            if (GUILayout.Button("Load"))
            {
                //Data = LoadData();
                SetCurWindow();
                LoadAllFiles();
                if (Paths.Count > 0)
                {
                    Data = Paths[0].TheData;
                    serialized = null;
                }

            }
            if (GUILayout.Button("Create New"))
            {
                if (EditorUtility.DisplayDialog("Create New", "You sure want to Create New File?", "Yes", "Cancel"))
                {
                    Data = Activator.CreateInstance<Z>();
                }
            }
            curRefsGo = (JsonRefsGo)EditorGUILayout.ObjectField("Ref Obj: ", curRefsGo, typeof(JsonRefsGo), false);
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
        protected void DeSelectInput()
        {
            GUI.FocusControl(GUI.GetNameOfFocusedControl());
        }

        protected virtual string FixName()
        {
            return "";
        }


        private void LoadAllFiles()
        {
            Paths = new List<ZDatas>();
            if (curRefsGo != null)
            {
                GetFilesOnRef();
                //for (int i = 0; i < Paths.Count; i++)
                //{
                //    Debug.Log(Paths[i].Path);
                //}
                return;
            }

            GetFilesAtPath();
            //for (int i = 0; i < Paths.Count; i++)
            //{
            //    Debug.Log(Paths[i].Path);
            //}
        }

        private void GetFilesOnRef()
        {
            //for (int i = 0; i < curRefsGo.RefObjects.Count; i++)
            //{
            //    //var fullPath = AssetDatabase.GetAssetPath(curRefsGo.RefObjects[i]);
            //    //Debug.Log(curRefsGo.RefObjects[i].GetType());
            //}
        }

        protected virtual string FolderSearch
        {
            get { return ""; }
        }
        private void GetFilesAtPath()
        {
            string startPath = Application.streamingAssetsPath;
            if (FolderSearch != "")
            {
                startPath += "/";
                startPath += FolderSearch;
            }
            AddAllInFolder(startPath);
            var allfolders = WorldsRunTimeUtilitys.FoldersOnFolder(startPath);
            for (int i = 0; i < allfolders.Count; i++)
            {
                AddAllInFolder(startPath + "/" + allfolders[i]);
                var allInFolder = WorldsRunTimeUtilitys.FoldersOnFolder(startPath + "/" + allfolders[i]);
                //Debug.Log(allfolders[i]+ " First search");
                if (allInFolder != null)
                {
                    for (int x = 0; x < allInFolder.Count; x++)
                    {
                        AddAllInFolder(startPath + "/" + allfolders[i] + "/" + allInFolder[x]);
                        //Debug.Log(allInFolder[x] + " Second Search");
                        var allTheInfolder = WorldsRunTimeUtilitys.FoldersOnFolder(startPath + "/" + allfolders[i] + "/" + allInFolder[x]);
                        if (allTheInfolder != null)
                        {
                            for (int z = 0; z < allTheInfolder.Count; z++)
                            {
                                //Debug.Log(allTheInfolder[z] + " Thirth Search");
                                AddAllInFolder(startPath + "/" + allfolders[i] + "/" + allInFolder[x] + "/" + allTheInfolder[z]);
                            }
                        }

                    }
                }

            }
        }

        private void AddAllInFolder(string path)
        {
            var allObjs = WorldsRunTimeUtilitys.ObjectsInfoInFolder(path);
            AddToList(allObjs, path);
        }
        private void AddToList(FileInfo[] files, string thePath)
        {
            if (files == null) return;
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Extension == ".json")
                {
                    if (AddObj(files[i]))
                    {
                        //Debug.Log(files[i].Name + " Custom Added");
                        var nObj = new ZDatas
                        {
                            Path = thePath + "/" + files[i].Name,
                           TheName = files[i].Name,
                            TheData = LoadData(thePath + "/" + files[i].Name)
                            
                        };
                        Paths.Add(nObj);
                        continue;
                    }
                    if (FixName() != "")
                    {
                        if (files[i].Name.Contains(FixName()))
                        {
                            //Debug.Log(files[i].Name + " Match");
                          
                            var nObj = new ZDatas
                            {
                                Path = thePath + "/" + files[i].Name,
                                TheName = files[i].Name,
                                TheData = LoadData(thePath + "/" + files[i].Name)
                            };
                            Paths.Add(nObj);
                        }
                    }
                }
            }
        }

        protected virtual bool AddObj(FileInfo theFileInfo)
        {
            return false;
        }
        protected virtual string SavePath()
        {
            return "";
        }

        private Z LoadData(string path)
        {
            return ToolForJson<Z>.LoadJsonData("", path, false);
            //Z data = Activator.CreateInstance<Z>();
            //data = ToolForJson<Z>.LoadJsonData("", path,false);
            //string filePath = EditorUtility.OpenFilePanel("Select MyData To Edit", SavePath() != "" ? Application.streamingAssetsPath + SavePath() : Application.streamingAssetsPath, "json");

            //if (!string.IsNullOrEmpty(filePath))
            //{
            //    var fin = filePath.Split("/"[0]);
            //    loadedName = fin[fin.Length - 1];
            //    string dataAsJason = File.ReadAllText(filePath);
            //    data = JsonUtility.FromJson<Z>(dataAsJason);
            //}
            //return data;
        }

        private void SaveLocalizationData(Z data, string path)
        {
            ToolForJson<Z>.SaveJsonData(data, path);
        }

        protected EditorWindow CurWindow;

        protected virtual void SetCurWindow()
        { }
        private const float list1Width = 250;
        private Vector2 scrollpos;
        private const float dataWidth1 = 350;
        private const float list2Width = 200;
        private const float listButtonsWidth = 150;

        private static Rect ListPosition = new Rect(10, StartPos1, list1Width, Height);
        protected static Rect SecondListPosition = new Rect(list1Width + dataWidth1 + 22, StartPos1, list2Width, Height);


        protected void DisplayList()
        {
            var curListHeight = ListPosition;
            curListHeight.height = CurWindow.position.height - StartPos1;
            curListHeight.height -= 10;
            GUILayout.BeginArea(curListHeight);

            scrollpos = EditorGUILayout.BeginScrollView(scrollpos, false, false, GUILayout.Width(curListHeight.width - 5), GUILayout.Height(curListHeight.height - 5));

            ListDisplay();

            EditorGUILayout.EndScrollView();

            GUILayout.EndArea();
        }

        protected void DisplayList2()
        {
            GUILayout.BeginArea(SecondListPosition);


            scrollpos2 = EditorGUILayout.BeginScrollView(scrollpos2, false, false, GUILayout.Width(SecondListPosition.width - 5), GUILayout.Height(SecondListPosition.height - 5));

            ListDisplay2();

            EditorGUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        protected virtual void ListDisplay()
        {

            GUILayout.Space(20);

            for (int i = 0; i < Paths.Count; i++)
            {
                if (GUILayout.Button(Paths[i].TheName, GUILayout.Width(listButtonsWidth), GUILayout.Height(20)))
                {
                    Data = Paths[i].TheData;
                    serialized = null;
                }
            }


        }
        protected virtual void ListDisplay2()
        { }
        protected void nText(string text)
        {
            GUILayout.Label(text);
        }
    }
}