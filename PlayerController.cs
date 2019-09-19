﻿using System.Collections;
using UnityEngine.UI;
using UnityEngine;

//4-18-19 Added gamemaster, and initialized greg's player stats and coin tally to script
public class PlayerController : MonoBehaviour
{
    //player movement speed, which does not have an effect on jump speed
    public float speed;
    //jump height
    public float jumpForce;
    //decides direction of player movement
    public float moveInput;

    private Rigidbody2D rb;
    
    //what side is player sprite facing, default = right
    private bool facingRight = true;

    //these variables are for checking if on ground
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    //jumps left after 1st jump, so total jumps =extraJumps+1
    private int extraJumps;
    public int extraJumpsValue;

    private GameMaster gm;

    private PlayerStats ps;

    //sets up level
    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        ps = new PlayerStats(100, 100, 01);
    }
    //left + right movement
    void FixedUpdate()
    {
        //checks if feet are on ground, recommended checkRadius = 0.5 for small sprites
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        //movement according to input speed and direction
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput*speed,rb.velocity.y);

        //changes direction depending on movement input
        if(facingRight==false && moveInput > 0)
        {
            flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            flip();
        }
    }
    private void Update()
    {
        //sets number of jumps when player lands to a total of 3
        if (isGrounded == true)
        {
            extraJumps = 2;
        }
        //if up arrow is pressed and jumps havent been used up then character jumps and number of jumps is decremented
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps>0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        //if player is grounded but extra jumps is 0 the player will jump and no decrement will occur
        else if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded==true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    void flip()
    {
        //makes player sprite face opposite direction
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    //destroys coin if collision with coin
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            gm.points = gm.points + 1;
        }
    }
}