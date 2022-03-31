using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sawLogic : MonoBehaviour
{
    public GameObject savePoint;
    Scene currentScene;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(currentScene.name);
        }
        else if(collision.CompareTag("block"))
        {
            Destroy(collision.gameObject);
        }

    }
}
