using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private GameObject robot;

    // Start is called before the first frame update
    void Start()
    {
        robot = GameObject.FindWithTag("Robot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col2d) 
    {
        if (col2d.gameObject.tag == "Player") 
        {
            GameController.instance.totalScore += 1;
            GameController.instance.UpdateScoreText();

            Destroy(gameObject);
        }
    }
}
