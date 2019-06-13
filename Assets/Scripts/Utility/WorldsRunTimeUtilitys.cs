using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using Worlds.Ui;

namespace Worlds
{
    public static class WorldsRunTimeUtilitys
    {
        public static string RareText(string theText, Color color)
        {
            return "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + theText + "</color>";
        }


        public static List<string> ObjectsInFolder(string folderPath, string ending, out List<string> paths)
        {
            if (Directory.Exists(folderPath))
            {
                var thePaths = new List<string>();
                var theNames = new List<string>();
                var theFolderData = new DirectoryInfo(folderPath);
                var theDatas = theFolderData.GetFiles(ending);
                for (int a = 0; a < theDatas.Length; a++)
                {
                    theNames.Add(theDatas[a].Name);
                    thePaths.Add(theDatas[a].DirectoryName);
                }
                paths = thePaths;
                return theNames;
            }
            paths = null;
            return null;
        }
        public static FileInfo[] ObjectsInfoInFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                var theFolderData = new DirectoryInfo(folderPath);
                var theDatas = theFolderData.GetFiles();
                return theDatas;
            }
            return null;
        }
        public static List<string> FoldersOnFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {

                var theNames = new List<string>();
                var theFolderData = new DirectoryInfo(folderPath);
                var theDatas = theFolderData.GetDirectories();
                for (int a = 0; a < theDatas.Length; a++)
                {
                    theNames.Add(theDatas[a].Name);
                }
                return theNames;
            }
            return null;
        }

        public static string ReplaceColoms(string originalText)
        {
            var fixText = originalText.Replace("\\", "/");
            return fixText;
        }
    
    }
}