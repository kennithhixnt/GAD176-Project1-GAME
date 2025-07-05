using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPlayer : MonoBehaviour
{
    public KeyCode turnLeft;
    public KeyCode turnRight;

    public float moveSpeed = 5f;
    public float turnSpeed = 200f;

    private bool isAlive = true;

    void Update()
    {
        if (!isAlive) return;

        // Basic forward movement
        transform.Translate(Vector3.up * (moveSpeed * Time.deltaTime));

        // Turning controls
        if (Input.GetKey(turnLeft))
        {
            transform.Rotate(Vector3.forward * (turnSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(turnRight))
        {
            transform.Rotate(Vector3.back * (turnSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Add logic for hitting trails or walls here
        // Example: if player hits a trail tagged as "Trail"
        if (collision.CompareTag("Trail") || collision.CompareTag("Wall"))
        {
            Die();
        }
    }

    public void Die()
    {
        if (!isAlive) return;

        isAlive = false;

        // Notify GameManager the player died
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerDied();
        }

        // Disable the player visually and functionally
        gameObject.SetActive(false);

        // Optional: add death effects, sounds, etc.
    }
}




