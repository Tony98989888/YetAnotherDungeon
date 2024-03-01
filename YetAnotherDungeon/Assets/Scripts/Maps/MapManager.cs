using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class TileData
{
    public Vector3Int Position;
    public string Name;
}

[Serializable]
public class TilemapData
{
    public List<TileData> Tiles = new List<TileData>();
}

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }
    
    public Tilemap[] Maps; // 在Inspector中分配多个Tilemap
    public Transform Player; // 分配玩家对象
    
    public string SavePath = "tilemap_save.json";
    
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

    public void SaveTilemap(Tilemap map)
    {
        TilemapData tilemapData = new TilemapData();
        BoundsInt bounds = map.cellBounds;
        TileBase[] allTiles = map.GetTilesBlock(bounds);
        
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    tilemapData.Tiles.Add(new TileData
                    {
                        Position = new Vector3Int(bounds.xMin + x, bounds.yMin + y, 0),
                        Name = tile.name
                    });
                }
            }
        }
        
        string json = JsonUtility.ToJson(tilemapData);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, SavePath), json);

        Debug.Log($"Tilemap saved to {Path.Combine(Application.persistentDataPath, SavePath)}");
    }
    
    public void LoadTilemap()
    {
        string filePath = Path.Combine(Application.persistentDataPath, SavePath);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            TilemapData tilemapData = JsonUtility.FromJson<TilemapData>(json);

            foreach (TileData tileData in tilemapData.Tiles)
            {
                TileBase tile = Resources.Load<TileBase>(tileData.Name);
                // Tilemap.SetTile(tileData.position, tile);
            }

            Debug.Log("Tilemap loaded from file.");
        }
        else
        {
            Debug.LogError("No save file found.");
        }
    }
}
