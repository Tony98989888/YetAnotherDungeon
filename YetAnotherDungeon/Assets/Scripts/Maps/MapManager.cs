using System;
using System.Collections.Generic;
using System.IO;
using Maps.Configs;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[Serializable]
public class TileData
{
    public TileBase Tile;
    public Vector3Int Position;

    [Serializable]
    public class TilemapData
    {
        public string MapName;
        public List<TileData> Tiles = new List<TileData>();
    }

    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance { get; private set; }

        // For all tiles info
        [SerializeField] List<TileConfig> m_tileConfigs;
        Dictionary<TileBase, TileConfig> m_tileInfo;

        // Current map data

        public Tilemap TestMap;

        private void Awake()
        {
            // 单例模式
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            LoadTileConfigs();

            // SaveTilemap(TestMap);
            LoadTilemap("Tilemap", TestMap);
        }

        // Used to get all tile config information
        private void LoadTileConfigs()
        {
            m_tileInfo = new Dictionary<TileBase, TileConfig>();
            foreach (var config in m_tileConfigs)
            {
                foreach (var tile in config.Tiles)
                {
                    m_tileInfo.Add(tile, config);
                }
            }
        }

        public TilemapData SaveTilemap(Tilemap tilemap)
        {
            TilemapData tilemapData = new TilemapData();

            foreach (var position in tilemap.cellBounds.allPositionsWithin)
            {
                TileBase tile = tilemap.GetTile(position);
                if (tile != null)
                {
                    TileData tileData = new TileData()
                    {
                        Tile = tile,
                        Position = position,
                    };
                    tilemapData.Tiles.Add(tileData);
                }
            }

            string json = JsonUtility.ToJson(tilemapData);
            File.WriteAllText(Path.Combine(Application.persistentDataPath, $"{tilemap.name}.json"), json);

            Debug.Log($"Tilemap saved to {Path.Combine(Application.persistentDataPath, $"{tilemap.name}.json")}");
            return tilemapData;
        }

        public void LoadTilemap(string tilemapName, Tilemap tileMap)
        {
            tileMap.ClearAllTiles();
            var savedData = File.ReadAllText(Path.Combine(Application.persistentDataPath, $"{tilemapName}.json"));
            var mapData = JsonUtility.FromJson<TilemapData>(savedData);
            foreach (var tile in mapData.Tiles)
            {
                tileMap.SetTile(tile.Position, tile.Tile);
            }
        }
    }
}