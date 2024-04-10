using System.Collections.Generic;
using System.IO;
using Maps.Configs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UserCreation;

public class MapDataHandler : MonoBehaviour
{
    const string MapDataFolderName = "MapData";
    // static readonly string MapDataSavePath = Path.Combine(Application.persistentDataPath, $"{MapDataFolderName}");

    public static void SaveMap(MapData mapData, PlayerData playerData)
    {
        var savePath = Path.Combine(playerData.SavePath, MapDataFolderName);
        Directory.CreateDirectory(savePath);
        
        MapSaveData saveData = new MapSaveData();
        foreach (var layer in mapData.Tilemaps)
        {
            TilemapSaveData tilemapSaveData = new TilemapSaveData(layer.name);
            foreach (var position in layer.cellBounds.allPositionsWithin)
            {
                TileBase tile = layer.GetTile(position);
                if (tile != null)
                {
                    TileSaveData tileSaveData = new TileSaveData()
                    {
                        Tile = tile,
                        Coord = position,
                        ColliderType = Tile.ColliderType.None,
                    };
                    tilemapSaveData.LayerData.Add(tileSaveData);
                }
            }
            
            saveData.TilemapDatas.Add(tilemapSaveData);
        }

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Path.Combine(savePath, $"{mapData.Index}.json"), json);
        Debug.Log($"Tilemap saved to - {Path.Combine(savePath, $"{mapData.Index}.json")}");
    }

    public static MapSaveData LoadMapData(MapIndex index, PlayerData playerData)
    {
        var savePath = Path.Combine(playerData.SavePath, MapDataFolderName);
        if (!Directory.Exists(savePath))
        {
            Debug.LogError($"{savePath} do not exist!");
            return null;
        }

        var filePath = Path.Combine(savePath, $"{index}.json");
        var savedData = File.ReadAllText(filePath);
        var mapSaveData = JsonUtility.FromJson<MapSaveData>(savedData);

        return mapSaveData;
    }

    public static GameObject LoadMap(MapSaveData mapSaveData)
    {
        //Create grid
        var grid = new GameObject($"{mapSaveData.Index}").AddComponent<Grid>();

        foreach (var tilemapSaveData in mapSaveData.TilemapDatas)
        {
            var tilemap = new GameObject($"{tilemapSaveData.Name}").AddComponent<Tilemap>();
            tilemap.AddComponent<TilemapRenderer>();
            tilemap.transform.SetParent(grid.transform);
            tilemap.ClearAllTiles();
            foreach (var tileSaveData in tilemapSaveData.LayerData)
            {
                tilemap.SetTile(tileSaveData.Coord, tileSaveData.Tile);
            }
        }

        return grid.GameObject();
    }
}