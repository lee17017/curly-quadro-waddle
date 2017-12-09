using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    public int playerID;
    
    Rigidbody body;
    public bool keyBoard;
    public float forceMultiplicator = 30;
    public float projectileHitMultiplicator;
    public float rotationSpeed;
    public GameObject projectile;
    public float slowingFactor; //how fast your velocity gets reduced towards 0 
    public float maxVelocity;
    public float stunDuration;
    public Vector3 startPosition;
    enum PlayerState {Normal, Hit};
    PlayerState state;
	// Use this for initialization
	void Start () {
        state = PlayerState.Normal;
        body = this.GetComponent<Rigidbody>();
        startPosition = this.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        checkMaxVelocity();
        slowDown();
        if (keyBoard && Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        else if (!keyBoard && InputManager.current.GetShoot("" + playerID)) {
            Shoot();
        }



            if (keyBoard)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                this.transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
            else if (Input.GetKey(KeyCode.RightArrow))
                this.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
        }
        else
            transform.eulerAngles = new Vector3(0, InputManager.current.GetAngle(""+playerID)-90, 0);
    }

    //Apply force towards Z-direction of Player and spawn Projectile in other direction
    void Shoot()
    {
        if (state == PlayerState.Normal)
        {
            body.AddRelativeForce(Vector3.forward * forceMultiplicator / Time.deltaTime);
            float xOffset = Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * this.transform.localScale.x *2;
            float zOffset = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * this.transform.localScale.z *2;
            Instantiate(projectile, new Vector3(transform.position.x - xOffset, transform.position.y, transform.position.z - zOffset), transform.rotation);
        }
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
        body.velocity -= body.velocity * Time.deltaTime * slowingFactor;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            Vector3 vel = other.GetComponent<Rigidbody>().velocity;
            body.AddForce(vel * projectileHitMultiplicator / Time.deltaTime);
            Destroy(other.gameObject);
            if (state == PlayerState.Normal)
                StartCoroutine(Stun());
        }
        else if (other.tag == "Player") {
            Debug.Log("player hit");
        }
    }

    IEnumerator Stun()
    {
        state = PlayerState.Hit;
        yield return new WaitForSeconds(stunDuration);
        state = PlayerState.Normal;
    }
}
