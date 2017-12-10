using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForce : MonoBehaviour {

    // public Rigidbody2D rb;
    // public GameObject circle;
    public float thrust;
    private float radius;
    private Vector3 center;


    void Start()
    {
        center = transform.position;
        radius = GetComponent<SphereCollider>().radius * transform.localScale.z;
        //Debug.Log(radius);
        // rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Test if object inside a circle
        // (x - center.x) ^ 2 + (y - center.y) ^ 2 < radius ^ 2;

    }

    // Update is called once per frame
    void Update() {
        // transform.Translate(Vector3.left * Time.deltaTime);
        GetComponentInChildren<SpriteRenderer>().color = MapManager.current.warpColor;
    }


    private void OnTriggerStay(Collider other)
    {
        if (MapManager.current.finished) {
            GameObject colObj = other.gameObject;
            float massOther = 2f;
            float massHole = thrust;

            if (other.tag == "Player")
            {
                colObj = other.gameObject;
                massOther = 1f;
                massHole = thrust;

                Vector3 colPos = colObj.transform.position;
                Vector3 center = transform.position; // delete later
                Vector3 forceVec = new Vector3(center.x - colPos.x, 0, center.z - colPos.z);
                float distance = forceVec.magnitude;
                if (distance < 0.5f)
                    distance = 0.5f;
                //    Debug.Log("Forcevector: " + forceVec);

                // Richtungsvektor * Kraft * Prozentuale Nähe zum center
                // colObj.GetComponent<Rigidbody>().AddForce(forceVec.normalized * thrust * (1 - (forceVec.magnitude / radius)));

                colObj.GetComponent<Rigidbody>().AddForce(massOther * massHole / (distance * distance) * forceVec.normalized);

                //  Debug.Log("calculated Force: " + (1 - (distance / radius)));


                // rb.AddForce(transform.up * thrust);
            }

        }
    }
}
