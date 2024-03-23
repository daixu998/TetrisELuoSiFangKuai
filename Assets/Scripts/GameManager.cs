using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

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

    private GameManager()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }
        else
        {
            Debug.LogWarning("EXTRA" + this + "DELETED");
            Destroy(gameObject);
        }

    }
}
