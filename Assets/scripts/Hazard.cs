using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    public bool isDead = false;
    public float reloadDelay = 3f; // seconds to wait before restarting the room

    // Prevent multiple hazards from scheduling multiple reloads
    private static bool s_reloadScheduled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Called when this Collider2D/ Rigidbody2D has begun touching another Rigidbody2D/Collider2D
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            isDead = true;
            Destroy(gameObject);
        }
    }

    // Called when another Collider2D enters a trigger attached to this object (if either is marked as trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hazard"))
        {
            isDead = true;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Hazard destroyed");
        
    }
}
