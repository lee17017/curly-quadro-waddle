using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini2PlayerBehaviourDuplicate : MonoBehaviour {

    Renderer[] renderers;
    private bool isWrappingX = false;
    private bool isWrappingZ = false;
    

	// Use this for initialization
	void Start () {
        renderers = GetComponentsInChildren<Renderer>();
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
        bool isVisible = CheckRenderers();
        if (isVisible)
        {
            isWrappingX = false;
            isWrappingZ = false;

            return;
        }

        if (isWrappingX && isWrappingZ)
        {
            return;
        }

        Vector3 newPosition = transform.position;

        if(newPosition.x > 15 || newPosition.x < -15)
        {
            if(newPosition.x > 15)
            {
                newPosition.x = -newPosition.x + 1;
            }
            else
            {

                newPosition.x = -newPosition.x - 1;
            }
            
            isWrappingX = true;
        }

        if (newPosition.z > 7 || newPosition.z < -12)
        {
            newPosition.z = -newPosition.z;
            isWrappingZ = true;
        }

        transform.position = newPosition;
    }

    bool CheckRenderers()
    {
        foreach(var renderer in renderers)
        {
            if (renderer.isVisible)
            {
                return true;
            }
        }
        return false;
    }
}
