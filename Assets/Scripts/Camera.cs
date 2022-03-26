using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Rigidbody camRb;
    private float camSpeed = 3f;
    float minCamDistance = 3f;

    private BallController ballController;
    private GameManager gameManager;
    // Start is called before the first frame update
   void Start()
    {
        camRb = GetComponent<Rigidbody>();
        ballController = GameObject.Find("Ball").GetComponent<BallController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GameStarted)
        {
            CameraMovement();
        }
    }
    void CameraMovement()
    {

        if (ballController.currentBallDist < minCamDistance)
            camRb.velocity = Vector3.forward * 0;
        else camRb.velocity = Vector3.forward * camSpeed;
    }
}
