using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillManager : MonoBehaviour {
    
    public PlayerBehavior king;

    public GameObject p1, p2, p3, p4;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        CheckHill();
	}

    void CheckHill()
    {
        int count = 0;
        king = null;

        if (p1.transform.position.x <= 17.5f && p1.transform.position.x >= 8.5f && p1.transform.position.z <=8.5 && p1.transform.position.z >= 5.5){
            count++;
            king = p1.GetComponent<PlayerBehavior>();
        }
        if (p2.transform.position.x <= 17.5f && p2.transform.position.x >= 8.5f && p2.transform.position.z <= 8.5 && p2.transform.position.z >= 5.5)
        {
            count++;
            king = p2.GetComponent<PlayerBehavior>();
        }
        if (p3.transform.position.x <= 17.5f && p3.transform.position.x >= 8.5f && p3.transform.position.z <= 8.5 && p3.transform.position.z >= 5.5)
        {
            count++;
            king = p3.GetComponent<PlayerBehavior>();
        }
        if (p4.transform.position.x <= 17.5f && p4.transform.position.x >= 8.5f && p4.transform.position.z <= 8.5 && p4.transform.position.z >= 5.5)
        {
            count++;
            king = p4.GetComponent<PlayerBehavior>();
        }

        if(count > 1) { king = null; }
    }
}
