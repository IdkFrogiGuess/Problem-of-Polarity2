using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public int NextLevel;
   public void ButtonStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(NextLevel);
    }

    public void ButtonCredits()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(NextLevel);
    }

   public void ButtonExit()
    {
        Debug.Log("Exit Game");
        Application.Quit();

    }
}
