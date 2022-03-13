/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 23, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Mar 11, 2022
 * 
 * Description: Updates end canvas refencing game manger
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //libraries for UI components

public class EndCanvas : MonoBehaviour
{
    /*** VARIABLES ***/

    GameManager gm; //reference to game manager

    public Text scoreTextbox; //textbox for score
    public Text highScoreTextbox; //textbox for best score
    public Text creditsTextbox; //textbox for the credits
    public Text copyrightTextbox; //textbox for the copyright

    //GM data
    private int score;
    private int highscore;

    [Header("Canvas SETTINGS")]
    public Text endMsgTextbox; //textbox for the title

    /*** METHODS ***/
    private void Start()
    {
        gm = GameManager.GM; //find the game manager

        Debug.Log(gm.endMsg);

        score = gm.Score;
        highscore = gm.HighScore;

        //Set the Canvas text from GM reference
        endMsgTextbox.text = gm.endMsg;
        creditsTextbox.text = gm.gameCredit;
        copyrightTextbox.text = gm.copyrightYear;
        SetHUD();
    } //end Start()

    // Update is called once per frame
    void Update()
    {
        GetGameStats();
        SetHUD();
    }//end Update()

    void SetHUD()
    {
        //if textbox exists update value
        if (scoreTextbox) { scoreTextbox.text = "Your Score: " + score; }
        if (highScoreTextbox) { highScoreTextbox.text = "Your High Score: " + highscore; }

    } //end SetHUD()

    void GetGameStats()
    {
        score = gm.Score;
        highscore = gm.HighScore;
    } //end GetGameStats()

    public void GameRestart()
    {
        gm.StartGame(); //refenece the StartGame method on the game manager
    } //end GameRestart()

    /*
    public void GameExit()
    {
        gm.ExitGame(); //refenece the ExitGame method on the game manager
    } //end GameExit() 
    */
} //end EndCanvas class
