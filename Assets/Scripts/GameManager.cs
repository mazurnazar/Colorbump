using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Slider slider;
    public GameObject winPanel;
    public GameObject lossPanel;
    public GameObject startPanel;
    public GameObject pressFinger;
    public GameObject startLine, finishLine, ball;

    public static GameManager gameManager;
    public bool GameStarted;
    
    void Start()
    {
        GameManager.gameManager = this;
    }
   
    public void StartGame()
    {
        GameStarted = true;
        Debug.Log("GameStarted");
        startPanel.SetActive(false);
        pressFinger.SetActive(false);
        
        
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(0);
    }
    public void WinGame()
    {
        SceneManager.LoadScene(0);
    }

    public void LevelPassState()
    {
        float distance;
        float levelDistance = finishLine.transform.position.z - startLine.transform.position.z;
        if (ball.transform.position.z > startLine.transform.position.z)
        {
            distance = (ball.transform.position.z - startLine.transform.position.z) / levelDistance*100;
            slider.value = distance;
        }
        else slider.value = 0;
         
    }

}
