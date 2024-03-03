using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Maps.Configs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public struct MapData
{
    public string Name;
    public List<Tilemap> Layers;
}


[Serializable]
public struct LayerData
{
    public string Name;
    public List<TileData> Tiles;

    public LayerData(string name, List<TileData> tiles)
    {
        Name = name;
        Tiles = tiles;
    }
}

[Serializable]
public struct TileData
{
    public TileBase Tile;
    public Vector3Int Position;
}

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    [SerializeField] List<TileConfig> m_tileConfigs;
    Dictionary<TileBase, TileConfig> m_tileInfo;

    public MapData TestMap;

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
        // LoadTileConfigs();
        LoadMap("Test1");
    }

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

    public void SaveMap(MapData mapData)
    {
        foreach (var layer in mapData.Layers)
        {
            var savePath = Path.Combine(Application.persistentDataPath, $"{mapData.Name}");
            Directory.CreateDirectory(savePath);
            LayerData layerData = new LayerData(layer.name, new List<TileData>());
            foreach (var position in layer.cellBounds.allPositionsWithin)
            {
                TileBase tile = layer.GetTile(position);
                if (tile != null)
                {
                    TileData tileData = new TileData()
                    {
                        Tile = tile,
                        Position = position,
                    };
                    layerData.Tiles.Add(tileData);
                }
            }

            string json = JsonUtility.ToJson(layerData);
            File.WriteAllText(Path.Combine(savePath, $"{layer.name}.json"), json);
            Debug.Log($"Tilemap saved to {Path.Combine(savePath, $"{layer.name}.json")}");
        }
    }

    public void LoadMap(string mapName)
    {
       
        var savePath = Path.Combine(Application.persistentDataPath, $"{mapName}");
        if (!Directory.Exists(savePath))
        {
            Debug.LogError($"Path: {savePath} do not exist!");
            return;
        }
        
        var grid = new GameObject($"{mapName}").AddComponent<Grid>();
        
        var filePaths = Directory.GetFiles(savePath);
        foreach (var path in filePaths)
        {
            var fileName = path.Split(Path.DirectorySeparatorChar.ToString()).Last();
            var tilemap = new GameObject($"{fileName}").AddComponent<Tilemap>();
            tilemap.AddComponent<TilemapRenderer>();
            tilemap.transform.SetParent(grid.transform);
            tilemap.ClearAllTiles();
            var savedData = File.ReadAllText(Path.Combine(savePath, $"{fileName}"));
            var mapData = JsonUtility.FromJson<LayerData>(savedData);
            foreach (var tile in mapData.Tiles)
            {
                tilemap.SetTile(tile.Position, tile.Tile);
            }
        }
    }
}