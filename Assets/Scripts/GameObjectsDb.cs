using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace Worlds
{
    [System.Serializable]
    public class GameObjectsDb 
    {
        public List<ObjectDbData> Objects = new List<ObjectDbData>();

        public void Load(string fileName)
        {
            Objects = LoadJsonData(fileName).Objects;
        }
        private GameObjectsDb LoadJsonData(string fileName)
        {
            GameObjectsDb data = Activator.CreateInstance<GameObjectsDb>();
            var filePath = Application.streamingAssetsPath + "/" + fileName + ".json";
            if (!File.Exists(filePath))
            {
                Debug.Log("Didnt find " + filePath);
                return data;
            }
            var dataAsJason = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<GameObjectsDb>(dataAsJason);
            return data;
        }

    }
}