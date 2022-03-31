using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuLevelManager : MonoBehaviour
{

    public GameObject glitchEffect;
    public GameObject buttonsPanel;
    public GameObject heroStartPoint;
    public GameObject hero;
    public GameObject changeLevel1;
    public float waitTime;
    public int fallCount;
    string fallString;
    public TextMeshProUGUI fallText;

    Scene currentScene;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    void Start()
    {
        glitchEffect.SetActive(false);
        fallString = "";
    }


    void Update()
    {
        fallText.text = fallString;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            glitchEffect.SetActive(true);
            buttonsPanel.SetActive(false);
            fallCount++;
            StartCoroutine(RestartScene());

            switch(fallCount)
            {
                case 2:
                    fallString = "WHAT Are You Doing!? Don't Break the ORDER!";
                    break;
                case 4:
                    fallString = "DO You Think this is FUNNY!!";
                    break;
                case 6:
                    fallString = "STOP IT!!!";
                    break;
                case 8:
                    fallString = "!YOU WANT THIS!";
                    changeLevel1.SetActive(true);
                    break;
                case 10:
                    fallString = "What the- How did you do that? ANYWAY, DON'T DO THIS! YOU ARE BREAKING THE GAME!!!";
                    break;
            }
        }
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(waitTime);
        glitchEffect.SetActive(false);
        hero.transform.position = heroStartPoint.transform.position;
        hero.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        fallString = "";
        if(fallCount != 8)
        {
            buttonsPanel.SetActive(true);
        }

    }

}
