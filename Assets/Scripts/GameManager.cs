using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Player player;
    public float respawnTime = 3;
    public float invecibleTime = 3;
    public int lives = 3;
    public int score = 0;
    [SerializeField] private TextMeshProUGUI playerScore;
    [SerializeField] private TextMeshProUGUI playerLives;

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        if (asteroid.size < 0.75f)
        {
            score += 100;
            playerScore.text = "Score = " + score.ToString();
        }
        else if (asteroid.size < 1.25f)
        {
            score += 50;
            playerScore.text = "Score = " + score.ToString();
        }
        else
        {
            score += 25;
            playerScore.text = "Score = " + score.ToString();
        }
    }

    public void PlayerDied()
    {
        lives--;

        if (lives <= 0)
        {
            GameOver();
            playerLives.text = "Lives x" + lives.ToString();
        }
        else
        {
            Invoke("Respawn", respawnTime);
            playerLives.text = "Lives x" + lives.ToString();
        }
    }

    private void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        player.gameObject.SetActive(true);
        Invoke("TurnOnCollisions", invecibleTime);
    }

    private void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        lives = 3;
        playerLives.text = "Lives x" + lives.ToString();
        score = 0;
        playerScore.text = "Score =" + score.ToString();

        player.isActive = true;

        Invoke("Respawn", respawnTime);
    }
}
