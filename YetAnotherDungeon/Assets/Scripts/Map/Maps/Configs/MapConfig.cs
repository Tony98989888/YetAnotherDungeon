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
        
    }
    
    [CreateAssetMenu(fileName = "NewMapConfig", menuName = "Tilemap system/MapConfig")]
    public class MapConfig : ScriptableObject
    {
        public List<MapData> MapData;
    }
}