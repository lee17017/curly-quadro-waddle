using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager current;

    public bool shoot1, shoot2, shoot3, shoot4;
    public bool shootDown1, shootDown2, shootDown3, shootDown4;
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
        shoot1 = false;
        shootDown1 = false;
        // Player 1
        if (Input.GetKeyDown(KeyCode.Joystick1Button7) || (Input.GetAxis("RT1") < -0.1 && !rt1))
        {
            rt1 = true;
            shoot1 = true;
            Debug.Log("Shoot 1");
        }
        if (Input.GetKey(KeyCode.Joystick1Button7) || (Input.GetAxis("RT1") < -0.1))
        {
            shootDown1 = true;
        }
        if(Input.GetAxis("RT1") > -0.1 && Input.GetAxis("RT1") < 0.1) { rt1 = false; }

        hor1 = Input.GetAxis("H1");
        vert1 = Input.GetAxis("V1");
        angle1 = Mathf.Atan2(vert1, hor1) * Mathf.Rad2Deg;

        // Player 2
        shoot2 = false;
        shootDown2 = false;
        if (Input.GetKeyDown(KeyCode.Joystick2Button7) || Input.GetKeyDown(KeyCode.Joystick2Button1)|| (Input.GetAxis("RT2") < -0.1 && !rt2))
        {
            rt2 = true;
            shoot2 = true;
            Debug.Log("Shoot 2");
        }
        if (Input.GetKey(KeyCode.Joystick1Button7) || (Input.GetAxis("RT2") < -0.1))
        {
            shootDown2 = true;
        }
        if (Input.GetAxis("RT2") > -0.1 && Input.GetAxis("RT2") < 0.1) { rt2 = false; }
        hor2 = Input.GetAxis("H2");
        vert2 = Input.GetAxis("V2");
        angle2 = Mathf.Atan2(vert2, hor2) * Mathf.Rad2Deg;
        // Player 3
        shoot3 = false;
        shootDown3 = false;
        if (Input.GetKeyDown(KeyCode.Joystick3Button7) || (Input.GetAxis("RT3") < -0.1 && !rt3))
        {
            shoot3 = true;
            rt3 = true;
            Debug.Log("Shoot 3");
        }
        if (Input.GetKey(KeyCode.Joystick1Button7) || (Input.GetAxis("RT3") < -0.1))
        {
            shootDown3 = true;
        }
        if (Input.GetAxis("RT3") > -0.1 && Input.GetAxis("RT3") < 0.1) { rt3 = false; }
        hor3 = Input.GetAxis("H3");
        vert3 = Input.GetAxis("V3");
        angle3 = Mathf.Atan2(vert3, hor3) * Mathf.Rad2Deg;
        // Player 4
        shoot4 = false;
        shootDown4 = false;
        if (Input.GetKeyDown(KeyCode.Joystick4Button7) || (Input.GetAxis("RT4") < -0.1 && !rt4))
        {
            rt4 = true;
            shoot4 = true;
            Debug.Log("Shoot 4");
        }
        if (Input.GetKey(KeyCode.Joystick1Button7) || (Input.GetAxis("RT4") < -0.1))
        {
            shootDown4 = true;
        }
        if (Input.GetAxis("RT4") > -0.1 && Input.GetAxis("RT4") < 0.1) { rt4 = false; }
        hor4 = Input.GetAxis("H4");
        vert4 = Input.GetAxis("V4");
        angle4 = Mathf.Atan2(vert4, hor4) * Mathf.Rad2Deg;
    }

    public bool GetShootDown(string id)
    {
        switch (id)
        {
            case "1":
                return shootDown1;
            case "2":
                return shootDown2;
            case "3":
                return shootDown3;
            case "4":
                return shootDown4;
            default:
                return false;
        }
    }


    public bool GetShoot(string id)
    {
        switch (id)
        {
            case "1":
                return shoot1;
            case "2":
                return shoot2;
            case "3":
                return shoot3;
            case "4":
                return shoot4;
            default:
                return false;
        }
    }

    public float GetAngle(string id)
    {
        switch (id)
        {
            case "1":
                return angle1;
            case "2":
                return angle2;
            case "3":
                return angle3;
            case "4":
                return angle4;
            default:
                return 0;
        }
    }
}
