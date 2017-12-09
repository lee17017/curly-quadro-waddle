using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public float angle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckButtons();
	}

    void CheckButtons()
    {
        // Player 1
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Debug.Log("Shoot 1");
        }

        angle = Input.GetAxis("Horizontal");

        // Player 2
        if (Input.GetKeyDown(KeyCode.Joystick2Button7))
        {
            Debug.Log("Shoot 2");
        }
        // Player 3
        if (Input.GetKeyDown(KeyCode.Joystick3Button7))
        {
            Debug.Log("Shoot 3");
        }
        // Player 4
        if (Input.GetKeyDown(KeyCode.Joystick4Button7))
        {
            Debug.Log("Shoot 4");
        }
    }
}
