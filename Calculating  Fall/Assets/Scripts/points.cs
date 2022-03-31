using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class points : MonoBehaviour
{
    public int score;
    public int heart = 6;
    AudioSource _source;
    public TextMeshProUGUI scoreText;
    float painfulTime;
    Scene currentScene;

    public Image heartBar1;
    public Image heartBar2;
    public Image heartBar3;

    public Sprite fullHeart;
    public Sprite halfHeart;

    public GameObject startingPose;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        scoreText.text = ": " + score;
        if (currentScene.buildIndex == 1)
        {
            PlayerPrefs.SetInt("Score", 0);
        }
        else
        {
            score = PlayerPrefs.GetInt("Score");
        }
    }

    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        scoreText.text = ": " + score;
        PlayerPrefs.SetInt("Score", score);
        Debug.Log(PlayerPrefs.GetInt("Score"));

        switch(heart)
        {
            case 6:
                heartBar1.sprite = fullHeart;
                heartBar2.sprite = fullHeart;
                heartBar3.sprite = fullHeart;
                break;
            case 5:
                heartBar1.sprite = halfHeart;
                heartBar2.sprite = fullHeart;
                heartBar3.sprite = fullHeart;
                break;
            case 4:
                heartBar1.gameObject.SetActive(false);
                heartBar2.sprite = fullHeart;
                heartBar3.sprite = fullHeart;
                break;
            case 3:
                heartBar1.gameObject.SetActive(false);
                heartBar2.sprite = halfHeart;
                heartBar3.sprite = fullHeart;
                break;
            case 2:
                heartBar1.gameObject.SetActive(false);
                heartBar2.gameObject.SetActive(false);
                heartBar3.sprite = fullHeart;
                break;
            case 1:
                heartBar1.gameObject.SetActive(false);
                heartBar2.gameObject.SetActive(false);
                heartBar3.sprite = halfHeart;
                break;
            case 0:
                heartBar1.gameObject.SetActive(false);
                heartBar2.gameObject.SetActive(false);
                heartBar3.gameObject.SetActive(false);
                SceneManager.LoadScene("GameOver");

                break;
            default:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("diamond"))
        {
            score += 10;
            _source.Play();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Respawn"))
        {
            heart -= 2;
            gameObject.transform.position = startingPose.transform.position;
        }
        else if(collision.gameObject.CompareTag("thorn"))
        {
            heart -= 1;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("thorn"))
        {            
            painfulTime += Time.deltaTime;
            if(painfulTime >= 2.5f)
            {
                heart--;
                painfulTime = 0;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("thorn"))
        {
            painfulTime = 0;
        }
    }
}
