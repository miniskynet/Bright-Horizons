//The initial code was generated with assistance from ChatGPT, an AI developed by OpenAI (OpenAI, 2023).
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Procedural_Generation : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase grassTile;
    public TileBase dirtTile;
    public GameObject collectiblePrefab;
    public Sprite[] numberSprites;
    public Sprite[] letterSprites;
    public Sprite[] colorSprites;
    public GameObject trapPrefab;
    private int levelWidth;
    public GameObject finishLine;
    private int baseLineHeight = 5;
    private int maxHillHeight;
    private int maxHillWidth;
    private int barrierHeight = 20;
    private int platformFrequency;
    private int platformLength;
    private int safeZoneWidth = 10;
    private int elementRange;

    private List<Vector3Int> itemPositions; // List to store potential collectible positions

    void Start()
    {
        //based on user selected difficulty level,
        //change the terrain generation values
        int currentDifficultyIndex = PlayerPrefs.GetInt("DifficultyIndex");
        if (currentDifficultyIndex == 0)
        {
            levelWidth = 100;
            finishLine.transform.position = new Vector2(93, 8);
            maxHillHeight = 2;
            maxHillWidth = 4;
            platformFrequency = 15;
            platformLength = 3;
            elementRange = 10;
        }
        else if (currentDifficultyIndex == 1)
        {
            levelWidth = 150;
            finishLine.transform.position = new Vector2(143, 8);
            maxHillHeight = 3;
            maxHillWidth = 5;
            platformFrequency = 15;
            platformLength = 4;
            elementRange = 5;
        }
        else if (currentDifficultyIndex == 2)
        {
            levelWidth = 200;
            finishLine.transform.position = new Vector2(193, 8);
            maxHillHeight = 4;
            maxHillWidth = 5;
            platformFrequency = 2;
            platformLength = 5;
            elementRange = 3;
        }
        itemPositions = new List<Vector3Int>();
        GenerateLevel();
        PlaceItems();
    }

    void GenerateLevel()
    {
        //create the base line
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < baseLineHeight; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), dirtTile);
            }
            tilemap.SetTile(new Vector3Int(x, baseLineHeight, 0), grassTile);
        }

        //add random hills and floating platforms
        //currentX is used to iterate through the width of the level
        int currentX = 0;
        while (currentX < levelWidth - safeZoneWidth)
        {
            int hillWidth = Random.Range(2, maxHillWidth);
            int hillHeight = Random.Range(1, maxHillHeight);

            //ensure the hills stay within the level width
            if (currentX + hillWidth < levelWidth - safeZoneWidth)
            {
                CreateHill(currentX, hillWidth, hillHeight);
                currentX += hillWidth;
            }

            int currentDifficultyIndex = PlayerPrefs.GetInt("DifficultyIndex");

            //add platforms only on standard and challenging difficulty levels
            if (currentDifficultyIndex == 1 || currentDifficultyIndex == 2)
            {
                //platforms are generated based on set frequency
                //however based on implemented logic, higher the frequency, lower the number of platforms 
                if (Random.Range(0, platformFrequency) < 5 && currentX + platformLength < levelWidth - safeZoneWidth)
                {
                    CreatePlatform(currentX, baseLineHeight + Random.Range(2, maxHillHeight + 2), platformLength);
                    currentX += platformLength;
                }
            }
            //add a random space between either hill or platform
            currentX += Random.Range(1, elementRange);
        }

        //create tall barriers at the start and end of the level
        //to ensure that the player does not fall off the game world
        CreateBarrier(-1, barrierHeight);
        CreateBarrier(levelWidth, barrierHeight);
    }

    void CreateHill(int startX, int width, int height)
    {
        //create hill using dirt and grass tiles
        for (int x = startX; x < startX + width; x++)
        {
            for (int y = baseLineHeight; y < baseLineHeight + height; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), dirtTile);
            }
            tilemap.SetTile(new Vector3Int(x, baseLineHeight + height, 0), grassTile);
        }
        //add top of the hill as a potential location for item placement
        itemPositions.Add(new Vector3Int(startX + width / 2, baseLineHeight + height, 0));
    }

    void CreatePlatform(int startX, int height, int length)
    {
        //create platform using grass tiles
        for (int x = startX; x < startX + length; x++)
        {
            tilemap.SetTile(new Vector3Int(x, height, 0), grassTile);
        }
        //add top of the platofrm as a potential location for item placement
        itemPositions.Add(new Vector3Int(startX + length / 2, height, 0));
    }

    void CreateBarrier(int startX, int height)
    {
        //creates tall barrier with dirt and grass tiles
        for (int y = 0; y < height; y++)
        {
            tilemap.SetTile(new Vector3Int(startX, y, 0), dirtTile);
        }
        tilemap.SetTile(new Vector3Int(startX, height, 0), grassTile);
    }

    void PlaceItems()
    {
        int currentLearningIndex = PlayerPrefs.GetInt("LearningIndex");
        //generate items based on the user selected learning preference
        if (currentLearningIndex == 0)
        {
            collectiblePrefab.GetComponent<SpriteRenderer>().sprite = numberSprites[Level_Manager.currentLevel];
            collectiblePrefab.transform.localScale = new Vector3(0.04f, 0.04f, 1);
        }
        else if (currentLearningIndex == 1)
        {
            collectiblePrefab.GetComponent<SpriteRenderer>().sprite = letterSprites[Level_Manager.currentLevel];
            collectiblePrefab.transform.localScale = new Vector3(2, 2, 1);
        }
        else if (currentLearningIndex == 2)
        {
            collectiblePrefab.GetComponent<SpriteRenderer>().sprite = colorSprites[Level_Manager.currentLevel];
            collectiblePrefab.transform.localScale = new Vector3(0.04f, 0.04f, 1);
        }
        //add the generated items into a parent object
        Transform collectibleParent = new GameObject("Collectibles").transform;
        int count = 0;

        foreach (var position in itemPositions)
        {
            if (count % 2 == 0)
            {
                //adjust the y position to account for the height of the collectible
                Vector3 worldPosition = tilemap.CellToWorld(new Vector3Int(position.x, position.y + 2, position.z));

                //instantiate the collectible as a child of the collectibleParent
                GameObject collectibleInstance = Instantiate(collectiblePrefab, worldPosition, Quaternion.identity);
                collectibleInstance.transform.SetParent(collectibleParent);
            }
            count++;

        }
    }


}
