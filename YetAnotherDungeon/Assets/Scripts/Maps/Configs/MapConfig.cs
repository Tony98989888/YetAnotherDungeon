using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maps.Configs
{
    [Serializable]
    public class MapData
    {
        public MapIndex Index;
        public List<Tilemap> Tilemaps;

        public MapData()
        {
            Index = MapIndex.None;
            Tilemaps = new List<Tilemap>();
        }
    }
    
    // Map config contains all maps
    [CreateAssetMenu(fileName = "NewMapConfig", menuName = "Tilemap system/MapConfig")]
    public class MapConfig : ScriptableObject
    {
        public List<MapData> MapData;
    }
}