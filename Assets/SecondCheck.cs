using UnityEngine;

public class SecondCheck : MonoBehaviour
{
    public bool CheckSecond = false;
    public GameObject Player;


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckSecond = true;
        }
        else
        {
            CheckSecond = false;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        CheckSecond = false;
    }
}
