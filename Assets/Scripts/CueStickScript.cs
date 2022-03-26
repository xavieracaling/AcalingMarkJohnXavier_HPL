using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using System.Threading.Tasks;
namespace Main 
{
    public class CueStickScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static CueStickScript CS;
    public Transform Ball;
    public Transform Point;
    public float WaveLength;
    public float Movement;
    public bool GameIsInProgress;
    public bool tt;
    private bool dragBall;
    void Start()
    {
         CS = this;
         Movement = 3f;
         WaveLength = 2.4f;
         dragBall = true;
         Manager.GameManager.GameIsInProgress = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouseTrigger();    
        if(dragBall)
        {
            DragBall();
            return;
        }
            
        
       
        if(CheckingGameProgress())
            return;

        if(Manager.GameManager.GameIsInProgress)
            return;
        RayCollision(); 
        
        MovementValues();
        StickMovementFunction();

    }
    public void DragBall()
    {
 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Vector3 mouseOnDragPlace =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("dragLayer") ))
        {
            Debug.DrawRay(Camera.main.transform.position,Vector3.forward,Color.red);
            Ball.position = new Vector3(hit.point.x,-4.9232f,hit.point.z);;
            
        }
    }
    private bool CheckingGameProgress()
    {
        if(Manager.GameManager.GameIsInProgress)
        {
            if(Ball.GetComponent<Rigidbody>().velocity.magnitude <= 0.1f)
            {
               Manager.GameManager.GameIsInProgress = false;
               WaveLength = 2.4f;
            }
              return true;
        }
        else 
        {
            return false;
        }
    }    
    public void CheckMouseTrigger()
    {
        if(Input.GetMouseButtonDown(0) && !dragBall)
        {
             Manager.GameManager.ReadyToHit = true;
             
        }
        if (Input.GetMouseButtonUp(0) && !dragBall)
        {
             Manager.GameManager.ReadyToHit = false; 
        }
        if(Input.GetMouseButtonDown(0) && dragBall)
        {
             dragBall = false;
             Manager.GameManager.GameIsInProgress = false;
             
        }
        
    }
    public void MovementValues()
    {
        if(!Manager.GameManager.ReadyToHit)
        {
            Movement -= Input.GetAxis("Mouse X") *8f * Time.deltaTime;    
        }
        else 
        {
            WaveLength -= Input.GetAxis("Mouse Y") *6f * Time.deltaTime;     
            WaveLength = Mathf.Clamp(WaveLength, 2.2f, 2.7f); 
        }
    }
    public void StickMovementFunction()
    {
        
        transform.position = new Vector3(
        Mathf.Sin(Movement) * WaveLength + Ball.transform.position.x,  
        Ball.transform.position.y, 
        Mathf.Cos( Movement) * WaveLength + Ball.transform.position.z);

        Vector3 pos = Ball.position   - transform.position;
        Quaternion rot = Quaternion.LookRotation(pos);
        transform.rotation = rot;
    }
    public void RayCollision()
    {
        RaycastHit hit;
        if(Physics.Raycast(Point.transform.position,Point.forward,out hit, 0.3f) && !Manager.GameManager.GameIsInProgress)
        {
        Debug.DrawRay(Point.position, hit.point, Color.red); 
            if(hit.transform.gameObject.tag == "MainBall" && Manager.GameManager.ReadyToHit)
            {
                Manager.GameManager.GameIsInProgress = true;
                Main.BallScript.BallGotHit(Point); 
                Manager.GameManager.ReadyToHit = false;
                WaveLength = 2.2f;
            }
          
        }
    }
    
}

}
