using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maps.Configs
{
    [CreateAssetMenu(fileName = "NewTileConfig", menuName = "Tilemap system/TileConfig")]
    public class TileConfig : ScriptableObject
    {
        public enum TileType
        {
            Walkable,
            Blocked
        }

        public TileType _TileType;
        public List<TileBase> Tiles;
    }
}
