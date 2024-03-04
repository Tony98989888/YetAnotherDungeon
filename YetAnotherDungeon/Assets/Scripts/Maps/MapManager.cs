using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Maps.Configs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public struct _MapData
{
    public string Name;
    public List<Tilemap> Layers;
}


[Serializable]
public struct LayerData
{
    public string Name;
    public List<_TileData> Tiles;

    public LayerData(string name, List<_TileData> tiles)
    {
        Name = name;
        Tiles = tiles;
    }
}

[Serializable]
public struct _TileData
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
}