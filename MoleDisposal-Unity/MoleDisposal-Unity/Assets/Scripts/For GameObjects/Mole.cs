/**** 
 * Created by: Andrew Nguyen
 * Date Created: Feb 28, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Feb 28, 2022
 * 
 * Description: Manages individual moles. Considering they will be generated 
 * Possible to do: The close animation will also be them emerging from the holes, which is the animation in reverse. Could be done via trigger?
 * Same case for the beep as it should only do it once.
 * Is levelend instant?
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{

    //VARIABLES//
    [Header("SET ON AWAKE")]
    public Rigidbody rb;
    public SpriteRenderer sr;
    public Animator animate;

    [Header("SET DYNAMICALLY")]
    //SET AS ANIMATOR PARAMETERS
    public bool hitCorrect;
    public bool hitIncorrect;
    public bool emerge;
    public bool retreat;
    public bool sequencingTurn;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        animate = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sequencingTurn)
        {
            animate.SetBool("sequencing", sequencingTurn);
        }

        if (emerge)
        {
            animate.SetBool("levelStart", emerge);
        }

        if (retreat)
        {
            animate.SetBool("levelEnd", retreat);
        }

    }

    private void OnCollisionEnter(Collision col)
    {
        //Check if Vivian hit the mole and if she hit the correct one. Likely use of an array? Placeholder code

        GameObject collidedWith = col.gameObject;
        if (collidedWith.tag == "Player" && PlayerCharacter.attacking == true) 
        {
            if (hitCorrect)
            {
                animate.SetBool("correct", hitCorrect);
            }

            else if (hitIncorrect)
            {
                animate.SetBool("incorrect", hitIncorrect);
            }
        }
    }
}
