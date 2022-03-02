/**** 
 * Created by: Andrew Nguyen
 * Date Created: Feb 27, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Mar 1, 2022
 * 
 * Description: Manages Vivian and her hammer
****/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    //Singleton

    //VARIABLES//

    GameManager gm; //reference to game manager

    [Header("SET IN INSPECTOR")]
    public float speed = 2f; //Do not raise to 5 (original value). High number will cause it to clip through due to use of transform

    [Header("SET ON AWAKE")]
    public Rigidbody rb;
    public SpriteRenderer sr;
    public Animator apc;

    [Header("SET DYNAMICALLY")]
    public Vector2 lastPos;
    public Vector2 playerMovement;
    public Vector3 pos;
    public bool canMove;
    static public bool attacking = false; //When PC is attacking with hammer
    private void Awake()
    {
        //P = this;
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        apc = GetComponent<Animator>();
    } //End Awake

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GM; //find the game manager
    } //End Start

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attacking");
            attacking = true;
            apc.SetTrigger("Attacking");
        }

        if (attacking == true || gm.sequencing == true)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

        if (canMove)
        {
            MoveCharacter();
        }

        apc.SetFloat("WalkingVert", playerMovement.y);
        apc.SetFloat("WalkingSide", playerMovement.x);
        apc.SetFloat("Speed", playerMovement.magnitude);
    } //End Update

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

        if (playerMovement.x == 1 || playerMovement.x == -1 || playerMovement.y == 1 || playerMovement.y == -1)
        {
            lastPos = playerMovement;
            apc.SetFloat("LastVert", lastPos.y);
            apc.SetFloat("LastSide", lastPos.x);

        }
        
        //Flip direction
        if (playerMovement.x < 0 || lastPos.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        
    } //End Move Character

    private void Attack()
    {
        
    } //End Attack

    private void EndAttack()
    {
        attacking = false;
        apc.SetBool("isAttacking", attacking);
    }




}
