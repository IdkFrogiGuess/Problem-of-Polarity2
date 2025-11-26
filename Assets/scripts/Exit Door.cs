using UnityEditor.Rendering;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public GameObject Door1;
    public GameObject Door2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() 
    {
        Door1 = GameObject.GetComponent<FirstCheck>().checkFirst;
        Door2 = GameObject.GetComponent<SecondCheck>().checkSecond;
    }
    void Update()
    {
       if (Door1 && Door2)
        {

        }
    }
    
}
