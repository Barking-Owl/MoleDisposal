/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 23, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Mar 7, 2022
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

        SetHUD();
    } //end Start()

    void SetHUD()
    {
        //if textbox exists update value
        if (scoreTextbox) { scoreTextbox.text = "Your Score: " + score; }
        if (highScoreTextbox) { highScoreTextbox.text = "Your High Score: " + highscore; }

    }//end SetHUD()
    public void GameRestart()
    {
        gm.StartGame(); //refenece the StartGame method on the game manager
    } //end GameRestart()

    public void GameExit()
    {
        gm.ExitGame(); //refenece the ExitGame method on the game manager
    } //end GameExit()
} //end EndCanvas class
