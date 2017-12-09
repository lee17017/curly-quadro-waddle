using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForce : MonoBehaviour {

    // public Rigidbody2D rb;
    // public GameObject circle;
    private float thrust;
    private float radius;
    private Vector2 center;


    void Start()
    {
        center = transform.position;
        radius = GetComponent<SphereCollider>().radius;
        thrust = 0;

        // rb = GetComponent<Rigidbody2D>();
        Debug.Log("started");
    }

    void FixedUpdate()
    {   
        // Test if object inside a circle
        // (x - center.x) ^ 2 + (y - center.y) ^ 2 < radius ^ 2;
        
    }

    // Update is called once per frame
    void Update () {
        // transform.Translate(Vector3.left * Time.deltaTime);
	}


    private void OnTriggerStay(Collider collision)
    {
        GameObject colObj = collision.gameObject;
        Debug.Log("collided");

        Vector2 colPos = colObj.transform.position;
        Vector2 forceVec = new Vector2(center.x - colPos.x, center.y - colPos.y);

        Debug.Log("Force: " + forceVec);
        thrust = forceVec.magnitude / 9;
        colObj.GetComponent<Rigidbody>().AddForce(forceVec.normalized * thrust);
        


        // rb.AddForce(transform.up * thrust);
    }





}
