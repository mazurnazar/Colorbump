using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollide : MonoBehaviour
{
    Renderer Player;
    GameManager gameManager;
    Rigidbody camRb;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Ball").GetComponent<Renderer>();
        camRb = GameObject.Find("Main Camera").GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag  == "Ball" )
        {
            
            if (gameObject.GetComponent<Renderer>().material.color == collision.gameObject.GetComponent<Renderer>().material.color)
            {
                return;                
            }
            else
            {
                Destroy(collision.gameObject);
                gameManager.lossPanel.SetActive(true);
                camRb.velocity = Vector3.zero;
                gameManager.GameStarted = false;
            }
        }
    }
}
