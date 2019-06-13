using System.IO;
using UnityEditor;
using UnityEngine;

namespace Worlds
{
    public class WorldsEditorTools : MonoBehaviour
    {
        [MenuItem("Tools/SetToGoundLevel %f")]
        public static void SetToGoundLevel()
        {
            Transform[] trans = Selection.transforms;
            for (int i = 0; i < trans.Length; i++)
            {
                RaycastHit hit;
                if (Physics.Raycast(trans[i].position, -Vector3.up, out hit))
                {
                    trans[i].position = hit.point;
                }
            }
        }

        public static void RefreshDb()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        public static FileInfo[] GetFileInfos(string path)
        {
            var filesAtFolder = new DirectoryInfo(path);
            var allFiles = filesAtFolder.GetFiles();
            return allFiles;
        }

        public static FileInfo[] GetFileInfoFull(string path)
        {
            var filesAtFolder = new DirectoryInfo(path);
            var allFiles = filesAtFolder.GetFiles("*.*", SearchOption.AllDirectories);
            return allFiles;
        }
        public static string ExportFolder(string folderPath, string FolderName)
        {
            var pathToExport = EditorUtility.OpenFolderPanel("Select Folder to export To ", "", "");

            if (!string.IsNullOrEmpty(pathToExport))
            {
                var savePath = pathToExport + "/" + FolderName;
                int increment = 1;
                if (Directory.Exists(savePath))
                {
                    var finalName = savePath + "_" + increment;
                    while (Directory.Exists(finalName))
                    {
                        increment++;
                        finalName = savePath + "_" + increment;
                    }
                    savePath = finalName;

                }
                FileUtil.CopyFileOrDirectory(folderPath, savePath);
                return savePath;
            }
            return "";
        }
    }
}