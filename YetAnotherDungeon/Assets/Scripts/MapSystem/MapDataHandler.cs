using System.IO;
using Maps.Configs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDataHandler : MonoBehaviour
{
    private const string MapDataFolderName = "MapData";
    static readonly string MapDataSavePath = Path.Combine(Application.persistentDataPath, $"{MapDataFolderName}");

    public static void SaveMapData(MapData mapData)
    {
        Directory.CreateDirectory(MapDataSavePath);
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
        File.WriteAllText(Path.Combine(MapDataSavePath, $"{mapData.Index}.json"), json);
        Debug.Log($"Tilemap saved to - {Path.Combine(MapDataSavePath, $"{mapData.Index}.json")}");
    }

    public static MapSaveData LoadMapData(MapIndex index)
    {
        if (!Directory.Exists(MapDataSavePath))
        {
            Debug.LogError($"{MapDataSavePath} do not exist!");
            return null;
        }

        //Create grid
        var grid = new GameObject($"{index}").AddComponent<Grid>();

        var filePath = Path.Combine(MapDataSavePath, $"{index}.json");
        var savedData = File.ReadAllText(filePath);
        var mapSaveDa = JsonUtility.FromJson<MapSaveData>(savedData);

        foreach (var tilemapSaveData in mapSaveDa.TilemapDatas)
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

        return mapSaveDa;
    }
}