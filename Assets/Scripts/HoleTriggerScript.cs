using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Balls" || other.gameObject.tag == "MainBall" )
        {
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
