using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Main
{
   public class BallScript : MonoBehaviour
{
   
   public static Rigidbody RB;
   private Vector3 startPos;
   public Transform Point;
   //public static  
   private void Start() {
        startPos = this.transform.position;
        RB = GetComponent<Rigidbody>();
   }
   public static void BallGotHit(Transform dir)
   {
        AudioManager.AM.AudioSourceList[0].Play();
        RB.AddForce(dir.forward * Random.Range(5f - (5f * LevelClass.ReductionTime),5f - (5f * LevelClass.ReductionTime)), ForceMode.Impulse);
   }
   void OnCollisionEnter(Collision other)
   {    
      if(other.gameObject.tag == "hole")
      {
         Debug.Log("Cue ball has been pocketed!");
         transform.position = new Vector3(1.7f, -4.9f, -3.9f);
         RB.velocity = Vector3.zero;
      }
      if(other.gameObject.tag == "exit")
      {
         transform.position = startPos;
         RB.velocity = Vector3.zero;
         RB.constraints = RigidbodyConstraints.FreezePositionY;
      }
   }
}

}

