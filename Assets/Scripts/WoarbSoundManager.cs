using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoarbSoundManager : MonoBehaviour {

    private AudioSource woarb;
    private int length;

	// Use this for initialization
	void OnAwake () {
        woarb = GetComponent<AudioSource>();
        length = (int)woarb.clip.length;
        woarb.timeSamples = woarb.clip.samples - 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
