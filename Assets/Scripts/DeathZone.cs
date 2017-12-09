using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            other.gameObject.transform.position = other.gameObject.GetComponent<PlayerBehavior>().startPosition;
        }
    }
}
