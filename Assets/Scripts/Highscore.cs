using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

    public Text[] lists;
    public InputField userInput;
    public GameObject input;
    public Text placeHolder;

    private int playerStat;
    private string playerName;

    private bool enterName;
    private bool nameInFile = false;

    private List<HighscoreElement> scoresList = new List<HighscoreElement>();

    
    private bool nameEntered;

    private void Awake()
    {
        foreach(Text list in lists){
            list.gameObject.SetActive(false);
        }
        input.SetActive(false);
        
        playerStat = (int) ScoreManager.getHighestScore();
    }

    private void Start()
    {
        loadList();

        if (checkIn(playerStat))
        {
            requestName();
        }
        else
        {
            printList();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !nameInFile)
        {
            
            writeToFile(playerName, playerStat);

            enterName = false;
            nameEntered = true;
            nameInFile = true;
        }

        if (nameEntered)
        {
            nameEntered = false;
            input.SetActive(false);
            loadList();
            printList();
        }

        if(Input.GetKeyDown("joystick 1 button 0") || Input.GetKeyDown("joystick 2 button 0"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void requestName()
    {        
        input.SetActive(true);
        enterName = true;
        //StartCoroutine(limitTime());
        StartCoroutine(getName());
    }


    IEnumerator getName()
    {
        while (enterName)
        {
            playerName = userInput.text.Replace(' ', '_');
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator limitTime()
    {
        yield return new WaitForSeconds(30);
        SceneManager.LoadScene(0);
    }

    private bool checkIn(int score)
    {
        foreach (HighscoreElement element in scoresList)
        {
            if(element.score < score)
            {
                return true;
            }
        }
        return false;
    }

    private void loadList()
    {
        string[] scores = null; //{ "", "" };
        string scorelist = "";
        if (System.IO.File.Exists(Application.dataPath + "//scores.txt"))
        {
            scorelist = System.IO.File.ReadAllText(Application.dataPath + "//scores.txt");//Lese von txt
        }
        scores = scorelist.Split();

        for (int i = 0; i < scores.Length - 1; i += 2)
        {          
            scoresList.Add(new HighscoreElement(scores[i+1], Convert.ToInt32(scores[i])));
        }
    }

    private void printList()
    {
        if(scoresList.Count == 0)
        {
            return;
        }
        scoresList = new List<HighscoreElement>();
        loadList();

        String[] outputs = new String[lists.Length];
        int numberOfElements = 10 / lists.Length;
        int j = 0;
        int i = 0;
        foreach (HighscoreElement element in scoresList)
        {
            if (j > 10 || i >= lists.Length)
            {
                break;
            }

            if (j >= numberOfElements)
            {
                i++;
                j = 0;
            }

            outputs[i] += element.toString();
            j++;
        }


        i = 0;
        foreach(Text list in lists)
        {
            list.text = outputs[i];
            list.gameObject.SetActive(true);
            i++;
        }

    }

    public static void writeToFile(string name, int score)
    {
        string scorelist = "";
        if (System.IO.File.Exists(Application.dataPath + "//scores.txt"))
        {
            scorelist = System.IO.File.ReadAllText(Application.dataPath + "//scores.txt");//Lese von txt
        }
        //Füge neues Score ein
        string[] wordlist = scorelist.Split();
        int length = wordlist.Length;
        Debug.Log(length);
        if (length % 2 != 0 || length < 2)
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
        System.IO.File.WriteAllText(Application.dataPath + "//scores.txt", scorelist);//Schreibe in txt
    }
}

public class HighscoreElement
{
    public String name { get; set; }
    public int score { get; set; }

    public HighscoreElement(String name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public String toString()
    {
        return this.name + "\t\t" + this.score + "\n";
    }
}
