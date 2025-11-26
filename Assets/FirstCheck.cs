using UnityEngine;

public class FirstCheck : MonoBehaviour
{
    public bool CheckFirst = false;
    public GameObject Player;


     void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            {
            CheckFirst = true;
        }
        else{
            CheckFirst = false;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        CheckFirst = false;
    }
}

