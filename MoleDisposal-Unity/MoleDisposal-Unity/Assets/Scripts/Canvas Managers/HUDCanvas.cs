/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 23, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Feb 28, 2022
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
    
    //GM Data
    private int level;
    private int totalLevels;
    private int lives;
    private int timelimit;

    private void Start()
    {
        gm = GameManager.GM; //find the game manager

        //reference to levle info
        level = gm.gameLevelsCount;
        totalLevels = gm.gameLevels.Length;



        SetHUD();
    }//end Start

    // Update is called once per frame
    void Update()
    {
        GetGameStats();
        SetHUD();
    }//end Update()

    void GetGameStats()
    {
        lives = gm.Lives;
        timelimit = gm.time;
    }

    void SetHUD()
    {
        //if texbox exsists update value
        if (levelTextbox) { levelTextbox.text = "Level: " + level + "/" + totalLevels; }
        if (livesTextbox) { livesTextbox.text = "Attempts: " + lives; }
        if (timerTextbox) { timerTextbox.text = "Time :" + timelimit; }

    }//end SetHUD()

}
