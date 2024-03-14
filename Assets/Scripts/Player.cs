using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerControls controls;

    private Rigidbody2D rb;
    [SerializeField] private bool thrusting;
    [SerializeField] private float thrustSpeed = 1f;
    private Vector2 turnDirection = Vector2.zero;
    public float rotationSpeed = 1f;
    private bool isActive = true;

    public Bullet bulletPrefab;

    private void Awake() 
    {
        controls = new PlayerControls();

        controls.PlayerActions.Thrust.started += ctx => thrusting = true;
        controls.PlayerActions.Thrust.canceled += ctx => thrusting = false;

        controls.PlayerActions.Turn.performed += ctx => turnDirection = ctx.ReadValue<Vector2>();
        controls.PlayerActions.Turn.canceled += ctx => turnDirection = Vector2.zero;

        //controls.PlayerActions.Shoot.started += ctx => Shoot();
        controls.PlayerActions.Shoot.performed += ctx => ShootRepeatedly();
        controls.PlayerActions.Shoot.canceled += ctx => CancelShoot();
    }

    private void Start() 
    {
        controls.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() 
    {
        if (thrusting) {
            rb.AddForce(transform.up * thrustSpeed);
        }

        rb.AddTorque(-rotationSpeed * turnDirection.x);
    }

    private void Shoot() 
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Fire(transform.up);
    }

    private void ShootRepeatedly()
    {
        if (!isActive || !IsInvoking("Shoot")) {
            InvokeRepeating("Shoot", 0f, 0.2f);
        }
    }

    private void CancelShoot()
    {
        CancelInvoke("Shoot");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;

            gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}