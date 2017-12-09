using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour {

    Rigidbody body;
    public int playerNmb;
    public float projectileSpeed;
    public float lifeTime;
	// Use this for initialization
	void Start () {
        body = this.GetComponent<Rigidbody>();
        body.velocity = new Vector3(Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * this.transform.localScale.x,
                                0,
                                Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * this.transform.localScale.x
                                )* -projectileSpeed;
        StartCoroutine(Death());
	}
	
	// Update is called once per frame
	void Update () {
	}

    IEnumerator Death()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }
    
}
