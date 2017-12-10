using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBot_Behaviour : MonoBehaviour {
    AudioSource music;
	// Use this for initialization
	void Start () {
		
	}

    void Awake()
    {
        music = GetComponent<AudioSource>();
        DontDestroyOnLoad(transform.gameObject);
    }

	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator switchVolumeOn(bool on, float transitionTime)
    {
        float time= transitionTime;
        if (on)
        {
            while (transitionTime > 0)
            {
                music.volume += 1f / time * Time.deltaTime * 1.5f;
            }
        }
        else
        {
            while (transitionTime > 0)
            {
                music.volume -= 1f / time * Time.deltaTime * 1.5f;
            }

        }
        yield return 0;
    }
}
