using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warpAnimator : MonoBehaviour {

    public GameObject[] gameObjects;
    [Range (0f, 2f)]
    public float spawn_delay;
    [Range(0f, 2f)]
    public float spawn_difference_between;

	// Use this for initialization
	void Start () {
        StartCoroutine(warpAnimation());
    }
	
	// Update is called once per frame
	void Update () {
	}

    IEnumerator warpAnimation()
    {
        while (true) {
            yield return new WaitForSeconds(spawn_delay);
            GameObject orange = Instantiate(gameObjects[1]);            
            orange.transform.parent = this.transform;            
            orange.transform.localRotation = gameObjects[1].transform.rotation;
            orange.transform.localPosition = new Vector3(0, 0.001f, 0);

            yield return new WaitForSeconds(spawn_difference_between);
            GameObject blue = Instantiate(gameObjects[0]);
            blue.transform.parent = this.transform;
            blue.transform.localPosition = new Vector3(0, 0.001f, 0);
            blue.transform.localRotation = gameObjects[0].transform.rotation;
        }
    }
}
