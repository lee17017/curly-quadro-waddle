using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJoin : MonoBehaviour {

    public int playerID;

    private bool joined;

    public Color joinedColor, notJoinedColor;

    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (AnimationManager.current.introFinished)
        {
            CheckStart();
            SwitchJoined();
            UpdateColor();
        }
    }

    void UpdateColor()
    {
        if (joined)
        {
            spriteRenderer.color = joinedColor;
        }
        else
        {
            spriteRenderer.color = notJoinedColor;
        }
    }

    void SwitchJoined()
    {
        if (InputManager.current.GetShoot("" + playerID))
        {
            joined = !joined;
            switch (playerID)
            {
                case 1:
                    Settings.p1 = joined;
                    break;
                case 2:
                    Settings.p2 = joined;
                    break;
                case 3:
                    Settings.p3 = joined;
                    break;
                case 4:
                    Settings.p4 = joined;
                    break;
            }
        }
    }

    void CheckStart()
    {
        switch (playerID)
        {
            case 1:
                bool startpressed = Input.GetKeyDown(KeyCode.Joystick1Button9);

                if (startpressed)
                {
                    StartGame();
                }
                break;
            case 2:
                startpressed = Input.GetKeyDown(KeyCode.Joystick2Button7);

                if (startpressed)
                {
                    StartGame();
                }
                break;
            case 3:
                startpressed = Input.GetKeyDown(KeyCode.Joystick3Button7);

                if (startpressed)
                {
                    StartGame();
                }
                break;
            case 4:
                startpressed = Input.GetKeyDown(KeyCode.Joystick4Button7);

                if (startpressed)
                {
                    StartGame();
                }
                break;
            default:
                break;
        }
    }

    void StartGame()
    {
        if(Settings.p1 || Settings.p2 || Settings.p3 || Settings.p4)
        {
            SceneManager.LoadScene(1);
        }
    }
}
