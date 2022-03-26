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
    public GameObject MessageContainer;
    public TextMeshProUGUI TextConditionGame;
    public TextMeshProUGUI Message;
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
    public void MessageFunction(string message)
    {
    Message.text = message;    
    GameObjectActive(MessageContainer, true);   
    }
    public void GameObjectActive(GameObject g ,bool active)
    {
    g.SetActive(active);    
    }
    public void StartShowMessage(string message, float seconds)
    {
    StartCoroutine(IEShowMessage(message,seconds));    
    }
    public IEnumerator IEShowMessage(string message, float seconds)
    {
    
    MessageFunction(message);
    yield return new WaitForSeconds(seconds);
    if(LevelClass.Balls > 0)
    {
        GameObjectActive(MessageContainer, false);
    }

    }

    
}
   
}

