using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Worlds.Monsters;

namespace Worlds
{
    public class MonstersModTools : EditorWindow
    {
        private static string path
        {
            get
            {
                return Application.streamingAssetsPath + "/MonsterBundles";
            }
        }
        private static string mosnterModFolder
        {
            get
            {
                return Application.streamingAssetsPath + "/MonstersMod";
            }
        }

        private static int curBundlesInMosntersMod;
        private static string manifestText = ".manifest";
        [MenuItem("Worlds/General Monsters Mod Tools")]
        public static void ShowMonstersModTools()
        {
            GetWindow(typeof(MonstersModTools));
            GetAllBundles();
        }

        private MonstersToRemove _curMonstersToRemove;
        protected void OnGUI()
        {
            GUILayout.Label("Generates all the asset bundles");
            if (GUILayout.Button("Create Monsters Bundles"))
            {
                CreateAssetBundles.BuildAssetBundles();

            }
            GUILayout.Label("Make sure that there are no other bundle files \n" +
                            " in the Streaming Assets");
            if (GUILayout.Button("Move Bundles to Folders"))
            {
                MoveMonsterBundlesToFolders();
            }
            GUILayout.Label("Current Bundles in MonstersMod: " + curBundlesInMosntersMod);
            if (GUILayout.Button("GetAllBundles in MonstersMod"))
            {
                GetAllBundles();
            }

            GUILayout.Space(10);
            GUILayout.Label("WARNING : this will delete all the bundle files\n" +
                            "that are in the MonstersMod Folder :");
            if (GUILayout.Button("Delete All Bundles in MonstersMod"))
            {
                DeleteAllBundlesInMonstersMod();
                GetAllBundles();
            }

            GUILayout.Space(20);

            GUILayout.Label("Set List of monsters to remove from the original");
            _curMonstersToRemove = (MonstersToRemove)EditorGUILayout.ObjectField(_curMonstersToRemove, typeof(MonstersToRemove), true);
            GUILayout.Label("Select where to export the Monsters Mod");
            if (GUILayout.Button("Export Monsters Folder", GUILayout.Height(50)))
            {
                ExportMonstersFiles();
            }
        }

        private static bool MosntersFolderExist
        {
            get
            {
                var exist = Directory.Exists(mosnterModFolder);
                if (!exist)
                {
                    Debug.Log("no folder at : " + mosnterModFolder);
                }
                return exist;
            }
        }
        private static void GetAllBundles()
        {
            if (!MosntersFolderExist)
            {
                return;
            }
            var curFiles = allBundlesFilesIn(mosnterModFolder, false);
            curBundlesInMosntersMod = curFiles.Count;
        }

        private static void DeleteAllBundlesInMonstersMod()
        {
            if (!MosntersFolderExist)
            {
                return;
            }
            var curFiles = allBundlesFilesIn(mosnterModFolder);
            if (curFiles.Count > 0)
            {
                Debug.Log("Deleting: " + curFiles.Count + " Files");
                for (int i = 0; i < curFiles.Count; i++)
                {
                    FileUtil.DeleteFileOrDirectory(curFiles[i]);
                }
                WorldsEditorTools.RefreshDb();
            }
        }
        private static List<string> allBundlesFilesIn(string mainFolder, bool checkManifest = true)
        {
            var infos = WorldsRunTimeUtilitys.FoldersOnFolder(mainFolder);
            List<string> curFiles = new List<string>();
            for (int i = 0; i < infos.Count; i++)
            {
                var fillFileName = mainFolder + "/" + infos[i];
                var allFiles = BunddlesInFolder(fillFileName, checkManifest);
                for (int j = 0; j < allFiles.Count; j++)
                {
                    curFiles.Add(allFiles[j]);
                }
            }
            return curFiles;
        }

        private static List<string> BunddlesInFolder(string mainFolder, bool checkManifest = true)
        {
            List<string> curFiles = new List<string>();
            var fillFileName = mainFolder + "/";
            var objsInfolders = WorldsEditorTools.GetFileInfos(fillFileName);
            for (int x = 0; x < objsInfolders.Length; x++)
            {
                if (objsInfolders[x].Extension == ".meta")
                {
                    continue;
                }
                if (objsInfolders[x].Name == "StreamingAssets" || objsInfolders[x].Name == "StreamingAssets.manifest")
                {
                    continue;
                }
                if (objsInfolders[x].Extension == "")
                {
                    curFiles.Add(objsInfolders[x].ToString());
                }
                if (checkManifest && objsInfolders[x].Extension == manifestText)
                {
                    curFiles.Add(objsInfolders[x].ToString());
                }
            }
            return curFiles;
        }

        public static void MoveMonsterBundlesToFolders(string saveFolderName = "MonstersMod")
        {
            var thePath = Application.streamingAssetsPath;
            //Check if folder exists
            if (!Directory.Exists(thePath))
            {
                return;
            }
            var bundleFiles = BunddlesInFolder(thePath);
            int startToRemove = thePath.Length + 1;
            for (int i = 0; i < bundleFiles.Count; i++)
            {
                var curName = bundleFiles[i].Remove(0, startToRemove);
                var originalName = curName;
                if (curName.Contains(manifestText))
                {
                    var nLoot = curName.Split("."[0]);
                    curName = nLoot[0];
                }
                var finalNameForFolder = curName;
                if (curName.Contains("_"))
                {
                    var charst = finalNameForFolder.ToCharArray();
                    var nextPos = curName.IndexOf("_");
                    charst[nextPos + 1] = char.ToUpper(charst[nextPos + 1]);
                    finalNameForFolder = new string(charst);
                }
                else
                {
                    var chars = finalNameForFolder.ToUpperInvariant();
                    finalNameForFolder = chars;
                }
                //Check if folder exists
                var curContentPath = Application.streamingAssetsPath + "/" + saveFolderName + "/" + finalNameForFolder;
                var fixContentPath = curContentPath.Replace("\\", "/");
                //If not create the folder
                if (!Directory.Exists(fixContentPath))
                {
                    Debug.Log(fixContentPath);
                    Directory.CreateDirectory(fixContentPath);
                }
                FileUtil.MoveFileOrDirectory(bundleFiles[i], fixContentPath + "/" + originalName);
            }
            Debug.Log("Moved : " + bundleFiles.Count);
            WorldsEditorTools.RefreshDb();
        }

        public void ExportMonstersFiles()
        {
            var nPath = WorldsEditorTools.ExportFolder(mosnterModFolder, "MonstersMod");
            if (string.IsNullOrEmpty(nPath))
            {
                return;
            }
            //Remove all metas at path
            var allfiles = WorldsEditorTools.GetFileInfoFull(nPath);
            for (int i = 0; i < allfiles.Length; i++)
            {
                if (allfiles[i].Extension == ".meta")
                {
                    FileUtil.DeleteFileOrDirectory(allfiles[i].FullName);
                }
            }
            if (_curMonstersToRemove != null)
            {
                for (int i = 0; i < _curMonstersToRemove.MonstersIds.Count; i++)
                {
                    CreateRemoveAt(nPath, _curMonstersToRemove.MonstersIds[i]);
                }
            }
            Debug.Log("xported To : " + nPath);
        }

        private void CreateRemoveAt(string savePath, int index)
        {
            var fullPath = savePath + "/" + index + "_Removing";
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            var nMonsterInfo = new MonsterBaseData();
            nMonsterInfo.Id = index;
            nMonsterInfo.Name = "Removed";
            string dataAsJason = JsonUtility.ToJson(nMonsterInfo, true);
            File.WriteAllText(fullPath + "/" + index + "_Remove.json", dataAsJason);
        }
        public static void BuildAssetBundles()
        {
            var thePath = Application.streamingAssetsPath;
            if (!Directory.Exists(thePath))
            {
                Directory.CreateDirectory(thePath);
            }
            BuildPipeline.BuildAssetBundles(thePath,
                BuildAssetBundleOptions.None,
                BuildTarget.StandaloneWindows);
        }
    }
}