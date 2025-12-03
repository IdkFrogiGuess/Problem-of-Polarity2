using UnityEngine;

public class SawbladePathfinding : MonoBehaviour
{
    //Point B has to be on the left and point A has to be on the right in order for code to work
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    private Animator anim;

    

    void Start()
    {
        //Get the rigidbody and animator from this object
        rb = GetComponent<Rigidbody2D>();
      currentPoint = pointB.transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }
        
       

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            //flip changes current point from point B to point A and starts travelling to that point
            flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            //flip changes current point from point A to point B and starts travelling to that point
            flip();
            currentPoint = pointB.transform;
        }
    }


    private void flip()
    { 
        //Local scale flips object when it reaches either point A or point B
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
