using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Worlds
{
    [CreateAssetMenu(menuName = "Worlds/DbTools/JsonRefsGo")]
    public class JsonRefsGo : ScriptableObject
    {
        public List<Object> RefObjects = new List<Object>();
    }
}