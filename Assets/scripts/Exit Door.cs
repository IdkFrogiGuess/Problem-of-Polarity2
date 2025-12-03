
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    

    public FirstCheck Door1;
    public SecondCheck Door2;
    public bool ExitIsOpen = false;
    public int NextLevel;
    

    void Update()
    {
        if (Door1.CheckFirst == true && Door2.CheckSecond == true )
        {
            ExitIsOpen = true;
        }
        else
        {
            ExitIsOpen = false;
        }
        if (ExitIsOpen)
        {
            ExitOpen();
        }
    }

    void ExitOpen()
    {
        Debug.Log("Exit Opened!");
    
            UnityEngine.SceneManagement.SceneManager.LoadScene(NextLevel);
        
    }
}
