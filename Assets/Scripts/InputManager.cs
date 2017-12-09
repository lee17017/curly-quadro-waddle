using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager current;

    public float angle1, angle2, angle3, angle4;

    private float hor1, vert1, hor2, hor3,hor4,vert2,vert3,vert4;
    private bool rt1, rt2, rt3, rt4;

	// Use this for initialization
	void Awake () {
        current = this;
	}
	
	// Update is called once per frame
	void Update () {
        CheckButtons();
	}

    void CheckButtons()
    {
        // Player 1
        if (Input.GetKeyDown(KeyCode.Joystick1Button7) || (Input.GetAxis("RT1") < -0.1 && !rt1))
        {
            rt1 = true;
            Debug.Log("Shoot 1");
        }
        if(Input.GetAxis("RT1") > -0.1 && Input.GetAxis("RT1") < 0.1) { rt1 = false; }

        hor1 = Input.GetAxis("H1");
        vert1 = Input.GetAxis("V1");
        angle1 = Mathf.Atan2(vert1, hor1) * Mathf.Rad2Deg;

        // Player 2
        if (Input.GetKeyDown(KeyCode.Joystick2Button7) || (Input.GetAxis("RT2") < -0.1 && !rt2))
        {
            rt2 = true;
            Debug.Log("Shoot 2");
        }
        if (Input.GetAxis("RT2") > -0.1 && Input.GetAxis("RT2") < 0.1) { rt2 = false; }
        hor2 = Input.GetAxis("H2");
        vert2 = Input.GetAxis("V2");
        angle2 = Mathf.Atan2(vert2, hor2) * Mathf.Rad2Deg;
        // Player 3
        if (Input.GetKeyDown(KeyCode.Joystick3Button7) || (Input.GetAxis("RT3") < -0.1 && !rt3))
        {
            rt3 = true;
            Debug.Log("Shoot 3");
        }
        if (Input.GetAxis("RT3") > -0.1 && Input.GetAxis("RT3") < 0.1) { rt3 = false; }
        hor3 = Input.GetAxis("H3");
        vert3 = Input.GetAxis("V3");
        angle3 = Mathf.Atan2(vert3, hor3) * Mathf.Rad2Deg;
        // Player 4
        if (Input.GetKeyDown(KeyCode.Joystick4Button7) || (Input.GetAxis("RT4") < -0.1 && !rt4))
        {
            rt4 = true;
            Debug.Log("Shoot 4");
        }
        if (Input.GetAxis("RT4") > -0.1 && Input.GetAxis("RT4") < 0.1) { rt4 = false; }
        hor4 = Input.GetAxis("H4");
        vert4 = Input.GetAxis("V4");
        angle4 = Mathf.Atan2(vert4, hor4) * Mathf.Rad2Deg;
    }
}
