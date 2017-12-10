using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public GameObject[] player = new GameObject[4];
    public GameObject[] warps = new GameObject[2];
    public GameObject crown;
    public GameObject text;
    public float timer;
    private bool once;
	// Use this for initialization
	void Start () {
        once = true;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0 && once)
            StartCoroutine("End");
	}
    IEnumerator End() {
        if (ScoreManager.draw())
        {
            timer += 10;
            yield return new WaitForSeconds(0);
        }
        else
        {
            once = false;
            int winner = ScoreManager.getWinner() - 1;
            for (int i = 0; i < 4; i++)
            {
                player[i].GetComponent<SphereCollider>().isTrigger = true;
                player[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                player[i].GetComponent<PlayerBehavior>().state = PlayerBehavior.PlayerState.End;
            }
            for (int i = 0; i < 2; i++)
            {
                warps[i].GetComponent<GravityForce>().end = true;
            }
            yield return new WaitForSeconds(2);
            player[winner].GetComponent<Rigidbody>().isKinematic = true;
            player[winner].transform.position = new Vector3(13f, 0.8f, 6.8f);
            player[winner].transform.rotation = Quaternion.EulerAngles(0, 170, 0);
            player[winner].gameObject.active = true;

            player[winner].transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            text.GetComponent<SpriteRenderer>().enabled = true;
            crown.GetComponent<SpriteRenderer>().enabled = true;
            for (int i = 0; i < 35; i++) {
                player[winner].transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
                text.transform.localScale += new Vector3(0.029f, 0.029f, 0.029f);
                crown.transform.localScale += new Vector3(0.029f, 0.029f, 0.029f);
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(7f);
            SceneManager.LoadScene(2);
            



            //player[winner].GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
