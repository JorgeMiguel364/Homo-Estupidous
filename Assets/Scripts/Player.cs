using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public float shockForce;
    public bool isJumping;

    private Rigidbody2D rb;
    private Animator anim;

    private Collider2D coll;
    private CircleCollider2D robotColl;

    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody2D>();
        anim = GetComponent <Animator>();

        coll = GetComponent <Collider2D>();
        robotColl = GameObject.FindWithTag("Robot").GetComponent <CircleCollider2D>();

        // Evita a colis�o entre o player e o rob�
        Physics2D.IgnoreCollision(coll, robotColl, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive == true) 
        {
            Mov();
            Jump();
        }

    }

    void Mov() 
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            transform.position += movement * Time.deltaTime * (Speed*2);
        }
        else 
        {
            transform.position += movement * Time.deltaTime * Speed;
        }

        if (Input.GetAxis("Horizontal") > 0f) 
        {
            anim.SetBool("walking", true);
            transform.eulerAngles = Vector3.zero;
        }

        if (Input.GetAxis("Horizontal") < 0f) 
        {
            anim.SetBool("walking", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if (Input.GetAxis("Horizontal") == 0f) 
        {
            anim.SetBool("walking", false);
        }
    }

    void Jump() 
    {
        if (Input.GetButtonDown("Jump") && !isJumping) 
        {
            rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            anim.SetBool("jumping", true);
        }
    }

    void RestartLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnCollisionEnter2D(Collision2D col2d) 
    {
        if (col2d.gameObject.layer == 3) 
        {
            isJumping = false;
            anim.SetBool("jumping", false);
        }

        if(col2d.gameObject.layer == 6) 
        {
            isAlive = false;
            anim.SetTrigger("death");
            coll.enabled = false;
            rb.gravityScale = 5f;
            rb.AddForce(transform.up * shockForce, ForceMode2D.Impulse);
        }
    }

    void OnBecameInvisible() 
    {
        SceneManager.LoadScene("Level1");
    }

    void OnCollisionExit2D(Collision2D col2d) 
    {
        if (col2d.gameObject.layer == 3)
        {
            isJumping = true;
        }
    }
}
