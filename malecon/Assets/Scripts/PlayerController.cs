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

    // The threshold that will allow the player to clap a bit out of time and
    // still be counted as success
    [Range(0, 1)] public float beatThreshold = 0.1f;

    // If the player is clapping
    private bool clap = false;

    // Conductor that keeps track of the song's beats and time
    private Conductor conductor;

    // The song position in beats of the last time the clap button was pressed.
    private float lastClappedSongPositionInBeats = 1f;

    // Score of the player.
    private int _score = 0;
    public int Score { 
        get { return _score;  }
        set { _score = value; }
    }

    private void Start()
    {
        conductor = GameObject.Find("SongManager").GetComponent<Conductor>();
    }

    private void Update()
    {
        // If the player stopped clapping, the game is over and the score is resetted
        if(CheckIfUserStoppedClapping(conductor.SongPositionInBeats)) {
            ResetScore();
        }
        // If clap button is pressed, set clap to true
        if (Input.GetButtonDown("Clap"))
        {
            clap = true;
            // We get de decimals on of the beat
            float currentSongPositionInBeats = conductor.SongPositionInBeats;
            float songPositionInBeatsDecimal = (float)conductor.SongPositionInBeats % 1;
            // If the decimals fall inside the treshold we count it as success and the score is updated.
            if (songPositionInBeatsDecimal < beatThreshold || songPositionInBeatsDecimal > 1 - beatThreshold)
            {
                UpdateScore();
            } else
            {
                ResetScore();
            }
            lastClappedSongPositionInBeats = currentSongPositionInBeats;
        }
    }

    // We use FixedUpdate to do all the animation work
    private void FixedUpdate()
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

    private void reverseImage()
    {
        // Switch the value of the Boolean
        facingRight = !facingRight;

        // Get and store the local scale of the RigidBody2D
        Vector2 theScale = rb.transform.localScale;

        // Flip it around the other way
        theScale.x *= -1;
        rb.transform.localScale = theScale;
    }

    // Reset score to zero.
    private void ResetScore()
    {
        Score = 0;
    }

    // Update the score adding one unit.
    private void UpdateScore()
    {
        Score++;
    }

    // Check if a sec per beat with threshold has passed since the player clapped to determine
    // if the player has stopped clapping.
    private bool CheckIfUserStoppedClapping(float currentSongPositionInBeats)
    {
        if(currentSongPositionInBeats - lastClappedSongPositionInBeats >= conductor.SecPerBeat + beatThreshold)
        {
            return true;
        }
        return false;
    }
}
