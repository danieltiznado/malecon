using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Player controller which contains the score of a player to display.
    private PlayerController playerController;

    // Text UI element in which we will display the score.
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = playerController.Score.ToString();
    }
}
