using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Bullet bulletPrefab;

    private Rigidbody2D _playerRb;

    private bool _thrust;

    private float _turningDirection;
    public float thrustSpeed = 1.0f;
    public float turningSpeed = 1.0f;
    public float maxSpeed = 25.0f;


    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {   
        //just little input manager

        _thrust = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turningDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turningDirection = -1.0f;
        }
        else
        {
            _turningDirection = 0.0f;
        }

        // fly through the walls
        if (_playerRb.transform.position.y > 4.8f)
        {
            this._playerRb.transform.position = new Vector3(this._playerRb.transform.position.x, -4.8f, 0.0f);
        }

        if (_playerRb.transform.position.y < -4.8f)
        {
            this._playerRb.transform.position = new Vector3(this._playerRb.transform.position.x, 4.8f, 0.0f);
        }

        if (_playerRb.transform.position.x < -8.8f)
        {
            this._playerRb.transform.position = new Vector3(8.8f, this._playerRb.transform.position.y, 0.0f);
        }
        if (_playerRb.transform.position.x > 8.8f)
        {
            this._playerRb.transform.position = new Vector3(-8.8f, this._playerRb.transform.position.y, 0.0f);
        }

        // maximum velocity
        if (_playerRb.velocity.magnitude > maxSpeed)
        {
            _playerRb.velocity = _playerRb.velocity.normalized * maxSpeed;
        }

        

        // use if statement to shoot
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shooting();
        }
    }
    private void FixedUpdate()
    {
        if (_thrust)
        {
            _playerRb.AddForce(this.transform.up * this.thrustSpeed);
        }

        if (_turningDirection != 0.0f)
        {
            _playerRb.AddTorque(_turningDirection * turningSpeed);
        }
    }

   
    private void Shooting()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation); //instantiate bullet in the definite direction and position
        bullet.Ignite(this.transform.up); //send the vector2 direction to the function Ignite
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            _playerRb.velocity = Vector3.zero;
            _playerRb.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied(); // reference to the gamemanager p.s. I know this is bad(

        }
    }


}
