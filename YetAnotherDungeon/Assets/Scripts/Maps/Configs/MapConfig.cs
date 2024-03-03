using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maps.Configs
{
    // Map = Combination of tile maps
    [Serializable]
    public struct MapData
    {
        public string Name;
        public List<Tilemap> Layers;
    }
    
    // Map config contains all maps
    [CreateAssetMenu(fileName = "NewMapConfig", menuName = "Tilemap system/MapConfig")]
    public class MapConfig : ScriptableObject
    {
        public List<MapData> MapData;
    }
}