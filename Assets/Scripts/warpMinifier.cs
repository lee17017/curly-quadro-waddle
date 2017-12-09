using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warpMinifier : MonoBehaviour {

    [Range (0f, 1f)]
    public float speed;	
	// Update is called once per frame
	void Update () {
		if(this.transform.localScale.magnitude >= 0.05)
        {
            this.transform.localScale -= new Vector3(speed, speed, speed);
        }
        else
        {
           DestroyImmediate(gameObject);
        }
	}
}
