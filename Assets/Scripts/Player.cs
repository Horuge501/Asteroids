using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerControls controls;

    private Rigidbody2D rb;
    public bool thrusting;
    public float thrustSpeed = 1f;
    private Vector2 turnDirection = Vector2.zero; // Inicializamos como Vector2.zero
    public float rotationSpeed = 1f;

    private void Awake() {
        controls = new PlayerControls();

        controls.PlayerActions.Thrust.started += ctx => thrusting = true;
        controls.PlayerActions.Thrust.canceled += ctx => thrusting = false;

        controls.PlayerActions.Turn.performed += ctx => turnDirection = ctx.ReadValue<Vector2>();
        controls.PlayerActions.Turn.canceled += ctx => turnDirection = Vector2.zero; // Resetear a Vector2.zero cuando se suelta

        controls.PlayerActions.Shoot.performed += ctx => Shoot();
    }

    private void Start() {
        controls.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (thrusting) {
            rb.AddForce(transform.up * thrustSpeed);
        }

        rb.AddTorque(-rotationSpeed * turnDirection.x);
    }

    private void Shoot() {
        Debug.Log("Shoot");
    }
}
