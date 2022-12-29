using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class WorldGeneration : MonoBehaviour
{
    //public Transform SnowMapPrefab;
    // Start is called before the first frame update
    public Transform GridTransform;
    public TileBase SnowTile;
    public Tilemap EnvironTileMap;
    public Tilemap snowTileMap;
    public float MAP_GEN_TIMER_LIMIT = 3f;
    public float PERLIN_ZOOM = 0.1f;
    public float SNOW_SPAWN_THRESHOLD = 0.1f;
    public float SNOW_INCREMENT = 0.05f;


    public Vector2Int MapSize = new Vector2Int(200, 200);
    Vector2Int startingPoint;
    float initialSnow = 0.0f;


    float genMapTimer = 0f;
    public float[,] snowMatrix;
    void Start()
    {
        snowMatrix = new float[MapSize.x, MapSize.y];
        for (int i = 1; i < MapSize.x; i++)
        {
            for (int j = 1; j < MapSize.y; j++)
            {
                snowMatrix[i, j] = 0.0f;
            }
        }

        GenerateInitialMap();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (initialSnow < 1f) InitialSnowProcedure();
    }


    private void InitialSnowProcedure()
    {
        for (int i = 1; i < MapSize.x; i++)
        {
            for (int j = 1; j < MapSize.y; j++)
            {
                if (snowMatrix[i, j] > 0f)
                {
                    snowMatrix[i, j] += SNOW_INCREMENT;
                    Vector3Int loc = new Vector3Int(i + startingPoint.x, j + startingPoint.y, 0);
                    snowTileMap.SetColor(loc, new Color(1, 1, 1, snowMatrix[i, j]));
                }
            }
        }
        initialSnow += SNOW_INCREMENT;
    }

    //A better way to probably have done this would be to individually spawn a tile based on random nummer
    private void GenerateInitialMap()
    {

        float newNoise = Random.Range(0f, 10000f);
        for (int i = 1; i < MapSize.x; i++)
        {
            for (int j = 1; j < MapSize.y; j++)
            {
                Vector3Int loc = new Vector3Int(i + startingPoint.x, j + startingPoint.y, 0);
                float rand = Mathf.PerlinNoise(i / (MapSize.x * PERLIN_ZOOM) + newNoise, j / (MapSize.y * PERLIN_ZOOM) + newNoise);
                //If there is tile underneath as the environment tile, ignore
                if (EnvironTileMap.GetTile(loc)) continue;

                if (rand < SNOW_SPAWN_THRESHOLD)
                {

                    snowMatrix[i, j] += SNOW_INCREMENT;
                    
                    snowTileMap.SetTile(loc, SnowTile);
                    snowTileMap.SetTileFlags(loc, TileFlags.None);
                    snowTileMap.SetColor(loc, new Color(1, 1, 1, snowMatrix[i, j]));

                    //Spawn Snow particleprefab

                }

            }
        }

    }

    public void GenerateSnow(Vector2 pos)
    {

    }
}
