using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    public float speed = 0;
    public bool isGrounded;

    Animator heroAnimator;
    Rigidbody2D heroRigid;
    Vector3 heroPos;

    public GameObject heroCam;
    public GameObject groundChacker;

    void Start()
    {
        heroAnimator = gameObject.GetComponent<Animator>();
        heroRigid = gameObject.GetComponent<Rigidbody2D>();
        heroPos = gameObject.transform.position;
    }

    //void FixedUpdate()
    //{
    //    heroRigid.velocity = new Vector2(speed, 0f);
    //}

    void Update()
    {

        if (Input.GetAxis("Horizontal") != 0)
        {
            //speed += Time.deltaTime * 20;
            //if(speed >= 10)
            //{
            speed = 10;
            //}
        }
        else
        {
            //speed -= Time.deltaTime * 1000;
            speed = 0;
            //if (speed <= 0)
            //{
            //    speed = 0;
            //}
        }

        if(Input.GetAxis("Horizontal") < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        heroPos = new Vector3(heroPos.x + (Input.GetAxis("Horizontal") * Time.deltaTime * speed), heroPos.y, 0);
        transform.position = heroPos;
        heroAnimator.SetFloat("speed", speed);
    }

    void LateUpdate()
    {
        heroCam.transform.position = new Vector3(heroCam.transform.position.x, heroCam.transform.position.y, heroCam.transform.position.z);
    }
}
