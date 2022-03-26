using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
namespace Manager
{
    public class GameManager : MonoBehaviour
{
    
    public static bool GameIsInProgress;
    public static bool ReadyToHit;
    public GameObject GameOverContainer;
    public TextMeshProUGUI TextConditionGame;
    public static GameManager GM;
    void Start()
    {
        GM = this;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void ShowGameOverContainer(string message, Color c)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameOverContainer.SetActive(true);
        TextConditionGame.text = message;
        TextConditionGame.color = c;
    }

   
}
   
}

