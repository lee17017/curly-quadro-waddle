using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlayerBehaviour : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        ScreenWrap();
    }

    void ScreenWrap()
    {
        
        Vector3 newPosition = transform.position;
        //x wrap
        if(newPosition.x > 25)
        {
            newPosition.x -= 24;
        }

        if(newPosition.x < 1)
        {
            newPosition.x += 24;
        }

        //z wrap
        if (newPosition.z > 14)
        {
            newPosition.z -= 14;
        }

        if (newPosition.z < 0)
        {
            newPosition.z += 14;
        }

        transform.position = newPosition;
    }


}
