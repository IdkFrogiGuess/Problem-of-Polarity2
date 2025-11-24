using UnityEngine;

public class PulleyElevator : MonoBehaviour
{
    public GameObject platform1;
    public GameObject platform2;
    public Vector3 offset;
    public Vector2 size;
    public LayerMask layer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit1 = Physics2D.BoxCast(platform1.transform.position + offset, size, 0, Vector2.zero, 0, layer);
        RaycastHit2D hit2 = Physics2D.BoxCast(platform2.transform.position + offset, size, 0, Vector2.zero, 0, layer);

        if(hit1)
        {
            Debug.Log("It works");
        }

        if(hit2)
        {
            Debug.Log("It is working");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(platform1.transform.position + offset, size);
        Gizmos.DrawWireCube(platform2.transform.position + offset, size);
        Gizmos.DrawLine(platform1.transform.position, platform2.transform.position);
    }
}
