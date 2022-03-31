using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updatedController : MonoBehaviour
{
    [Header("Some Numbers")]
    public float jumpForce = 8.0f;
    public float speed = 8.0f;
    public float moveDirection;
    public float heroTan;
    public float ladderSpeed;

    [Header("Calculate derivative")]
    float jumpPos1;
    float jumpPos2;
    float derivative;
    float timer;
    float ladderStayTime;

    [Header("Bool Checkings")]
    bool jump;
    bool grounded = true;
    bool isMoving;

    [Header("Components")]
    public GameObject cam;
    SpriteRenderer heroSprite;
    Rigidbody2D heroRig;
    Animator heroAnim;

    [Header("Vectors --->")]
    Vector2 jumpStartPos;
    Vector2 startingVelocity;

    [Header("Calculate Falling Point")]
    GameObject[] points;
    public GameObject point;
    public GameObject groundPosition;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    public bool willHerogrounded = true;
    public bool animBoolCont;
    //public bool heroGoingLava;

    void Start()
    {
        heroSprite = GetComponent<SpriteRenderer>();
        heroRig = GetComponent<Rigidbody2D>();
        heroAnim = GetComponent<Animator>();

        jumpPos1 = gameObject.transform.position.y;
        jumpPos2 = jumpPos1;

        points = new GameObject[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, transform.position, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        if (heroRig.velocity != Vector2.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        heroRig.velocity = new Vector2(speed * moveDirection, heroRig.velocity.y);
        if(jump)
        {
            heroRig.velocity = new Vector2(heroRig.velocity.x, jumpForce);
            //heroRig.AddForce(new Vector2(0, jumpForce));
            jump = false;

            heroTan = Mathf.Atan2(heroRig.velocity.y, heroRig.velocity.x) * Mathf.Rad2Deg;
            Debug.LogWarning("x ve y Velocity üçgen açýsý: " + heroTan);
        }

    }

    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log(derivative);

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if(Input.GetKey(KeyCode.A))
            {
                moveDirection = -1;
                heroSprite.flipX = true;
                heroAnim.SetFloat("speed", speed);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                moveDirection = 1;
                heroSprite.flipX = false;
                heroAnim.SetFloat("speed", speed);
            }
        }
        else if(grounded)
        {
            moveDirection = 0;
            heroAnim.SetFloat("speed", 0);
        }

        if(grounded && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            grounded = false;

            heroAnim.SetBool("jumpUp", true);
            heroAnim.SetBool("isJump", true);
            Debug.LogWarning("Yukarý Çýkýyor");

        }

        if (!willHerogrounded && heroAnim.GetBool("isJump"))
        {
            heroAnim.SetBool("falling", true);
        }
        else
        {
            heroAnim.SetBool("falling", false);
        }

        // Jump-Up Jump-Down Deðiþimi
        if (timer >= 0.01f)
        {
            jumpPos1 = gameObject.transform.position.y;
            derivative = jumpPos1 - jumpPos2;
            if(jumpPos1 != jumpPos2)
            {
                if(derivative < -0.05f)
                {
                    heroAnim.SetBool("jumpUp", false);
                    heroAnim.SetBool("jumpdown", true);
                    Debug.LogWarning("Aþaðý Düþüyor");
                    heroAnim.SetBool("isJump", true);

                    Debug.LogWarning("X Velocity: " + heroRig.velocity.x);
                    Debug.LogWarning("Y Velocity: " + heroRig.velocity.y);


                    jumpStartPos = groundPosition.transform.position;
                    startingVelocity = heroRig.velocity;

                    for (int i = 0; i < numberOfPoints; i++)
                    {
                        points[i].transform.position = fallDirection(i * spaceBetweenPoints);

                        if(points[i].transform.GetChild(0).gameObject.activeSelf)
                        {
                            willHerogrounded = true;
                            animBoolCont = true;
                        }
                        else if(points[i].transform.GetChild(0).gameObject.activeSelf)
                        {
                            //Debug.LogWarning(i + ". saniyede deðme tespit edilmedi!");
                        }
                        //else if(points[i].transform.GetChild(1).gameObject.activeSelf)
                        //{
                        //    heroGoingLava = true;
                        //}
                    }
                    if(!animBoolCont)
                    {
                        willHerogrounded = false;
                    }

                }
                jumpPos2 = jumpPos1;
            }
            timer = 0;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            grounded = true;
            heroAnim.SetBool("jumpUp", false);
            willHerogrounded = true;
            animBoolCont = false;

            if (heroAnim.GetBool("isJump"))
            {
                heroAnim.SetBool("jumpFinished", true);
                Debug.LogWarning("Yere Çarptý");
                heroAnim.SetBool("isJump", false);
                Debug.LogWarning("Zýplama Bitti");
                heroAnim.SetBool("jumpdown", false);

                //heroGoingLava = false;
                StartCoroutine(WaitForFinish());
            }
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            ladderStayTime = 0;
            heroAnim.SetBool("isClimb", false);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("ladder"))
        {
            ladderStayTime += Time.deltaTime;
            if (Input.GetKey(KeyCode.W))
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + Time.deltaTime * ladderSpeed, gameObject.transform.position.z);
                heroRig.velocity = new Vector2(0, 0);
                heroAnim.SetBool("isClimb", true);

            }
            else if(Input.GetKey(KeyCode.S))
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + Time.deltaTime * ladderSpeed * -1f, gameObject.transform.position.z);
                heroRig.velocity = new Vector2(0, 0);
                heroAnim.SetBool("isClimb", true);
            }
            else if (ladderStayTime >= 0.3f)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + Time.deltaTime * ladderSpeed * -1f, gameObject.transform.position.z);
                heroRig.velocity = new Vector2(0, 0);
                heroAnim.SetBool("isClimb", true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ladder"))
        {
            heroAnim.SetBool("isClimb", false);
        }
    }

    Vector2 fallDirection(float t)
    {
        //position = starting position + (starting velocity x time) + 0,5 * accelleration * t * t = Ne yedüðü belürsüz formül = yer deðiþtirme = Vilk * t + a * tKare /2
        Vector2 position = (Vector2)jumpStartPos + (startingVelocity * t) + 0.5f * Physics2D.gravity * (t * t);
        return position;
    }

    void LateUpdate()
    {
        cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, cam.transform.position.z);
    }

    IEnumerator WaitForFinish()
    {
        yield return new WaitForSeconds(0.2f);
        heroAnim.SetBool("jumpFinished", false);
        heroAnim.SetBool("isJump", false);
    }

}
