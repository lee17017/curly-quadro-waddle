using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    Rigidbody body;
    public float forceMultiplicator = 30;
    public float projectileHitMultiplicator;
    public float rotationSpeed;
    public GameObject projectile;
    public float slowingFactor; //how fast your velocity gets reduced towards 0 
    public float maxVelocity; 
	// Use this for initialization
	void Start () {
        body = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        checkMaxVelocity();
        slowDown();
        if (Input.GetButtonDown("Shoot")) {
            Shoot();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
            this.transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
        else if (Input.GetKey(KeyCode.RightArrow))
            this.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));

    }

    //Apply force towards Z-direction of Player and spawn Projectile in other direction
    void Shoot()
    {
        body.AddRelativeForce(Vector3.forward*forceMultiplicator/Time.deltaTime);
        float xOffset = Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * this.transform.localScale.x;
        float zOffset = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * this.transform.localScale.x;
        Instantiate(projectile, new Vector3(transform.position.x-xOffset, transform.position.y, transform.position.z-zOffset), transform.rotation);
    }


    //set Velocity to maxVel value if player is too fast
    void checkMaxVelocity() {
        Vector3 val = new Vector3();
        for (int i = 0; i < 3; i++)
        {
            if (Mathf.Abs(body.velocity[i]) > maxVelocity)
            {
                val[i] = maxVelocity * (body.velocity[i]>0 ? 1:-1);
                Debug.Log("Max Vel" + i);
            }
            else
                val[i] = body.velocity[i];
        }
        body.velocity = val;
    }


    //constant slowDown of Player
    void slowDown()
    {
        body.velocity -= body.velocity.normalized * Time.deltaTime * slowingFactor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            Vector3 vel = other.GetComponent<Rigidbody>().velocity;
            body.AddForce(vel * projectileHitMultiplicator / Time.deltaTime);
            Destroy(other.gameObject);
        }
    }
}
