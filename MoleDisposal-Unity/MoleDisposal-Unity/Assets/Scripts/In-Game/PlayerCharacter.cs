/**** 
 * Created by: Andrew Nguyen
 * Date Created: Feb 27, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Mar 7, 2022
 * 
 * Description: Manages Vivian and her hammer
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    /*** VARIABLES ***/

    GameManager gm; //reference to game manager


    [Header("SET IN INSPECTOR")]
    public float speed = 2f; //Do not raise to 5 (original value). High number will cause it to clip through due to use of transform

    [Header("SET ON AWAKE")]
    public Rigidbody rb;
    public SpriteRenderer sr;
    static public Animator apc;

    [Header("SET DYNAMICALLY")]
    public Vector2 lastPos;
    public Vector2 playerMovement;
    public Vector3 pos;

    static public bool canMove;
    static public bool attacking = false; //When PC is attacking with hammer
    public List<GameObject> hits;

    /*** METHODS ***/
    private void Awake()
    {
        gm = GameManager.GM; //find the game manager
        hits = gm.playerHits;

        //Get components
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        apc = GetComponent<Animator>();
    } //End Awake()



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        } //end if

        if (attacking == true || gm.sequencing == true)
        {
            canMove = false;
        } //end if

        else if (!attacking || !gm.sequencing)
        {
            canMove = true;
        } //end else if

        if (canMove)
        {
            MoveCharacter();
        } //end if

        //Set animations for walking
        apc.SetFloat("WalkingVert", playerMovement.y);
        apc.SetFloat("WalkingSide", playerMovement.x);
        apc.SetFloat("Speed", playerMovement.magnitude);
    } //End Update()

    //Check if Vivian hit the mole and if she hit the correct one. 
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Mole" && attacking == true)
        {
            Debug.Log("Vivian hit the Mole");
            other.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            hits.Add(other.gameObject);
            gm.CheckMoles();
        } //end if
    } //end OnCollisionStay

    //We lost the level! Trigger an animation and set a flag to restart in animation (event)
    static public void LoseLevel()
    {
        apc.SetTrigger("Lose");
    } //end LoseLevel()

    public void LoseReload()
    {
        gm.LoseALife();
    } //end LoseReload()

    //We won the level! Trigger an animation and set a flag to restart in animation
    static public void WinLevel()
    {
        apc.SetTrigger("Win");
    } //end Winlevel()

    public void WinNext()
    {
        gm.NextLevel();
    } //end Winnext()

    //Move character
    private void MoveCharacter()
    {
        playerMovement.x = Input.GetAxis("Horizontal");
        playerMovement.y = Input.GetAxis("Vertical");

        //Move the character
        pos = transform.position;
        pos.x += (playerMovement.x * speed * Time.deltaTime);
        pos.z += (playerMovement.y * speed * Time.deltaTime);
        transform.position = pos;

        //Record last position
        if (playerMovement.x == 1 || playerMovement.x == -1 || playerMovement.y == 1 || playerMovement.y == -1)
        {
            lastPos = playerMovement;
            apc.SetFloat("LastVert", lastPos.y);
            apc.SetFloat("LastSide", lastPos.x);
        } //end if
        
        //Flip direction
        if (playerMovement.x < 0 || lastPos.x < 0)
        {
            sr.flipX = true;
        } //end if

        else
        {
            sr.flipX = false;
        } //end if

        
    } //End Move Character

    static public void Attack()
    {
        Debug.Log("Attacking");
        attacking = true;
        apc.SetTrigger("Attacking");
    } //end Attack()

    //Method that fires when Vivian finishes attacking
    private void EndAttack()
    {
        attacking = false;
        apc.SetBool("isAttacking", attacking);
    } //end EndAttack()

} //end PlayerCharacter class
