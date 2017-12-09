using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationManager : MonoBehaviour {

    public static AnimationManager current;
    
    public float titleTime, numberTime, startTime, playerTime;

    public SpriteRenderer title, number, start, p1, p2, p3, p4;

    public Color p1Target, p2Target, p3Target, p4Target;

    public bool introFinished;

    public bool flickerNumber, flickerTitle;

    public float flickerNumberStrength, flickerTitleStrength;
    public float flickerNumberMin, flickerTitleMin;

    void Awake()
    {
        current = this;
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(Intro());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }

        if (introFinished)
        {
            Flicker();
        }
	}

    IEnumerator Intro()
    {
        // Title
        float time = titleTime;
        while(titleTime > 0)
        {
            titleTime -= Time.deltaTime;
            title.color += 1f / time * new Color(1, 1, 1) * Time.deltaTime;
            yield return null;
        }

        // Number
        time = numberTime;
        while (numberTime > 0)
        {
            numberTime -= Time.deltaTime;
            number.color += 1f / time * new Color(1, 1, 1) * Time.deltaTime;
            yield return null;
        }
        // Player
        time = playerTime;
        while (playerTime > 0)
        {
            playerTime-= Time.deltaTime;
            p1.color += 1f / time * p1Target * Time.deltaTime;
            yield return null;
        }
        playerTime = time;
        while (playerTime > 0)
        {
            playerTime -= Time.deltaTime;
            p2.color += 1f / time * p2Target * Time.deltaTime;
            yield return null;
        }
        playerTime = time;
        while (playerTime > 0)
        {
            playerTime -= Time.deltaTime;
            p3.color += 1f / time * p3Target * Time.deltaTime;
            yield return null;
        }
        playerTime = time;
        while (playerTime > 0)
        {
            playerTime -= Time.deltaTime;
            p4.color += 1f / time * p4Target * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // Start
        time = startTime;
        while (startTime > 0)
        {
            startTime -= Time.deltaTime;
            start.color += 1f / time * new Color(1, 1, 1) * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1);

        introFinished = true;
    }

    void Flicker()
    {
        if (flickerNumber)
        {
            number.color += new Color(1, 1, 1) * Random.Range(-flickerNumberStrength, flickerNumberStrength);

            float r = Mathf.Clamp(number.color.r, flickerNumberMin, 1.0f);
            float g = Mathf.Clamp(number.color.g, flickerNumberMin, 1.0f);
            float b = Mathf.Clamp(number.color.b, flickerNumberMin, 1.0f);

            number.color = new Color(r, g, b);
        }
        if (flickerTitle)
        {
            title.color += new Color(1, 1, 1) * Random.Range(-flickerTitleStrength, flickerTitleStrength);

            float r = Mathf.Clamp(title.color.r, flickerTitleMin, 1.0f);
            float g = Mathf.Clamp(title.color.g, flickerTitleMin, 1.0f);
            float b = Mathf.Clamp(title.color.b, flickerTitleMin, 1.0f);

            title.color = new Color(r, g, b);
        }
    }
}
