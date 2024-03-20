using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public Text time;

    public Text gradeText;

    public GameObject gameOver;

    public Button startGameBtn;
    int grade = 0;
    float gametime = 0;
    // Start is called before the first frame update
    void Start()
    {
        gradeText.text = "0";
        gameOver.active = false;
        startGameBtn.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStopwatchDisplay();

        gametime += Time.deltaTime;
    }

    void UpdateStopwatchDisplay()
    {
        int minutes = Mathf.FloorToInt(gametime / 60);
        int secends = Mathf.FloorToInt(gametime % 60);
        time.text = string.Format("{0:00}:{1:00}", minutes, secends);
    }

    public void addGrade(int t)
    {
        if (t == 1)
        {
            grade += 1;

        }
        else if (t == 2)
        {
            grade += 2;
        }
        else if (t == 3)
        {
            grade += 4;
        }
        else if (t == 4)
        {
            grade += 8;
        }
        gradeText.text = grade.ToString();
    }

    public void gameOverr()
    {
        Time.timeScale = 0;
        gameOver.active = true;
        startGameBtn.transform.gameObject.SetActive(true);
    }


    public void startGames()
    {
        Grid.resetGrid();
        Time.timeScale = 1;
        gradeText.text = "0";
        gameOver.active = false;
        gametime = 0;
        startGameBtn.transform.gameObject.SetActive(false);
    }
}
