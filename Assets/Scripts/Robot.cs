using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private Animator anim;
    public float Speed;
    public Transform target;
    private Transform player;

    int tam;

    void Start()
    {
        anim = GetComponent <Animator>();
        player = GameObject.FindWithTag("Player").GetComponent <Transform>();
    }

    void Update()
    {
        Mov();
    }

    void Mov() 
    {
        // Segue o player at� uma certa dist�ncia
        if (Vector2.Distance(transform.position, target.position) > .8f)
        {
            anim.SetBool("isMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        // Atualiza a rota��o do sprite com base na posi��o do player
        if (transform.position.x < player.transform.position.x)
        {
            transform.eulerAngles = Vector3.zero;
        }

        if (transform.position.x > player.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
}
