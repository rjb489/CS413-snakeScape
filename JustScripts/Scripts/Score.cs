using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static float timer;
    public static bool timeStarted = false;
    public static bool gameIsPaused;
    public int score;
    [Header("Inscribed")]
    public Text scoreCounter;

    void Start()
    {
        // reset timer to 0 when scene is started
        timer = 0;
    }

    void Update()
    {
        if (timeStarted)
        {
            timer += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    void OnGUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        timeStarted = true;
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        // Create a GUIStyle with a larger font size
        GUIStyle timerStyle = new GUIStyle();
        timerStyle.fontSize = 40; // Set the font size here
        timerStyle.normal.textColor = Color.white; // Set the text color

        // Adjust the Rect size to fit the larger font
        GUI.Label(new Rect(10, 10, 200, 50), niceTime, timerStyle);
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            timeStarted = false;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void UpdateScore(int byValue)
    {
        this.score += byValue;
        this.scoreCounter.text = "Score: " + this.score;
    }
}
