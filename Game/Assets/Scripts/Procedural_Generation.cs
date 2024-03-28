using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Procedural_Generation : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase grassTile;
    public TileBase dirtTile;
    public GameObject collectiblePrefab;
    private int levelWidth;
    public GameObject finishLine;
    public int baseLineHeight;
    public int maxHillHeight;
    public int maxHillWidth;
    public int barrierHeight;
    public int platformFrequency;
    public int platformLength;
    public int safeZoneWidth; 

    private List<Vector3Int> collectiblePositions; // List to store potential collectible positions

    void Start()
    {
        int currentDifficultyIndex = PlayerPrefs.GetInt("DifficultyIndex");
        if(currentDifficultyIndex == 0){
            levelWidth = 100;
            finishLine.transform.position = new Vector2(93,8);
        } else if (currentDifficultyIndex == 1){
            levelWidth = 150;
            finishLine.transform.position = new Vector2(143,8);
        } else if (currentDifficultyIndex == 2){
            levelWidth = 200;
            finishLine.transform.position = new Vector2(193,8);
        }
        collectiblePositions = new List<Vector3Int>();
        GenerateLevel();
        PlaceCollectibles();
    }

    void GenerateLevel()
    {
        // Create the base line
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < baseLineHeight; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), dirtTile);
            }
            tilemap.SetTile(new Vector3Int(x, baseLineHeight, 0), grassTile);
        }

        // Add random hills and floating platforms
        int currentX = 0;
        while (currentX < levelWidth - safeZoneWidth)
        {
            // Hills
            int hillWidth = Random.Range(2, maxHillWidth);
            int hillHeight = Random.Range(1, maxHillHeight);

            if (currentX + hillWidth < levelWidth - safeZoneWidth) // Ensure hills fit within level width
            {
                CreateHill(currentX, hillWidth, hillHeight);
                currentX += hillWidth;
            }

            // Floating platforms
            if (Random.Range(0, platformFrequency) < 5 && currentX + platformLength < levelWidth - safeZoneWidth) // Random chance to create a platform
            {
                CreatePlatform(currentX, baseLineHeight + Random.Range(2, maxHillHeight + 2), platformLength);
                currentX += platformLength;
            }

            currentX += Random.Range(1, 3); // Random gap after each hill or platform
        }

        // Create tall barriers at the start and end of the level
        CreateBarrier(-1, barrierHeight); // Start barrier
        CreateBarrier(levelWidth, barrierHeight); // End barrier is now placed after the safe zone
    }

    void CreateHill(int startX, int width, int height)
    {
        for (int x = startX; x < startX + width; x++)
        {
            for (int y = baseLineHeight; y < baseLineHeight + height; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), dirtTile);
            }
            tilemap.SetTile(new Vector3Int(x, baseLineHeight + height, 0), grassTile);
        }
        // Add the position of the top of the hill to the collectiblePositions list
        collectiblePositions.Add(new Vector3Int(startX + width / 2, baseLineHeight + height, 0));
    }

    void CreatePlatform(int startX, int height, int length)
    {
        for (int x = startX; x < startX + length; x++)
        {
            tilemap.SetTile(new Vector3Int(x, height, 0), grassTile);
        }
        // Add the position of the platform to the collectiblePositions list
        collectiblePositions.Add(new Vector3Int(startX + length / 2, height, 0));
    }

    void CreateBarrier(int startX, int height)
    {
        for (int y = 0; y < height; y++)
        {
            tilemap.SetTile(new Vector3Int(startX, y, 0), dirtTile);
        }
        tilemap.SetTile(new Vector3Int(startX, height, 0), grassTile);
    }

    void PlaceCollectibles()
    {
        float collectibleHeight = collectiblePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        Transform collectibleParent = new GameObject("Collectibles").transform; // Create a parent object for collectibles
        int count = 0;

        foreach (var position in collectiblePositions)
        {
            if(count%2==0){
                // Adjust the y position to account for the height of the collectible
            Vector3 worldPosition = tilemap.CellToWorld(new Vector3Int(position.x, position.y + 1, position.z));
            worldPosition += new Vector3(0, collectibleHeight / 2, 0); // Offset to place collectible on top

            // Instantiate the collectible as a child of the collectibleParent
            GameObject collectibleInstance = Instantiate(collectiblePrefab, worldPosition, Quaternion.identity);
            collectibleInstance.transform.SetParent(collectibleParent);
            }
            count++;
            
        }
    }

}
