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
    public float shootHoldCooldown;
    public float shootDownCooldown;
    public float respawTime;
    public Vector3 startPosition;
    enum PlayerState {Normal, Respawn, Hit};
    private Color playerColor;
    private float shootTimer;
    public Color playerColorStun;
    PlayerState state;

    void Awake()
    {
        gameObject.SetActive(Settings.IsActive(playerID));
    }

	// Use this for initialization
	void Start () {
        state = PlayerState.Normal;
        body = this.GetComponent<Rigidbody>();
        startPosition = this.transform.position;
        playerColor = GetComponentInChildren<SpriteRenderer>().color;
    }
	
	// Update is called once per frame
	void Update () {
        if (MapManager.current.finished)
        {
            checkMaxVelocity();
            slowDown();
            shootTimer += Time.deltaTime;
            if (keyBoard && Input.GetKeyDown(KeyCode.Space))
            {
                Shoot(shootDownCooldown);
            }
            else if (!keyBoard && InputManager.current.GetShoot("" + playerID))
            {
                Shoot(shootDownCooldown);
            }
            else if (InputManager.current.GetShootDown("" + playerID))
            {
                Shoot(shootHoldCooldown);
            }
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
    void FixedUpdate()
    {
        ScreenWrap();
    }
    //Apply force towards Z-direction of Player and spawn Projectile in other direction
    void Shoot(float timeLimit)
    {
        if (state != PlayerState.Hit && shootTimer>timeLimit)
        {
            shootTimer = 0;
            body.AddRelativeForce(Vector3.forward * forceMultiplicator / Time.deltaTime);
            float xOffset = Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * this.transform.localScale.x;
            float zOffset = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * this.transform.localScale.z;
            GameObject temp = (GameObject)Instantiate(projectile, new Vector3(transform.position.x - xOffset, transform.position.y, transform.position.z - zOffset), transform.rotation);
            temp.GetComponentInChildren<SpriteRenderer>().color = playerColor;
            temp.GetComponent<ProjectileBehavior>().playerNmb = playerID;
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
        if (other.tag == "Projectile" && other.GetComponent<ProjectileBehavior>().playerNmb != playerID)
        {
            Vector3 vel = other.GetComponent<Rigidbody>().velocity;
            body.AddForce(vel * projectileHitMultiplicator / Time.deltaTime);
            Destroy(other.gameObject);
            if (state == PlayerState.Normal)
                StartCoroutine(Stun());
        }
        else if (other.tag == "Player") {
            Vector3 velRel = GetComponent<Rigidbody>().velocity-other.GetComponent<Rigidbody>().velocity;
            GetComponent<Rigidbody>().velocity -= velRel;
            Debug.Log(velRel);
        }
        else if(other.tag == "Deathzone")
        { 
            StartCoroutine(Respawn());
        }
    }


    void ScreenWrap()
    {

        Vector3 newPosition = transform.position;
        //x wrap
        if (newPosition.x > 25)
        {
            newPosition.x -= 24;
        }

        if (newPosition.x < 1)
        {
            newPosition.x += 24;
        }

        //z wrap
        if (newPosition.z > 14)
        {
            newPosition.z -= 14;
        }

        if (newPosition.z < 0)
        {
            newPosition.z += 14;
        }

        transform.position = newPosition;
    }

    IEnumerator Respawn() {
        state = PlayerState.Respawn;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        transform.position = startPosition + new Vector3(0, 0, 1000);
        yield return new WaitForSeconds(respawTime / 2);
        StopCoroutine(Stun());
        GetComponentInChildren<SpriteRenderer>().color = playerColor;
        transform.position = startPosition;
        yield return new WaitForSeconds(respawTime / 2);
        state = PlayerState.Normal;
    }

    IEnumerator Stun()
    {
        state = PlayerState.Hit;
        for (int i = 0; i < 5; i++)
        {
            GetComponentInChildren<SpriteRenderer>().color = playerColorStun;
            yield return new WaitForSeconds(stunDuration / 5);
            GetComponentInChildren<SpriteRenderer>().color = playerColor;
            yield return new WaitForSeconds(stunDuration / 5);

        }
        GetComponentInChildren<SpriteRenderer>().color = playerColor;
        if(state == PlayerState.Hit)
            state = PlayerState.Normal;
    }


}
