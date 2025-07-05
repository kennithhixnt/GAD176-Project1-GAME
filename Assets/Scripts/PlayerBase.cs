using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PlayerBase : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 200f;

    // Handle input (override in subclass)
    public abstract void HandleInput();

    // Move forward by default
    public virtual void MoveForward()
    {
        transform.Translate(Vector3.up * (speed * Time.deltaTime), Space.Self);
    }

    protected virtual void Update()
    {
        HandleInput();
        MoveForward();
    }
}


