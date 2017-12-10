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
    public float maxVelocityZ;
    public float maxVelocityX;
    public float stunDuration;
    public float shootHoldCooldown;
    public float shootDownCooldown;
    public float respawnTime;
    public float lastHitTime;
    public Vector3 startPosition;
    public enum PlayerState {Normal, Respawn, Hit, End};
    private Color playerColor;
    private float shootTimer;
    private int lastHitBy = 0;
    public Color playerColorStun;
    public PlayerState state;
    private ParticleSystem boostParticles;

    void Awake()
    {
        gameObject.SetActive(Settings.IsActive(playerID));
        //boostParticles = gameObject.transform.GetChild(1).GetComponent<ParticleSystem>();
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
        if (MapManager.current.finished && state != PlayerState.End)
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
            //boostParticles.Play();
        }
    }


    //set Velocity to maxVel value if player is too fast
    void checkMaxVelocity() {
        Vector3 val = new Vector3();
        
        if (Mathf.Abs(body.velocity.z) > maxVelocityZ)
            val.z = maxVelocityZ * (body.velocity.z>0 ? 1:-1);
        else
            val.z = body.velocity.z;


        if (Mathf.Abs(body.velocity.x) > maxVelocityX)
            val.x = maxVelocityX * (body.velocity.x > 0 ? 1 : -1);
        else
            val.x = body.velocity.x;

        val.y = 0;
        body.velocity = val;
    }


    //constant slowDown of Player
    void slowDown()
    {
        body.velocity -= body.velocity * Time.deltaTime * slowingFactor;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state != PlayerState.End)
        {
            if (other.tag == "Player")
            {
                //Vector3 velRel = GetComponent<Rigidbody>().velocity-other.GetComponent<Rigidbody>().velocity;
                //GetComponent<Rigidbody>().velocity -= velRel;
                //Debug.Log(velRel);
            }
            else if (other.tag == "Deathzone")
            {
                ScoreManager.addScore(playerID, -100);
                StartCoroutine("Respawn");
            }
        }
        else if (other.tag == "Deathzone") {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Transform other = col.transform;
        if (other.tag == "Projectile" && other.GetComponent<ProjectileBehavior>().playerNmb != playerID)
        {
            Vector3 vel = other.GetComponent<Rigidbody>().velocity;
            body.AddForce(vel * projectileHitMultiplicator / Time.deltaTime);
            lastHitBy = other.GetComponent<ProjectileBehavior>().playerNmb;
            StopCoroutine("resetLastHit");
            StartCoroutine("resetLastHit");
            Destroy(other.gameObject);
            if (state == PlayerState.Normal)
                StartCoroutine("Stun");
        }
    }


    void ScreenWrap()
    {

        Vector3 newPosition = transform.position;
        //x wrap
        if (newPosition.x > 24.5f)
        {
            newPosition.x -= 23;
        }

        if (newPosition.x < 1.5f)
        {
            newPosition.x += 23;
        }

        //z wrap
        if (newPosition.z > 13.5f)
        {
            newPosition.z -= 13.5f;
        }

        if (newPosition.z < 0)
        {
            newPosition.z += 13.5f;
        }

        transform.position = newPosition;
    }

    IEnumerator Respawn() {
        state = PlayerState.Respawn;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        transform.position = startPosition + new Vector3(0, 5+playerID*2, 1000);
        StopCoroutine("Stun");
        GetComponentInChildren<SpriteRenderer>().color = playerColor;
        yield return new WaitForSeconds(respawnTime / 2);
        if (state == PlayerState.End)
            gameObject.SetActive(false);
        transform.position = startPosition;
        yield return new WaitForSeconds(respawnTime / 2);
        if (state == PlayerState.End)
            gameObject.SetActive(false);
        state = PlayerState.Normal;
    }

    IEnumerator resetLastHit()
    {
        yield return new WaitForSeconds(lastHitTime);
        lastHitBy = 0;
        Debug.Log("reset");
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
