using UnityEngine;

public class fusion : MonoBehaviour
{
    public int NextLevel;
    public Animator animator;
    public bool Check = false;
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
        if (Check)
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(NextLevel);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Check = true;


        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        Check = false;
    }
}




