using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player;

    public ParticleSystem explosion;

    public Image[] CurrentLives;
    public Sprite fullLive;
    public Sprite emptyLive;

    public UnityEngine.UI.Text scoreLabel;

    public int lives = 3;
    public int numberOfLives;
    public int score = 0;

    public float respawnTime = 3.0f;
    public float ignoreCollisionsTime = 3.0f;



    private void Update()
    {
        // life system
        if (lives > numberOfLives)
        {
            lives = numberOfLives;
        }

        for (int i = 0; i < CurrentLives.Length; i++)
        {
            if (i < lives)
            {
                CurrentLives[i].sprite = fullLive;
            }
            else
            {
                CurrentLives[i].sprite = emptyLive;
            }

            if (i < numberOfLives)
            {
                CurrentLives[i].enabled = true;
            }
            else
            {
                CurrentLives[i].enabled = false;
            }
        }

        scoreLabel.text = "" +  score;
    }
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if (asteroid.size <= 0.5f)
        {
            score += 20;
        }
        else if (asteroid.size < 1.0f)
        {
            score += 50;
        }
        else
        {
            score += 100;
        }
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        this.lives--;

        if (this.lives <= 0)
        {
            GameOver();
        }
        else 
        {
            Invoke("Respawn", respawnTime);
        }
        
    }

    private void Respawn()
    {

        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        this.player.gameObject.SetActive(true);
        Invoke("TurnOnCollisions", ignoreCollisionsTime);

    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    private void GameOver()
    {
        this.lives = 3;
        this.score = 0;

        Invoke("Respawn", this.respawnTime);
    }
}
