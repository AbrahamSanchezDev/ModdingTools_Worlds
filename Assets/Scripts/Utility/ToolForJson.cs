using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Worlds
{
    public static class ToolForJson<T>
    {
        public static void CheckForFolder(string path)
        {
            if (!Directory.Exists(Application.streamingAssetsPath + "/" + path))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath + "/" + path);
                //Debug.Log("Didnt Exist");
            }
            //else
            //{
            //    Debug.Log("Already Existed");
            //}
        }

        public static bool FileExist(string path)
        {
            return File.Exists(Application.streamingAssetsPath + "/" + path + ".json");
        }
        public static bool SaveJsonData(T data, string path, bool addStreamingAssets = true)
        {
            var filePath = Application.streamingAssetsPath + "/" + path + ".json";
            if (!addStreamingAssets)
            {
                filePath = path + ".json";
            }

            //if (Directory.Exists(filePath)) return false;
            var dataAsJason = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, dataAsJason);

#if UNITY_EDITOR
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
#endif
            return true;
        }

        public static List<T> LoadAllOnPath(string folderPath)
        {
            var pathToFiles = Application.streamingAssetsPath + "/" + folderPath;
            var dir = new DirectoryInfo(pathToFiles);
            //Debug.Log(dir.DisplayText);
            var curFilesPaths = new List<string>();
            var buildingDatas = new List<T>();
            //Get All json Files on the structuresPath
            if (dir.Exists)
            {
                var paths = dir.GetFiles("*.*");

                for (int i = 0; i < paths.Length; i++)
                {
                    if (paths[i].Extension == ".json")
                    {
                        // Debug.Log(paths[i].DisplayText);
                        var nDic = paths[i].Name.Remove(paths[i].Name.Length - 5);
                        //Debug.Log(nDic[0]);
                        curFilesPaths.Add(nDic);
                    }
                }
            }
            //Create Datas
            for (int i = 0; i < curFilesPaths.Count; i++)
            {
                buildingDatas.Add(LoadJsonData(folderPath + "/" + curFilesPaths[i]));
            }
            return buildingDatas;
        }
        public static T LoadJsonData(string path, string forcePath = "", bool addStreamingAssets = true)
        {
            T data = Activator.CreateInstance<T>();
            var filePath = Application.streamingAssetsPath + "/" + path + ".json";
            if (forcePath != "")
            {
                filePath = addStreamingAssets ? Application.streamingAssetsPath + "/" + forcePath : forcePath;
            }
            if (!File.Exists(filePath))
            {
                Debug.Log("Didnt find " + filePath);
                return data;
            }
            var dataAsJason = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<T>(dataAsJason);
            return data;
        }
    }
}