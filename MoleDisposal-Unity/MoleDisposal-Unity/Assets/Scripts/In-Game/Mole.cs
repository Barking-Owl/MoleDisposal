/**** 
 * Created by: Andrew Nguyen
 * Date Created: Feb 28, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Feb 28, 2022
 * 
 * Description: Manages individual moles. Considering they will be generated 

 * Is levelend instant?
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{

    //VARIABLES//
    [Header("SET ON AWAKE")]
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
            animate.SetTrigger("sequencing 0");
            sequencingTurn = true;
        }

        if (emerge)
        {
            animate.SetTrigger("levelStart");
        }

        if (retreat)
        {
            animate.SetTrigger("levelEnd");
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
                animate.SetTrigger("correct");
            }

            else if (hitIncorrect)
            {
                animate.SetTrigger("incorrect");
            }
        }
    }

    private void endSequence ()
    {
        sequencingTurn = false;
        animate.SetBool("sequencing", sequencingTurn);
    }
}
