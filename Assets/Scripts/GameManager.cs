using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button startButton;
    public GameState gameState;
    public static GameManager instance;


    public enum GameState
    {
        Playing,
        CreatGrid,
        PlayAnimation,

        RemoveGrid,
        

        Paused,
        GameOver
    }

    public enum GameDir
    {
        UP,DOWN,LEFT,RIGHT,NELL
    }

    public GameDir gameDir = GameDir.NELL;

    private GameManager()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA" + this + "DELETED");
            Destroy(gameObject);
        }

    }
    void Start()
    {
        if (startButton!= null)
        {
             startButton.onClick.AddListener(StartScene);
        }
       
    }
    public void StartScene(){
        SceneManager.LoadScene(1);
    }

    public void GoAppScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitScene()
    {
       UnityEngine.Application.Quit();
    }

    public void GoScene(int sceneIndex)
    {
         SceneManager.LoadScene(sceneIndex);
    }
}
