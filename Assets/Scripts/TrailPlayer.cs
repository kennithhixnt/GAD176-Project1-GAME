using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPlayer : PlayerBase
{
    [Header("Input Keys")]
    public KeyCode turnLeft = KeyCode.A;
    public KeyCode turnRight = KeyCode.D;

    [Header("Trail")]
    public TrailManager trailManager;

    private void Start()
    {
        trailManager.UpdateTrail(transform.position);
        base.Update();
        trailManager.StartTrail(transform.position);
    }

    public override void HandleInput()
    {
        float turn = 0f;
        if (Input.GetKey(turnLeft)) turn += 1f;
        if (Input.GetKey(turnRight)) turn -= 1f;
        transform.Rotate(0f, 0f, turn * turnSpeed * Time.deltaTime);
    }

    protected override void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trail"))
        {
            TrailSegment trail = other.GetComponent<TrailSegment>();
            if (trail != null && trail.owner != this.gameObject)
            {
                Debug.Log($"{gameObject.name} hit another player's trail!");
                Destroy(this.gameObject);
                GameManager.Instance.PlayerDied(this.gameObject);
            }
        }
    }
    

}


