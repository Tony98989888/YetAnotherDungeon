
using Maps.Configs;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UserCreation;

public class MapManager : Singleton<MapManager>
{
    public static MapSaveData CurrentMapData;
    public static GameObject CurrentMapObject;
    
    [ShowOnly, SerializeField]
    MapConfig m_mapConfig;

    void Awake()
    {
        EventBetter.Listen(this, (OnBeginNewGame newGame) =>
        {
            SaveAllRawMaps(newGame.PlayerData);
        });

        if (m_mapConfig == null)
        {
            m_mapConfig = Resources.Load<MapConfig>("MapConfig");
        }
    }

    void SaveAllRawMaps(PlayerData playerData)
    {
        foreach (var map in m_mapConfig.MapData)
        {
            MapDataHandler.SaveMap(map, playerData);
        }
    }


    // public static void SwitchMap(MapIndex index)
    // {
    //     if (CurrentMapData != null && CurrentMapObject != null)
    //     {
    //         Destroy(CurrentMapObject);
    //     }
    //
    //     CurrentMapData = MapDataHandler.LoadMapData(index);
    //     CurrentMapObject = MapDataHandler.LoadMap(CurrentMapData);
    // }

    // public void Initialize()
    // {
    //     // Save original maps for new save
    //     foreach (var data in MapConfig.MapData)
    //     {
    //         // Check if map data exist for current save if exist then we do not save
    //     }
    // }
    
}