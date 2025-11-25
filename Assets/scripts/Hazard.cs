using System;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
  void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("Hazard"))
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }
    }
}
