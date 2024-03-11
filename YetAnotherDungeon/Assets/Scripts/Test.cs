using MapSystem.CustomMapSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    private Tilemap tilemap;

    public void Start()
    {
        tilemap = GameObject.FindObjectOfType<Tilemap>().GetComponentInChildren<Tilemap>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tileposition = tilemap.WorldToCell(mousePosition);

            if (tilemap.GetTile<FunctionalTile>(tileposition) != null)
            {
                Debug.Log(tilemap.GetTile<FunctionalTile>(tileposition).GetCustomTileType());
            }
        }
    }
}