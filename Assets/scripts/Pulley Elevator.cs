using UnityEngine;

public class PulleyElevator : MonoBehaviour
{
    public GameObject platform1;
    private Rigidbody2D platform1rb;
    public GameObject platform2;
    private Rigidbody2D platform2rb;

    public Vector3 offset;
    public Vector2 size;
    public LayerMask layer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platform1rb=platform1.GetComponent<Rigidbody2D>();
        platform2rb=platform2.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit1 = Physics2D.BoxCast(platform1.transform.position + offset, size, 0, Vector2.zero, 0, layer);
        RaycastHit2D hit2 = Physics2D.BoxCast(platform2.transform.position + offset, size, 0, Vector2.zero, 0, layer);

        float weight1 = 0;
        if(hit1)
        {
            Debug.Log("It works");
            weight1 = hit1.rigidbody.mass;
        }

        float weight2 = 0;
        if(hit2)
        {
            Debug.Log("It is working");
            weight2 = hit2.rigidbody.mass;
        }

        float difference = weight2 - weight1;
        // if difference is positive then the right side should move down
        // if difference is negative then the left side will move down

        if (difference > 0)
        {
            platform2rb.linearVelocityY = -difference;
            platform1rb.linearVelocityY = difference;
        }
        else if (difference < 0)
        {
            platform1rb.linearVelocityY = difference;
            platform2rb.linearVelocityY = -difference;
        }
        else
        {
            platform1rb.linearVelocityY = 0;
            platform2rb.linearVelocityY = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(platform1.transform.position + offset, size);
        Gizmos.DrawWireCube(platform2.transform.position + offset, size);
        Gizmos.DrawLine(platform1.transform.position, platform2.transform.position);
    }
}
