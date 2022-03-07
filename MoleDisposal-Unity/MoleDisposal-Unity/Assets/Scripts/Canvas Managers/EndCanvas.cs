/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 23, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Mar 6, 2022
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

    [Header("Canvas SETTINGS")]
    public Text endMsgTextbox; //textbox for the title

    /*** METHODS ***/
    private void Start()
    {
        gm = GameManager.GM; //find the game manager

        Debug.Log(gm.endMsg);

        //Set the Canvas text from GM reference
        endMsgTextbox.text = gm.endMsg;

    } //end Start()

    public void GameRestart()
    {
        gm.StartGame(); //refenece the StartGame method on the game manager
    } //end GameRestart()

    public void GameExit()
    {
        gm.ExitGame(); //refenece the ExitGame method on the game manager
    } //end GameExit()
} //end EndCanvas class
