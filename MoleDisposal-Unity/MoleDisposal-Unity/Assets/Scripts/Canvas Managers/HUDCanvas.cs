/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 23, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Mar 7, 2022
 * 
 * Description: Updates HUD canvas referecing game manager
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCanvas : MonoBehaviour
{
    /*** VARIABLES ***/

    GameManager gm; //reference to game manager

    [Header("Canvas SETTINGS")]
    public Text levelTextbox; //textbox for level count
    public Text livesTextbox; //textbox for the lives
    public Text timerTextbox; //textbox for time
    public Text scoreTextbox; //textbox for score
    public Text highScoreTextbox; //textbox for best score
    
    //GM Data
    private int level;
    private int totalLevels;
    private int lives;
    private float timelimit;
    private int score;
    private int highscore;

    /*** METHODS ***/

    private void Start()
    {
        gm = GameManager.GM; //find the game manager

        //reference to level info
        level = gm.gameLevelsCount;
        totalLevels = gm.gameLevels.Length;
        timelimit = gm.time;
        DisplayTime();



        SetHUD();
    }//end Start

    // Update is called once per frame
    void Update()
    {
        GetGameStats();
        DisplayTime();
        SetHUD();

    }//end Update()

    void GetGameStats()
    {
        lives = gm.Lives;
        timelimit = gm.time;
        score = gm.Score;
        highscore = gm.HighScore;
    }

    void SetHUD()
    {
        //if textbox exists update value
        if (levelTextbox) { levelTextbox.text = "Level: " + level + "/" + totalLevels; }
        if (livesTextbox) { livesTextbox.text = "Attempts: " + lives; }
        if (timerTextbox) { timerTextbox.text = "Time: " + timelimit; }
        if (scoreTextbox) { scoreTextbox.text = "Score: " + score; }
        if (highScoreTextbox) { highScoreTextbox.text = "Best: " + highscore; }

    }//end SetHUD()

    void DisplayTime()
    {
        timelimit = Mathf.FloorToInt(timelimit % 60);
    }//end DisplayTime()
}
