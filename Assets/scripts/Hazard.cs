using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    public bool isDead = false;
    public float reloadDelay = 3f; // seconds to wait before restarting the room

    // Optional particle system prefab to spawn on death.
    // If left null, the script will look for a ParticleSystem on this GameObject or its children and clone it.
    [SerializeField] private ParticleSystem deathParticlesPrefab = null;

    // Prevent multiple hazards from scheduling multiple reloads
    private static bool s_reloadScheduled = false;

    void Start()
    {
    }

        // Called when this Collider2D/ Rigidbody2D has begun touching another Rigidbody2D/Collider2D
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
            return;

        if (collision.gameObject.CompareTag("Hazard"))
        {
            isDead = true;
            PlayDeathParticles();
            // Schedule the reload on a persistent reloader before destroying this object
            SceneReloader.ScheduleReload(reloadDelay);
            Destroy(gameObject, 0.25f);
        }
    }

    // Called when another Collider2D enters a trigger attached to this object (if either is marked as trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead)
            return;

        if (other.CompareTag("Hazard"))
        {
            isDead = true;
            PlayDeathParticles();
            // Schedule the reload on a persistent reloader before destroying this object
            SceneReloader.ScheduleReload(reloadDelay);
            Destroy(gameObject, 0.25f);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Player destroyed");
    }

    // Spawn or play particle effects for death in a way that is independent of this GameObject's lifetime.
    private void PlayDeathParticles()
    {
        // Instantiate a prefab if provided
        if (deathParticlesPrefab != null)
        {
            var psGo = Instantiate(deathParticlesPrefab.gameObject, transform.position, transform.rotation);
            var ps = psGo.GetComponent<ParticleSystem>();
            if (ps != null)
                ps.Play();

            // Compute a destroy time: duration + max start lifetime, fallback if looping
            float destroyTime = EstimateParticleLifetime(ps);
            Destroy(psGo, destroyTime);
            return;
        }

        // Otherwise try to find a ParticleSystem attached to this object or its children
        var localPs = GetComponentInChildren<ParticleSystem>();
        if (localPs != null)
        {
            // Clone the particle system GameObject so it can play after this object is destroyed
            var psGo = Instantiate(localPs.gameObject, localPs.transform.position, localPs.transform.rotation);
            var ps = psGo.GetComponent<ParticleSystem>();
            if (ps != null)
                ps.Play();

            float destroyTime = EstimateParticleLifetime(ps);
            Destroy(psGo, destroyTime);
        }
        // If no particle system found and no prefab assigned, do nothing.
    }

    // Estimate how long to keep the instantiated particle GameObject alive.
    // Uses ParticleSystem.main.duration + main.startLifetime.constantMax when available.
    private float EstimateParticleLifetime(ParticleSystem ps)
    {
        if (ps == null)
            return 2f; // safe default

        var main = ps.main;

        // If the system loops, we cannot rely on duration; choose a safe default to avoid leaks.
        if (main.loop)
            return 5f;

        float duration = main.duration;
        float startLifetimeMax = 0f;

        // Try to get a reasonable max start lifetime; constantMax is available for MinMaxCurve
        try
        {
            startLifetimeMax = main.startLifetime.constantMax;
        }
        catch
        {
            // Fallback if APIs differ; try constant
            try
            {
                startLifetimeMax = main.startLifetime.constant;
            }
            catch
            {
                startLifetimeMax = 0.5f;
            }
        }

        // Add a small buffer
        return Mathf.Max(0.5f, duration + startLifetimeMax + 0.1f);
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
