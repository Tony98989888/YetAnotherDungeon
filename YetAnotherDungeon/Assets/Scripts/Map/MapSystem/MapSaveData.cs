using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum MapIndex
{
    None,
    Startup,
    Village,
    CombatTutorial,
}

[Serializable]
public class TilemapSaveData
{
    public string Name;
    public List<TileSaveData> LayerData;

    public TilemapSaveData(string name)
    {
        Name = name;
        LayerData = new List<TileSaveData>();
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
        Index = MapIndex.None;
        TilemapDatas = new List<TilemapSaveData>();
    }
}