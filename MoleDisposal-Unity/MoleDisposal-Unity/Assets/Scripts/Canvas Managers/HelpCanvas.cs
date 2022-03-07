/**** 
 * Created by: Andrew Nguyen
 * Date Created: Mar 7, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Mar 7, 2022
 * 
 * Description: Updates the canvas for the help screen
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //libraries for UI components

public class HelpCanvas : MonoBehaviour
{
    /*** VARIABLES ***/
   
    GameManager gm; //reference to game manager

    [Header("Canvas SETTINGS")]
    public Text titleTextbox; //textbox for the title
    public Text helpTextbox; //textbox for help

    /*** METHODS ***/
    private void Start()
    {
         gm = GameManager.GM; //find the game manager
         
         //Set the Canvas text from GM reference
         titleTextbox.text = gm.helpTitle; 
         helpTextbox.text = gm.gameHelpText;
    } //end Start()

   public void GoBack()
    {
        gm.GoBack(); //reference the StartGame method on the game manager
    } //end GoBack()

}
