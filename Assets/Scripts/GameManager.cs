using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject jack;
    public GameObject boulesPlayer1;
    public GameObject boulesPlayer2;

    private Throwable jackThrowable;
    private Throwable player1Throwable;
    private Throwable player2Throwable;

    private bool jackThrown = false;
    private bool player1Thrown = false;
    private bool player2Thrown = false;

    private bool gameOver = false;

    private int player1Score = 0;
    private int player2Score = 0;

    void Start()
    {
        jackThrowable = jack.GetComponent<Throwable>();
        player1Throwable = boulesPlayer1.GetComponent<Throwable>();
        player2Throwable = boulesPlayer2.GetComponent<Throwable>();

        // Initially, only Jack is active
        jack.SetActive(true);
        boulesPlayer1.SetActive(false);
        boulesPlayer2.SetActive(false);
    }

    void Update()
    {
        // Check if the game is over
        if (gameOver)
        {
            // Perform game over actions (e.g., display scores, restart game, etc.)
            return;
        }

        if (!jackThrown)
        {
            // Check if Jack has been thrown
            if (jackThrowable.IsThrown() && jackThrowable.IsStoppedMoving())
            {
                jackThrown = true;
                StartCoroutine(ActivatePlayer1());
            }
        }
        else if (!player1Thrown)
        {
            // Check if Player 1 has been thrown
            if (player1Throwable.IsThrown() && player1Throwable.IsStoppedMoving())
            {
                player1Thrown = true;
                StartCoroutine(ActivatePlayer2());
            }
        }
        else if (!player2Thrown)
        {
            // Check if Player 2 has been thrown
            if (player2Throwable.IsThrown() && player2Throwable.IsStoppedMoving())
            {
                player2Thrown = true;
                StartCoroutine(DelayedScoreCalculation());
            }
        }
    }

    IEnumerator ActivatePlayer1()
    {
        yield return new WaitForSeconds(1f); // Wait for a short delay before activating Player 1
        boulesPlayer1.SetActive(true);
    }

    IEnumerator ActivatePlayer2()
    {
        yield return new WaitForSeconds(1f); // Wait for a short delay before activating Player 2
        boulesPlayer2.SetActive(true);
    }

    IEnumerator DelayedScoreCalculation()
    {
        yield return new WaitForSeconds(5f); // Delay the score calculation by 1 second

        CalculateScores();
        EndGame();
    }

    void CalculateScores()
    {
        // Calculate the distance between Jack and each player
        float distanceToPlayer1 = Vector3.Distance(jack.transform.position, boulesPlayer1.transform.position);
        float distanceToPlayer2 = Vector3.Distance(jack.transform.position, boulesPlayer2.transform.position);

        // Determine the player closest to Jack and award them a point
        if (distanceToPlayer1 < distanceToPlayer2)
        {
            player1Score++;
        }
        else
        {
            player2Score++;
        }
    }

    void EndGame()
    {
        // Set the game over flag
        gameOver = true;

        // Perform end game actions (e.g., display scores, restart game, etc.)
        Debug.Log("Game Over");
        Debug.Log("Player 1 Score: " + player1Score);
        Debug.Log("Player 2 Score: " + player2Score);
    }
}