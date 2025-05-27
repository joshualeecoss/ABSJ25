using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Movement variables
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    // Collectable variables
    private GameObject nearbyCollectable;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Stops the Z-axis rotation when colliding with the objects
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Get input from player
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        moveVelocity = moveInput * moveSpeed;
        
        // Retrieve Collectable with E key
        if(Input.GetKeyDown(KeyCode.E) && nearbyCollectable != null)
        {
            Debug.Log("Collected: " + nearbyCollectable.name);
            // Here you can increase score, inventory, etc.\
            Destroy(nearbyCollectable);
            nearbyCollectable = null;
        }
    }

    void FixedUpdate()
    {
        // Apply movement in physics step
        rb.velocity = moveVelocity;
    }

    // Entering upon collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit Something: " + collision.gameObject.name);

        if(collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collided with an obstacle!");
            moveInput = new Vector2(0, 0);
        }
    }

    // Entering upon trigger activating
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered with: " + other.name);

        if (other.CompareTag("Collectable"))
        {
            nearbyCollectable = other.gameObject;
            Debug.Log("Can collect: " + other.name);
        }
    }

    void OnTriggerExit2D(Collider2D other)
{
    // Leaving upon trigger activating
    if (other.gameObject == nearbyCollectable)
    {
        nearbyCollectable = null;
        Debug.Log("Moved away from collectable.");
    }
}
}
