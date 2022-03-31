using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDetecter : MonoBehaviour
{
    public bool collesionTriggered;
    updatedController controller;
    void Start()
    {
        controller = new updatedController();
    }

    
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            collesionTriggered = true;
        }
    }
}
