using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // What is the maximum speed we want the player to walk at
    public float maxSpeed = 5f;

    // Start facing right (like the sprite-sheet)
    private bool facingRight = true;

    // This will be a reference to the RigidBody2D 
    // component in the Player object
    public Rigidbody2D rb;

    // This is a reference to the Animator component
    public Animator anim;

    // If the player is clapping
    private bool clap = false;

    private void Update()
    {
        // If clap button is pressed, set clap to true
        if (Input.GetButtonDown("Clap"))
        {
            //anim.SetTrigger("Clap");
            clap = true;
        }
    }

    // We use FixedUpdate to do all the animation work
    void FixedUpdate()
    {

        // Get the extent to which the player is currently pressing left or right
        float h = Input.GetAxis("Horizontal");

        // Move the RigidBody2D (which holds the player sprite)
        // on the x axis based on the stae of input and the maxSpeed variable
        rb.velocity = new Vector2(h * maxSpeed, rb.velocity.y);

        // If clap bool is true, trigger the clapping animation
        // on the animator
        if (clap)
        {
            anim.SetTrigger("Clap");
        }
        // We reset the clap bool to false
        clap = false;

        // Check which way the player is facing 
        // and call reverseImage if neccessary
        if (h > 0 && !facingRight)
            reverseImage();
        else if (h < 0 && facingRight)
            reverseImage();

    }

    void reverseImage()
    {
        // Switch the value of the Boolean
        facingRight = !facingRight;

        // Get and store the local scale of the RigidBody2D
        Vector2 theScale = rb.transform.localScale;

        // Flip it around the other way
        theScale.x *= -1;
        rb.transform.localScale = theScale;
    }
}
