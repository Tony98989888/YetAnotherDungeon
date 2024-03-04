using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum MapIndex
{
    None,
    Startup,
    Village,
}

[Serializable]
public class TilemapSaveData
{
    public string Name;
    public List<TileSaveData> LayerData;

    public TilemapSaveData()
    {
        LayerData = new List<TileSaveData>();
    }

    public TilemapSaveData(string name)
    {
        Name = name;
        LayerData = new List<TileSaveData>();
    }

    public TilemapSaveData(List<TileSaveData> layerData)
    {
        LayerData = layerData;
    }
}

[Serializable]
public class TileSaveData
{
    public TileBase Tile;
    public Vector3Int Coord;
    public Tile.ColliderType ColliderType;
}

[Serializable]
public class MapSaveData
{
    public MapIndex Index;
    public List<TilemapSaveData> TilemapDatas;

    public MapSaveData()
    {
        TilemapDatas = new List<TilemapSaveData>();
    }

    public MapSaveData(List<TilemapSaveData> tilemapDatas)
    {
        TilemapDatas = tilemapDatas;
    }
}