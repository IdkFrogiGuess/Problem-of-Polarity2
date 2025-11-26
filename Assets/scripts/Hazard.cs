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

    void Start()
    {
    }

    void Update()
    {
        if (isDead)
        {
            ParticleSystem particle = GetComponent<ParticleSystem>();
        }
    }

    // Called when this Collider2D/ Rigidbody2D has begun touching another Rigidbody2D/Collider2D
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            isDead = true;
            // Schedule the reload on a persistent reloader before destroying this object
            SceneReloader.ScheduleReload(reloadDelay);
            Destroy(gameObject, 0.25f);
        }
    }

    // Called when another Collider2D enters a trigger attached to this object (if either is marked as trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hazard"))
        {
            isDead = true;
            // Schedule the reload on a persistent reloader before destroying this object
            SceneReloader.ScheduleReload(reloadDelay);
            Destroy(gameObject, 0.25f);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Player destroyed");
    }

    // Private nested reloader MonoBehaviour to run the coroutine on a DontDestroyOnLoad object.
    private class SceneReloader : MonoBehaviour
    {
        private static SceneReloader s_instance;

        // Ensure a persistent SceneReloader exists and return it
        private static SceneReloader EnsureInstance()
        {
            if (s_instance == null)
            {
                var go = new GameObject("SceneReloader");
                DontDestroyOnLoad(go);
                s_instance = go.AddComponent<SceneReloader>();
            }
            return s_instance;
        }

        // Public entry to schedule a reload (called from Hazard instances)
        public static void ScheduleReload(float delay)
        {
            // If a reload was already scheduled, do nothing
            if (s_reloadScheduled)
                return;

            s_reloadScheduled = true;
            var inst = EnsureInstance();
            inst.StartCoroutine(inst.ReloadCoroutine(delay));
        }

        // Coroutine that waits then reloads the active scene and cleans up
        private IEnumerator ReloadCoroutine(float delay)
        {
            // Wait for the configured delay
            yield return new WaitForSeconds(delay);

            // Reload the active scene
            var active = SceneManager.GetActiveScene();
            SceneManager.LoadScene(active.buildIndex);

            // Reset the flag so future reloads can be scheduled
            s_reloadScheduled = false;

            // Destroy the reloader GameObject to avoid accumulating objects across reloads
            Destroy(this.gameObject);
            s_instance = null;
        }
    }
}
