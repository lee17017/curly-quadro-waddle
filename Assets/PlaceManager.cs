using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceManager : MonoBehaviour {

    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;

    public GameObject player;

    public int playerID;

    public int place;

    void Awake()
    {
        gameObject.SetActive(Settings.IsActive(playerID));
    }

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Color col = player.transform.GetComponentInChildren<SpriteRenderer>().color;

        spriteRenderer.color = col;
    }

    // Update is called once per frame
    void Update() {

        spriteRenderer.enabled = MapManager.current.finished;

        ScoreManager.calculateRanking();

        place = ScoreManager.getRank(playerID);

        if(place < 1) { place = 1; }
        if(place > 4) { place = 4; }
        spriteRenderer.sprite = sprites[place - 1];
    }


}
