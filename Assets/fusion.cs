using UnityEngine;

public class fusion : MonoBehaviour
{
    public Animator animator;
    public bool CheckFirst = false;
    public GameObject Player;
    public GameObject Player2;
    public GameObject finalForm;
    public bool change = false;
    private void Start()
    {
        finalForm.SetActive(false);
    }

    private void Update()
    {
        if (CheckFirst)
        {
            change = true;
            if (change)
            {
                Player.SetActive(false);
                Player2.SetActive(false);
                finalForm.SetActive(true);
                

            }
        }
    }
    public void Changed()
    {
        animator.SetBool("Flex", true);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckFirst = true;


        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        CheckFirst = false;
    }
}




