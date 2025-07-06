using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TrailPlayer : MonoBehaviour
{
    [Header("Controls")] public KeyCode turnLeft = KeyCode.A;
    public KeyCode turnRight = KeyCode.D;

    [Header("Movement Settings")] public float moveSpeed = 1f;
    public float turnSpeed = 180f;

    public Rigidbody2D rb; // Rigidbody2D for physics-based movement
    public bool isAlive = true;

    public void Start()
    {
        // Get the Rigidbody2D component attached to this GameObject

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("TrailPlayer requires a Rigidbody2D.");
        }

        // Freeze rotation if using velocity-based movement
        rb.freezeRotation = true;
        rb.gravityScale = 0;
    }

    public void Update()
    {
        // check if player dead, stops proccessing inputs
        if (!isAlive) return;

        // Handle rotation
        if (Input.GetKey(turnLeft))
        {
            transform.Rotate(Vector3.forward * (turnSpeed * Time.deltaTime));
        }

        if (Input.GetKey(turnRight))
        {
            transform.Rotate(Vector3.back * (turnSpeed * Time.deltaTime));
        }
    }

    public void FixedUpdate()
    {
        // If the player is dead, stop moving
        if (!isAlive) return;

        // Constant forward movement
        rb.velocity = transform.up * moveSpeed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore collisions if already dead
        if (!isAlive) return;

        if (other.CompareTag("Wall"))
        {
            // Player hit a wall — die immediately
            Debug.Log($"{name} hit a wall!");
            Die();
        }
        // Player hit a trail check if it's someone else’s trail
        else if (other.CompareTag("Trail"))
        {
            TrailIdentity identity = other.GetComponent<TrailIdentity>();
// If the trail belongs to another player, die
            if (identity != null && identity.owner != this.gameObject)
            {
                Debug.Log($"{name} hit another player's trail!");
                Die();
            }
        }
    }

    /// <summary>
    /// Kills the player — stops movement and disables the GameObject.
    /// </summary>
    public void Die()
    {
        isAlive = false;
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}




