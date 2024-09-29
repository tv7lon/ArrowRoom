using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class Player : MonoBehaviour
{
    //movement
    private Rigidbody2D rb;
    public float playerSpeed;
    private bool facingRight =true;

    //animation 
    private Animator anim;
    private bool isGrounded;

    //health
     HungerBar hungerBarMethod;

    //sound
    [SerializeField] private AudioSource eatCookieSound; 
    void Start()
    {
        //gets the component that is on the Hungerbar/scripts etc object
        hungerBarMethod = GameObject.Find("HungerBar").GetComponent<HungerBar>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

           //applies a velocity of A on the x axis and B on the y axis (dont want the y value to be different from gravity so just get the velocity that the rigidBody2d has already)
           rb.velocity = new Vector2(horizontalInput * playerSpeed, rb.velocity.y);

        //going right
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        //animation: horizontalInput = 0 means player is not moving (0!=0 is false). horizontal != 0 means player is moving (!0 != 0 is true)
        anim.SetBool("Moving", horizontalInput != 0);
        anim.SetBool("Grounded", isGrounded);

    }

    private void Flip()
    {
        transform.Rotate(0f, -180f, 0f);
        facingRight = !facingRight;
    }

    private void Jump()
    {
        //apply a velocity of SPEED upwards (on y axis + positive value) 
        rb.velocity = new Vector2(rb.velocity.x, playerSpeed);
        //use trigger for jumping: activates the transition immediately then resets automatically - instantaneous movements
        anim.SetTrigger("Jump");
        //character wont transition to idle in mid air
        isGrounded = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if player collides with the ground
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Food")
        {
            hungerBarMethod.DecreaseHunger(25);
            eatCookieSound.Play();
            //destroys the object (the cookie prefab) that caused the collision
            Destroy(collision.gameObject);
        }

    }
}
