using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _bulletRb;

    public float speed = 500.0f;
    public float maxLifetime = 10.0f;

    private void Awake()
    {
        _bulletRb = GetComponent<Rigidbody2D>();
    }

    public void Ignite(Vector2 direction)
    {
        _bulletRb.AddForce(direction * this.speed); // add force in the direction of the player direction 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject); //if it collides with the boundary it will be destroyed;
    }
}
