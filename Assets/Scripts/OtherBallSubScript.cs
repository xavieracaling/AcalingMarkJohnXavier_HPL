using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
public class OtherBallSubScript : MonoBehaviour
{
    private static Rigidbody rb;
    private Vector3 startPos;

    private void Start() {
            rb = GetComponent<Rigidbody>();
            startPos = transform.position;
    }
    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "MainBall"|| other.gameObject.tag == "Balls")
        {
            AudioManager.AM.AudioSourceList[0].Play();
        }
        if(other.gameObject.tag == "hole")
        {
            AudioManager.AM.AudioSourceList[2].Play();
            LevelClass.LC.BallReduce();
            Destroy(this.gameObject);
            if(rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY;
            }
            
            
        }
        Debug.Log("Ontrigger");
        if(other.gameObject.tag == "exit")
        {
            transform.position = startPos;
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }

    }
    private void OnTriggerEnter(Collider other) {
        
    }
}
