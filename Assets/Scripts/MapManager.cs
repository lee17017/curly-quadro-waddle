using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public static MapManager current;

    // Prefab for Tile
    public GameObject tilePrefab;

    public GameObject[,] mapTiles;

    public Material tileMaterial;
    public Material hillMaterial;
    public Color tileColor;
    public Color hillColor;

    public SpriteRenderer p1, p2, p3, p4;

    public SpriteRenderer countdownSprite;

    public Sprite countdown2, countdown1, countdownGo;

    // Gridsize
    private int size = 27;

    public bool finished;

    void Awake()
    {
        current = this;
    }

	// Use this for initialization
	void Start ()
    {
        //current = this;
        mapTiles = new GameObject[size, (int)(size/16f*9)];

        p1.enabled = false;
        p2.enabled = false;
        p3.enabled = false;
        p4.enabled = false;

        countdownSprite.enabled = false;

        CreateMap();
    }
	
	// Update is called once per frame
	void Update () {
        AdjustCamera();
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

        for(int x=9; x<=17; x++)
        {
            for(int y=6; y<=8; y++)
            {
                mapTiles[x, y].GetComponent<Renderer>().material = hillMaterial;
            }
        }

        RevealMap();
    }

    void RevealMap()
    {
        StartCoroutine(GlowIntro());
        StartCoroutine(SpawnPlayers());
        /*
        foreach(GameObject obj in mapTiles)
        {
            StartCoroutine(ScaleTile(obj));
        }
        */
    }

    IEnumerator GlowIntro()
    {
        Color glow = Color.black;
        Color glowHill = Color.black;

        float timer = 0;
        while (timer < 2f)
        {
            glow = tileColor * timer;
            glowHill = hillColor * timer;
            timer += Time.deltaTime/3f;
            tileMaterial.SetColor("_EmissionColor", glow);
            hillMaterial.SetColor("_EmissionColor", glowHill);
            yield return null;
        }
    }

    IEnumerator ScaleTile(GameObject obj)
    {
        obj.transform.localScale = Vector3.zero;

        while(obj.transform.localScale.x < 1)
        {
            obj.transform.localScale += Vector3.one * Time.deltaTime/3;
            yield return null;
        }

        obj.transform.localScale = Vector3.one;
    }

    IEnumerator SpawnPlayers()
    {
        yield return new WaitForSeconds(2);

        float time = 0;

        Color p1Start, p2Start, p3Start, p4Start;

        p1Start = p1.color;
        p2Start = p2.color;
        p3Start = p3.color;
        p4Start = p4.color;

        p1.enabled = true;
        p2.enabled = true;
        p3.enabled = true;
        p4.enabled = true;

        p1.color = Color.black;
        p2.color = Color.black;
        p3.color = Color.black;
        p4.color = Color.black;

        float maxTime = 3f;

        while (time < maxTime)
        {
            p1.color = p1Start * time / maxTime;
            p2.color = p2Start * time / maxTime;
            p3.color = p3Start * time / maxTime;
            p4.color = p4Start * time / maxTime;
            time += Time.deltaTime;

            yield return null;
        }

        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {

        float time = 0;

        countdownSprite.enabled = true;

        // THREE

        countdownSprite.transform.localScale = Vector3.one;

        while (countdownSprite.transform.localScale.x > 0)
        {
            time += Time.deltaTime;
            if(time >= 1) { break; }
            if (countdownSprite.transform.localScale.x <= (Vector3.one * Time.deltaTime).x)
            {
                countdownSprite.enabled = false;
                break;
            }
            countdownSprite.transform.localScale -= Vector3.one * Time.deltaTime / 1.5f;
            yield return null;
        }

        // TWO

        time = 0;
        
        countdownSprite.sprite = countdown2;

        countdownSprite.transform.localScale = Vector3.one;

        while (countdownSprite.transform.localScale.x > 0)
        {
            time += Time.deltaTime;
            if (time >= 1) { break; }
            if (countdownSprite.transform.localScale.x <= (Vector3.one * Time.deltaTime).x)
            {
                countdownSprite.enabled = false;
                break;
            }
            countdownSprite.transform.localScale -= Vector3.one * Time.deltaTime / 1.5f;
            yield return null;
        }

        // ONE
        time = 0;
        countdownSprite.sprite = countdown1;

        countdownSprite.transform.localScale = Vector3.one;

        while (countdownSprite.transform.localScale.x > 0)
        {
            time += Time.deltaTime;
            if (time >= 1) { break; }
            if (countdownSprite.transform.localScale.x <= (Vector3.one * Time.deltaTime).x)
            {
                countdownSprite.enabled = false;
                break;
            }
            countdownSprite.transform.localScale -= Vector3.one * Time.deltaTime / 1.5f;
            yield return null;
        }


        // GO
        countdownSprite.sprite = countdownGo;

        countdownSprite.transform.localScale = Vector3.one;

        finished = true;

        while (countdownSprite.transform.localScale.x > 0)
        {
            if (countdownSprite.transform.localScale.x <= (Vector3.one * Time.deltaTime).x)
            {
                countdownSprite.enabled = false;
                break;
            }
            countdownSprite.transform.localScale -= Vector3.one * Time.deltaTime;
            yield return null;
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
