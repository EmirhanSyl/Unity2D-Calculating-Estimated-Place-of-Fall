using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkColletion : MonoBehaviour
{
    public GameObject kontrolObject;
    public GameObject goingToLava;

    void Start()
    {
        kontrolObject.SetActive(false);
        goingToLava.SetActive(false);
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ground"))
        {
            kontrolObject.SetActive(true);
        }
        else if(collision.CompareTag("lava"))
        {
            goingToLava.SetActive(true);
        }
        else
        {
            //kontrolObject.SetActive(false);
        }

    }
    void OnTriggerExit2D(Collider2D collision2)
    {
        kontrolObject.SetActive(false);
        goingToLava.SetActive(false);
    }

}
