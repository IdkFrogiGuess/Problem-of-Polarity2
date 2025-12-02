using UnityEngine;

public class Credits : MonoBehaviour
{
   
    public Vector2 openPosition;
    
    public float speed = 1f;
  
    public bool moveIndefinitely = false;
   
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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed = (float)(speed + 0.1);

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            speed = (float)(speed - 0.1);

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            speed = 0;
        }

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
