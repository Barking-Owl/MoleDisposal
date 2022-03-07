/**** 
 * Created by: Andrew Nguyen
 * Date Created: Mar 5, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Mar 6, 2022
 * 
 * Description: Creates moles. Attached to Main Camera
 * 
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleCrafter : MonoBehaviour
{
    
    GameManager gm; //reference to game manager

    [Header("SET IN INSPECTOR")]
    public static int maxMoles = 4; //Maximum amount of moles to be out
    public GameObject molePrefab; //Mole prefabs to be generated



    [Header("SET DYNAMICALLY")]
    public int moleSeq;
    public bool seq;
    public List<GameObject> mls;
    public GameObject[] moleInstances;

    private void Awake()
    {
        gm = GameManager.GM; //find the game manager
        
        //Initialize an array with a size of 8
        //moleInstances = new GameObject[maxMoles];
        moleInstances = GameObject.FindGameObjectsWithTag("MoleHoles");

        //References to gamemanager elements
        seq = gm.sequencing;
        mls = gm.moles;

        //Others
        moleSeq = 0;
        
        InitMoles();
    } //End Awake()

    // Start is called before the first frame update
    void Start()
    {

        

    } //end Start()

    // Update is called once per frame
    void Update()
    {
        if (mls[moleSeq].GetComponent<Mole>().endSequence == true)
        {
            if (moleSeq < mls.Count-1)
            {
                moleSeq++;
                Debug.Log("Next mole playing: " + moleSeq);
                mls[moleSeq].GetComponent<Mole>().SetAnimate();
            } //end if (moleSeq < mls.Count)

            if (moleSeq == mls.Count-1)
            {
                gm.sequencing = false;

                //Issue I've run into is that after the very first attempt, be it next level or a retry, player can't move unless they attack.
                //This is an attempt to stitch that part of the game - as it can take away precious seconds. This also gives the player a telegraph that the game's begun,
                //and the timer is ticking.
                if (PlayerCharacter.canMove == false)
                {
                    Debug.Log("Character can't move, attacking");
                    PlayerCharacter.Attack(); 
                    mls[moleSeq].GetComponent<Mole>().endSequence = false;
                }
            }
        } //end if (mls[moleSeq].GetComponent<Mole>().endSequence == true)


        
    } //end Update()

    public void InitMoles()
    {
        GameObject mol;
        for (int i = 0; i < maxMoles; i++)
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

            if (moleInstances[i].transform.childCount > 0)
            {
                moleInstances[i].GetComponent<Collider>().enabled = false;
            }
            else
            {
                moleInstances[i].GetComponent<Collider>().enabled = true;
            }
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
        for (int i = 0; i < maxMoles-1; i++)
        {
            int j = Random.Range(i, maxMoles);
            tmp = moleInstances[j];
            tmpM = mls[j];

            moleInstances[j] = moleInstances[i];
            mls[j] = mls[i];

            moleInstances[i] = tmp;
            mls[i] = tmpM;
        }
    } //end ScrambleMoles()

    public void BeginSequence()
    {
        //Get the first component. Then, after that...
        mls[moleSeq].GetComponent<Mole>().SetAnimate();
        Debug.Log("Size of Mole List: " + mls.Count);
    } //end BeginSequence()

} //end MoleCrafter class
