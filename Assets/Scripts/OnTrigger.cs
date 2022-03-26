using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] Renderer Player;
    private GameManager gameManager;
    [SerializeField] private Rigidbody camRb, rb;
    [SerializeField] Material EnemyMaterial;
    private BallController ballController;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ballController = GameObject.Find("Ball").GetComponent<BallController>();
       
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            if (gameObject.tag == "FinishLine")
            {
                Debug.Log("Finish");
                gameManager.winPanel.SetActive(true);
                gameManager.GameStarted = false;

                camRb.velocity = Vector3.zero;
                rb.velocity = Vector3.zero;
               
            }
            if (gameObject.tag == "ChangeColor")
            {
                Debug.Log("ChangeColor");
                Player.material = ballController.EnemyMaterial;

            }
        }
      
    }
}
