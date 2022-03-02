/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 23, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Mar 1, 2022
 * 
 * Description: Basic GameManager Template
****/

/** Import Libraries **/
using System; //C# library for system properties
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //libraries for accessing scenes


public class GameManager : MonoBehaviour
{
    /*** VARIABLES ***/

    #region GameManager Singleton
    static private GameManager gm; //refence GameManager
    static public GameManager GM { get { return gm; } } //public access to read only gm 

    //Check to make sure only one gm of the GameManager is in the scene
    void CheckGameManagerIsInScene()
    {

        //Check if instnace is null
        if (gm == null)
        {
            gm = this; //set gm to this gm of the game object
            Debug.Log(gm);
        }
        else //else if gm is not null a Game Manager must already exsist
        {
            Destroy(this.gameObject); //In this case you need to delete this gm
        }
        DontDestroyOnLoad(this); //Do not delete the GameManager when scenes load
        Debug.Log(gm);
    }//end CheckGameManagerIsInScene()
    #endregion

    [Header("GENERAL SETTINGS")]
    public string gameTitle = "Mole Disposal";  //name of the game
    public string gameCredits = "Made by Andrew Nguyen."; //game creator(s)
    public string copyrightDate = "Copyright " + thisDay; //date cretaed
    public float time = 30.0f; //Time. This is set dynamically, but that feature may be cut. By default it's 30
    public bool sequencing = false; //This is so the player can't move when the moles are beeping the sequence. By default should be aflse
    public GameObject[] moleArray; //Each of the eight moles will be loaded into this array
    public GameObject[] playerHits; //The player's hits

    [Header("GAME SETTINGS")]

    //static vairables can not be updated in the inspector, however private serialized fileds can be
    [SerializeField] //Access to private variables in editor
    private int numberOfLives; //set number of lives in the inspector
    static public int lives; // number of lives for player 
    public int Lives { get { return lives; } set { lives = value; } }//access to private variable died [get/set methods]

    [SerializeField] //Access to private variables in editor
    [Tooltip("Check to test player lost the level")]
    private bool levelLost = false;//we have lost the level (ie. player died)
    public bool LevelLost { get { return levelLost; } set { levelLost = value; } } //access to private variable lostLevel [get/set methods]

    [Space(10)]
    public string defaultEndMessage = "Game Over";//the end screen message, depends on winning outcome
    public string looseMessage = "Oh no..."; //Message if player looses
    public string winMessage = "Right on!"; //Message if player wins
    [HideInInspector] public string endMsg;//the end screen message, depends on winning outcome

    [Header("SCENE SETTINGS")]
    [Tooltip("Name of the start scene")]
    public string startScene;

    [Tooltip("Name of the game over scene")]
    public string gameOverScene;

    [Tooltip("Count and name of each Game Level (scene)")]
    public string[] gameLevels; //names of levels
    [HideInInspector]
    public int gameLevelsCount; //what level we are on
    private int loadLevel; //what level from the array to load

    public static string currentSceneName; //the current scene name;

    [Header("FOR TESTING")]
    public bool nextLevel = false; //test for next level

    //Game State Varaiables
    [HideInInspector] public enum gameStates { Idle, Playing, Death, GameOver, BeatLevel };//enum of game states
    [HideInInspector] public gameStates gameState = gameStates.Idle;//current game state

    //Timer Varaibles
    private float currentTime; //sets current time for timer
    private bool gameStarted = false; //test if games has started

    //Win/Loose conditon
    [SerializeField] //to test in inspector
    private bool playerWon = false;

    //reference to system time
    private static string thisDay = System.DateTime.Now.ToString("yyyy"); //today's date as string


    /*** METHODS ***/

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        //runs the method to check for the GameManager
        CheckGameManagerIsInScene();

        //store the current scene
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;


    }//end Awake()


    // Update is called once per frame
    private void Update()
    {
        //if ESC is pressed , exit game
        if (Input.GetKey("escape")) { ExitGame(); }

        //If Time is up. Just to check timer and update it. SHOULD ONLY COUNT DOWN ONCE GAME STARTED/IS IN LEVEl
        if (gameStarted) { TimeCheck(); }
        
        //Check if player's hit moles match
        //checkMoles();

        //Check for next level
        if (nextLevel) { NextLevel(); }

        //if we are playing the game
        if (gameState == gameStates.Playing)
        {
            //if we have died and have no more lives, go to game over
            if (levelLost && (lives == 0)) { GameOver(); }

        }//end if (gameState == gameStates.Playing)


    }//end Update


    //LOAD THE GAME FOR THE FIRST TIME OR RESTART
    public void StartGame()
    {
        //SET ALL GAME LEVEL VARIABLES FOR START OF GAME

        gameLevelsCount = 1; //set the count for the game levels
        loadLevel = gameLevelsCount - 1; //the level from the array
        SceneManager.LoadScene(gameLevels[loadLevel]); //load first game level

        gameState = gameStates.Playing; //set the game state to playing

        lives = numberOfLives; //set the number of lives

        endMsg = defaultEndMessage; //set the end message default

        playerWon = false; //set player winning condition to false
        gameStarted = true; //The timer can start. This also will freeze and reset when the player wins or loses.
    }//end StartGame()

    public void initializeMoles()
    {
        sequencing = true;
        //Do a for loop through the Mole Holes and assign each a random index 
        //for (mole, moleArray)
        //{

        //}
    } //end initializeMoles()

    //Another method, but this time for the player's hits
    //public void playerHits()
    //{

    //}

    //Check if the contents of playerHits matches that of the first moles
    public void checkMoles()
    {
        //Loop thru each item in the array, if there is a mismatch break the loop player loses (restarts) level and loses a life (listed as an attempt)
        //An incomplete array would still be a mismatch?
        //Or only check it after finish?
        if (playerHits != moleArray) 
        {
            playerWon = false;
            GameOver();
        } 
    }

    //EXIT THE GAME
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exited Game");
    }//end ExitGame()


    //GO TO THE GAME OVER SCENE
    public void GameOver()
    {
        gameStarted = false;
        time = 30.0f;
        gameState = gameStates.GameOver; //set the game state to gameOver

       if(playerWon) { endMsg = winMessage; } else { endMsg = looseMessage; } //set the end message

        SceneManager.LoadScene(gameOverScene); //load the game over scene
        Debug.Log("Gameover");
    }
    
    
    //GO TO THE NEXT LEVEL
    void NextLevel()
    {
        nextLevel = false; //reset the next level

        //as long as our level count is not more than the amount of levels
        if (gameLevelsCount < gameLevels.Length)
        {
            gameLevelsCount++; //add to level count for next level
            loadLevel = gameLevelsCount - 1; //find the next level in the array
            SceneManager.LoadScene(gameLevels[loadLevel]); //load next level
            time = 30.0f; //Reset the timer

        }else{ //if we have run out of levels go to game over
            GameOver();
        } //end if (gameLevelsCount <=  gameLevels.Length)

    }//end NextLevel()

    public void TimeCheck()
    {
        if (time <= 0) {
            playerWon = false;
            GameOver();
        }
        else
        {
            time -= Time.deltaTime;
            Debug.Log(time);
        }
    } //end TimeCheck();

    //Static public method to get if the game currently is in sequencing phase. MAY NOT BE NEEDED, SO REMOVE
    public static bool getSequencing()
    {
        return gm.sequencing;
    } //end getSequencing()

    //Static public method to get the time
    public static float getTime()
    {
        return gm.time;
    } //end getTime()

}
