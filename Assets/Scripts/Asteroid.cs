using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _asteroidRb;

    public Sprite[] sprites;

    public float size = 1.0f;
    public float[] sizes = new float[3] { 0.5f, 1.0f, 2.0f };
   
    public float speed = 50.0f;
    public float maxLifeTime = 20.0f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _asteroidRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)]; //random sprite type

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f); //random rotation for the asteroid

        size = sizes[Random.Range(0, sizes.Length)];
        this.transform.localScale = Vector3.one * this.size; // random size

        _asteroidRb.mass = this.size; // the larger the size the larger the mass
    }

    public void SetTrajectory(Vector2 direction)
    {
        _asteroidRb.AddForce(direction * this.speed); //add force in the definite direction

        Destroy(this.gameObject, this.maxLifeTime);//destroy after time 
    }


    //check the collision between asteroid and bullet
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) 
        {
            if (this.size * 0.5f >= 1.0f) // if the asteroid is big then it will be devided into two parts
            {
                CreateSplit();
                CreateSplit();
            }

            FindObjectOfType<GameManager>().AsteroidDestroyed(this); 
            Destroy(this.gameObject);
        }
    }


    //create a split of the asteroid
    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f; // set random position of the part of the big asteroid

        Asteroid half = Instantiate(this, position, this.transform.rotation);

        half.size = this.size * 0.5f; 

        half.SetTrajectory(Random.insideUnitCircle.normalized);
    }
}
