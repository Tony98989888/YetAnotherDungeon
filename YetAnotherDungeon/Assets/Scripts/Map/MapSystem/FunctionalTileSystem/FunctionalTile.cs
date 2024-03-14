using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MapSystem.CustomMapSystem
{
    public enum FunctionalTileType
    {
        PlayerStart,
        Obstacle,
    }

    public class FunctionalTile : TileBase
    {
        [SerializeField] private Sprite m_sprite;

        [SerializeField] private FunctionalTileType m_tileType;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);
            tileData.sprite = m_sprite;
        }

        public FunctionalTileType GetCustomTileType()
        {
            return m_tileType;
        }

        #region Editor

        [MenuItem("Assets/Create/CreateCustomTile")]
        public static void CreateCustomTile()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Tile Information", "New Tile Information", "Asset",
                "Save Tile Information", "Assets/Tilemap/Tiles");
            if (path == "")
            {
                return;
            }

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<FunctionalTile>(), path);
        }

        #endregion
    }
}