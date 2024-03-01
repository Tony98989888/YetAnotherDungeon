using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maps.Configs
{
    [Serializable]
    public struct MapData
    {
        public string MapName;
        public Tilemap Map;
    }
    
    [CreateAssetMenu(fileName = "NewMapConfig", menuName = "Tilemap system/MapConfig")]
    public class MapConfig : ScriptableObject
    {
        public List<MapData> MapData;
    }
}