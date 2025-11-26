using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public bool buttonPressed = false;
    public GameObject door;
    
    void Update()
    {
        if (buttonPressed)
        {
            door.transform.Translate(Vector3.up * Time.deltaTime * 2);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            buttonPressed = true;
        }
    }
}
