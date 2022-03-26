using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelClass : MonoBehaviour
{
    public static bool dragBall;
    private static int currentTime;
    private static int timeReduce;
    
    private bool stopTime;
    private static int balls;
    public static float ReductionTime;
    public static LevelClass LC;
    public static int Level;
    public readonly int TimeStart = 300;
    public TextMeshProUGUI BallsLabel;
    public TextMeshProUGUI TimeLabel;
    public TextMeshProUGUI LevelLabel;
    public TextMeshProUGUI TimeReductionLabel;
    public TextMeshProUGUI SlowIncreaseLabel;

    public GameObject GroupOfBallsPrefab;
    public GameObject Ball;
    public Vector3 OffsetBallCont;
    
    

    public static int Balls 
    {
        get => balls;
        set => balls = value;
    }
    public static int CurrentTime
    {
        get => currentTime;
        set => currentTime = value;
    }
    public static int TimeReduce
    {
        get => timeReduce;
        set => timeReduce = value;
    }
    
    private void Start() {
        Initialize();
        
    }
    private void Initialize() 
    {   
        Manager.GameManager.GM.MessageFunction("DRAG THE BALL");
        Manager.GameManager.GameIsInProgress = true;
        dragBall = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        OffsetBallCont = new Vector3(2.238f,-5.16f,-2.996f);
        Level = 1;
        ReductionTime = 0;
        CurrentTime = TimeStart;
        Balls = 6;
        LC = this;
        StartCountdown();
    }

   
    [ContextMenu("LoadLevel")]
    public  void LoadLevel ()
    {       
            StopAllCoroutines();
            Manager.GameManager.GM.MessageFunction("DRAG THE BALL");
            Manager.GameManager.GameIsInProgress = true;
            Main.CueStickScript.CS.ResetPosRot();
            dragBall = true;
            Balls = 6;
            stopTime = true;
            Debug.Log("Good job you have proceeded to the next level!");
            Level +=1;
            TimeReduce += 20;
            CurrentTime = (TimeStart - TimeReduce);
            ReductionTime += 0.3f;
            TimeLabel.text = $"{CurrentTime} SECONDS LEFT";
            LevelLabel.text = $"Level {Level}";
            SlowIncreaseLabel.text = $"SLOW:  {ReductionTime * 100}%";
            StartCountdown();
            Reset();

        
        
    }

    
    [ContextMenu("TestReset")]
    public void Reset()
    {
        BallsLabel.text = "6 BALLS LEFT";
        ResetPosition(Instantiate(GroupOfBallsPrefab),OffsetBallCont, default); //GroupOfBalls
        ResetPosition(Ball,new Vector3(1.7f,-4.9f, -3.9f),default);
    }
    private void ResetPosition(GameObject t, Vector3 v, Quaternion r)
    {
        t.transform.position = v;
        //t.transform.rotation = r;
    }
    public void BallReduce()
    {
        Balls -= 1;
        BallsLabel.text = $"{Balls} BALLS LEFT";

        CheckIfGameWins();
    }
    private bool CheckIfGameWins()
    {
        if(Balls < 1 && Level < 5)
        {
            
            LoadLevel();
            return false;
        }
        if(Level == 5 && Balls < 1)
        {
            GameOver();
            Manager.GameManager.GM.ShowGameOverContainer("YOU WIN!", Color.green);
            
            return true;
        }
        else 
        {
            return false;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over! Thank you for playing.");
    }
    

    public void StartCountdown()
    {
        stopTime = false;
        StartCoroutine(CountDown());
    }
    public IEnumerator CountDown()
    {
        while (CurrentTime > 0 && !stopTime)
        {
            CurrentTime--;
            TimeLabel.text = $"{CurrentTime} SECONDS LEFT";
            Manager.AudioManager.AM.AudioSourceList[1].Play();
            yield return new WaitForSeconds(1f);
        }
        Manager.GameManager.GM.ShowGameOverContainer("YOU LOST!", Color.red);
        Debug.Log("Game Over");
    }

    
}
