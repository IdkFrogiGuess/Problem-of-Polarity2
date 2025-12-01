using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public bool buttonPressed = false;
    public bool hasBeenOpened = false;
    
    public GameObject door;
    public bool isHeavy = false;
    public Vector2 openPosition;
    private Vector2 closedPosition;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(openPosition,door.transform.localScale);
    }


    private void Start()
    {
        closedPosition = door.transform.position;
    }
    void Update()
    {
        if (buttonPressed)
        {
            door.transform.position = Vector2.Lerp(door.transform.position, openPosition, Time.deltaTime * 2);
            hasBeenOpened = true;
        }
        
        if( hasBeenOpened && isHeavy && buttonPressed == false)
        {
            door.transform.position = Vector2.Lerp( door.transform.position, closedPosition, Time.deltaTime*4);
        }
        if (hasBeenOpened && isHeavy && buttonPressed == true)
        {
            door.transform.position = Vector2.Lerp(door.transform.position, openPosition, Time.deltaTime * 2);
        }
    }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && isHeavy == false)
            {
                buttonPressed = true;
            }
        }
    
        void OnTriggerStay2D(Collider2D other)
        {
            if (isHeavy && other.CompareTag("Heavy"))
            {
                buttonPressed = true;
            }

        }
        void OnTriggerExit2D(Collider2D other)
        {
            if (isHeavy)
            {
                buttonPressed = false;
            }

        }
    }



