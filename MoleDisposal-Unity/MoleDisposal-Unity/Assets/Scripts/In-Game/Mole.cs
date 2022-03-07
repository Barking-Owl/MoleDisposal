/**** 
 * Created by: Andrew Nguyen
 * Date Created: Feb 28, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Mar 6, 2022
 * 
 * Description: Manages individual moles. MoleCrafter generates moles

 * 
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{

    /*** VARIABLES ***/
    [Header("SET ON AWAKE")]
    public SpriteRenderer sr;
    public Animator animate;
    public Rigidbody rb;

    [Header("SET DYNAMICALLY")]
    public bool hitCorrect;
    public bool hitIncorrect;
    public bool sequencingTurn; //This is in addition to a trigger, so the Mole can go to the idle animation properly
    public bool endSequence; //Marks the end of an animation, useful for getting the next mole to play without fuss

    /*** METHODS ***/

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animate = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        endSequence = false;
    } //end Awake()

    // Start is called before the first frame update
    void Start()
    {
        
    } //end Start()

    // Update is called once per frame
    void Update()
    {

        if (hitCorrect)
        {
            animate.SetTrigger("correct");
        } //end if

        if (hitIncorrect)
        {
            animate.SetTrigger("incorrect");
        } //end if

    } //end Update()

    public void EndSequence()
    {
        Debug.Log("Ending animation");
        sequencingTurn = false;
        endSequence = true;
        animate.SetBool("sequencing", sequencingTurn); //To transition to idle
    } //end EndSequence()

    public void SetAnimate()
    {
        Debug.Log("Starting animation");
        sequencingTurn = true;
        animate.SetTrigger("sequencing 0");
        endSequence = false;
    } //end SetAnimate()
} //end Mole class
