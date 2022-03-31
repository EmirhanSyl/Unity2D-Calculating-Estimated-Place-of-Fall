using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishDoor : MonoBehaviour
{
    int sceneNumber;

    private void Awake()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneNumber + 1);
            sceneNumber = SceneManager.GetActiveScene().buildIndex;
        }
    }

}
