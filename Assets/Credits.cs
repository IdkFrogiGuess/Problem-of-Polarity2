using UnityEngine;

public class Credits : MonoBehaviour
{
    // Set the target X/Y in the inspector (Y should be higher than current Y to move up).
    public Vector2 openPosition;
    // Movement speed multiplier for Lerp or Translate
    public float speed = 1f;
    // If true, credits will continuously move up; if false, they will move toward openPosition.y
    public bool moveIndefinitely = false;
    // Snap threshold to stop Lerp jitter
    private const float SnapThreshold = 0.01f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 center = new Vector3(openPosition.x, openPosition.y, transform.position.z);
        Vector3 size = transform.localScale;
        Gizmos.DrawWireCube(center, size);
    }

    void Update()
    {
        if (moveIndefinitely)
        {
          
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
            return;
        }

   
        Vector3 target = new Vector3(transform.position.x, openPosition.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);

       
        if (Mathf.Abs(transform.position.y - target.y) <= SnapThreshold)
        {
            transform.position = target;
        }
    }
}
