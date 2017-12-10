using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerScore : MonoBehaviour {
    public GameObject p1Drill, p2Drill;

    private int p1score = 0;        //Extrascore des Spielers
    private int p2score = 0;
    private int p1depth = 0;        //Depthscore des Spielers
    private int p2depth = 0; 
    private int p1multiplyer = 1;   //Material-Multiplyer
    private int p2multiplyer = 1;
    public float p1speed = 0;       //relative Geschwindigkeit des Spielers
    public float p2speed = 0;
    private bool p1value = false;   //Befindet sich der Spieler in wertvollem Material?
    private bool p2value = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        p1depth += (int)((p1speed) * p1multiplyer);
        p2depth += (int)((p2speed) * p2multiplyer);
        if (p1value){p1score += 10/p1multiplyer;}
        if (p2value){p2score += 10 / p2multiplyer;}

        p1score =(int) -(p1Drill.transform.position.y - 27);
        p2score =(int) -(p2Drill.transform.position.y - 27);

        GameManager.current.playerScore1 = (int)p1score;
        GameManager.current.playerScore2 = (int)p2score;

        if (!GameManager.current.run)
        {
            int[] scores = { (int)p1score, (int)p2score };
        
            GameManager.current.winnerScore = Mathf.Max(scores);
        }
	}

    public static void writeToBoard(string name, int score)
    {
        string scorelist="";
        if (System.IO.File.Exists(Application.dataPath + "//resources//scores.txt"))
        {
            scorelist= System.IO.File.ReadAllText(Application.dataPath + "//resources//scores.txt");//Lese von txt
        }
        //Füge neues Score ein
        string[] wordlist = scorelist.Split();
        int length=wordlist.Length;
        Debug.Log(length);
        if (length % 2 != 0||length<2)
        {
            scorelist = score + " " + name;
        }
        else
        {
            bool changed = false;
            for (int i = 0; i < length; i += 2)
            {
                int compare;
                Int32.TryParse(wordlist[i], out compare);
                Debug.Log("Vergleich: " + score + " " + compare);
                if (score > compare)
                {
                    if (i > 18)
                    {
                        wordlist[i - 1] += " " + score + " " + name;
                        changed = true;
                        break;
                    }
                    wordlist[i] = score + " " + name + " " + wordlist[i];
                    changed = true;
                    Debug.Log(wordlist[i]);
                    break;
                }
            }
            if (!changed)
            {
                wordlist[length - 1] += " " + score + " " + name;
            }

            scorelist = "";
            for (int i = 0; i < length; i++)
            {
                if (i > 18)
                {
                    break;
                }
                scorelist += wordlist[i];
                if (i < length - 1)
                {
                    scorelist += " ";
                }
            }
            Debug.Log("Liste: " + scorelist);
        }
        System.IO.File.WriteAllText(Application.dataPath + "//resources//scores.txt", scorelist);//Schreibe in txt
    }
}
