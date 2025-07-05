using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TrailPlayer : MonoBehaviour
{
    [Header("Controls")]
    public KeyCode turnLeft = KeyCode.A;
    public KeyCode turnRight = KeyCode.D;

    [Header("Movement Settings")]
    public float moveSpeed = 1f;
    public float turnSpeed = 180f;

    private Rigidbody2D rb;
    private bool isAlive = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("TrailPlayer requires a Rigidbody2D.");
        }

        // Freeze rotation if using velocity-based movement
        rb.freezeRotation = true;
        rb.gravityScale = 0;
    }

    private void Update()
    {
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

    private void FixedUpdate()
    {
        if (!isAlive) return;

        // Constant forward movement
        rb.velocity = transform.up * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAlive) return;

        if (other.CompareTag("Wall"))
        {
            Debug.Log($"{name} hit a wall!");
            Die();
        }
        else if (other.CompareTag("Trail"))
        {
            TrailIdentity identity = other.GetComponent<TrailIdentity>();

            if (identity != null && identity.owner != this.gameObject)
            {
                Debug.Log($"{name} hit another player's trail!");
                Die();
            }
        }
    }

    private void Die()
    {
        isAlive = false;
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);

     
    }
}




