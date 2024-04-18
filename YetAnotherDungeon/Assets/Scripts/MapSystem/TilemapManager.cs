using Maps.Configs;
using UnityEngine;
using UserCreation;

public class TilemapManager : Singleton<TilemapManager>
{
    public static MapSaveData CurrentMapData;
    public static GameObject CurrentMapObject;
    
    [ShowOnly, SerializeField]
    MapConfig m_mapConfig;

    protected override void Awake()
    {
        base.Awake();
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
            MapSystemUtilities.SaveMap(map, playerData);
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