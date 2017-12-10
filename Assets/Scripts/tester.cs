using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ScoreManager.reset();
        ScoreManager.addScore(1, 6);
        ScoreManager.addScore(2, 5);
        ScoreManager.addScore(3, 7);
        ScoreManager.addScore(4, 4);

        Debug.Log(ScoreManager.draw());
        Debug.Log(ScoreManager.getHighestScore());
        Debug.Log(ScoreManager.getWinner());
        ScoreManager.addScore(2,7);
        ScoreManager.addScore(3,5);
        Debug.Log(ScoreManager.draw());
        Debug.Log(ScoreManager.getHighestScore());
        Debug.Log(ScoreManager.getWinner());

    }

    // Update is called once per frame
    void Update () {
		
	}
}
