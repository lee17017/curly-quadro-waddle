using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    // Prefab for Tile
    public GameObject tilePrefab;

    public GameObject[,] mapTiles;

    // Gridsize
    public int size = 27;
    private int oldSize;

    // Camera Height
    public float camHeight;

	// Use this for initialization
	void Start ()
    {
        mapTiles = new GameObject[size, (int)(size/16f*9)];

        CreateMap();
    }
	
	// Update is called once per frame
	void Update () {
        AdjustCamera();

        // Size Changed
        if(oldSize != size)
        {
            CreateMap();
        }
	}

    void TrackValues()
    {
        oldSize = size;
    }

    // Creates All of the map tiles
    public void CreateMap()
    {
        // Clamp Values
        size = Mathf.Max(size, 10);

        //Destroy old tiles
        foreach(GameObject obj in mapTiles)
        {
            DestroyImmediate(obj);
        }

        // Create new Array
        mapTiles = new GameObject[size, (int)(size / 16f * 9)];

        // Create new tiles
        for(int x=0; x<size; x++)
        {
            for(int y=0; y < (int)(size / 16f * 9); y++)
            {
                Vector3 position = new Vector3(x, 0, y);
                mapTiles[x,y] = Instantiate(tilePrefab, position, Quaternion.identity, transform);
            }
        }
    }

    // Adjusts the Position and Rotation of Camera
    public void AdjustCamera()
    {
        // Calculate Height
        float height = (size/2f) / Mathf.Tan(Mathf.Deg2Rad*60)*1.5f;
        Camera.main.transform.position = new Vector3((size-1) / 2f, height, ((int)(size / 16f * 9) - 1) / 2f);
    }
}
