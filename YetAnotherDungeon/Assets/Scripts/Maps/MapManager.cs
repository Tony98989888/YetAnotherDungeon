using Maps.Configs;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    public static MapSaveData CurrentMapData;
    public static GameObject CurrentMapObject;

    public MapConfig MapConfig;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public static void SwitchMap(MapIndex index)
    {
        if (CurrentMapData != null && CurrentMapObject != null)
        {
            Destroy(CurrentMapObject);
        }

        CurrentMapData = MapDataHandler.LoadMapData(index);
        CurrentMapObject = MapDataHandler.LoadMap(CurrentMapData);
    }

    public void Initialize()
    {
        // Save original maps for new save
        foreach (var data in MapConfig.MapData)
        {
            
        }
    }
    
}