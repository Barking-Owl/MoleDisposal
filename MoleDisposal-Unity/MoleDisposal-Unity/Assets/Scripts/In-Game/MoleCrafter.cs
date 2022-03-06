/**** 
 * Created by: Andrew Nguyen
 * Date Created: Mar 5, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Mar 5, 2022
 * 
 * Description: Creates moles
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleCrafter : MonoBehaviour
{
    
    GameManager gm; //reference to game manager

    [Header("SET IN INSPECTOR")]
    public static int maxMoles = 8; //Maximum amount of moles to be out
    public GameObject molePrefab; //Mole prefabs to be generated
    
    

    [Header("SET DYNAMICALLY")]
    public bool seq;
    public List<GameObject> mls;
    public GameObject[] moleInstances;

    private void Awake()
    {
        gm = GameManager.GM; //find the game manager
        
        //Initialize an array with a size of 8
        //moleInstances = new GameObject[maxMoles];
        moleInstances = GameObject.FindGameObjectsWithTag("MoleHoles");
        seq = gm.sequencing;
        mls = gm.moles;

        InitMoles();
    } //End Awake()

    // Start is called before the first frame update
    void Start()
    {
        //gm = GameManager.GM; //find the game manager

        //References to GameManager elements
        

    } //end Start()

    // Update is called once per frame
    void Update()
    {
        
    } //end Update()

    public void InitMoles()
    {
        GameObject mol;
        for (int i = 0; i < moleInstances.Length; i++)
        {
            mol = Instantiate<GameObject>(molePrefab);
            mol.name = "Mole " + i+1; //Identifiers for each individual mole. Since i starts at 0 we need to increment by 1 to match.
            //Set the parent for each
            mol.transform.SetParent(moleInstances[i].transform);

            //Position the mole
            mol.transform.parent = moleInstances[i].transform;
            mol.transform.position = moleInstances[i].transform.position;

            //Add it to the GM's mole array
            mls.Add(mol);

        } //end for

        //Scramble the mole order
        ScrambleMoles();
        BeginSequence();
        

    } //end InitMoles()

    //Scramble order of the array
    public void ScrambleMoles()
    {
        GameObject tmp;
        GameObject tmpM;
        for (int i = 0; i < moleInstances.Length-1; i++)
        {
            int j = Random.Range(i, moleInstances.Length);
            tmp = moleInstances[j];
            tmpM = mls[j];

            moleInstances[j] = moleInstances[i];
            mls[j] = mls[i];

            moleInstances[i] = tmp;
            mls[i] = tmpM;
        }
    } //end ScrambleMoles()
      //Then have each of them beep in sequence. Mole will have an event at the end to refer to the nextMole method
      //In the nextMole method get the next mole in the array, possibly through an int to check the indices
      //When the last one is done (through a check if) set sequencing to false and the timer may start and the player may move
      //Do a for loop through the Mole Holes and assign each a random index 
      //for (mole, moleArray)
      //{

    //}
    public void BeginSequence()
    {
        //Get the first component. Then, after that...
        //mls[0].GetComponent<Mole>().SetAnimate();
        for (int i = 0; i < mls.Count-1; i++)
        {
            mls[i].GetComponent<Mole>().SetAnimate();

            if(mls[i].GetComponent<Mole>().sequencingTurn == false)
            {
                mls[i + 1].GetComponent<Mole>().SetAnimate();
            }
        }
    }
}
