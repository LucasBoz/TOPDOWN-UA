using UnityEngine;
using UnityEngine.Tilemaps;

public class TileClickDetector : MonoBehaviour
{
    
    public Tilemap tilemap;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(worldPoint);
            TileBase clickedTile = tilemap.GetTile(cellPosition);

            if (clickedTile != null)
            {
                Debug.Log("Clicked on target tile!");
                Debug.Log(clickedTile.name);
                
                
            }
        }
    }
    
}
