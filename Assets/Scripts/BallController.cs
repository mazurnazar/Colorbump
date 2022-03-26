using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BallController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Rigidbody camRb;
    [SerializeField] Renderer Player;
    public Material FriendMaterial, EnemyMaterial;
    [SerializeField] float thrust = 100f;
    

    [SerializeField] float BallSpeed;
   
    public UnityEngine.Camera myCam;

    [SerializeField] float minBallDist;
    [SerializeField] float maxBallDist;
    public float currentBallDist;
    private Vector2 lastMousePos;
    private GameManager gameManager;
    void Start()
    {
       
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if (!gameManager.GameStarted)
            return;
        BallCamDist();
        ForwardMovement();
        if (Input.GetMouseButton(0))
        {
            Movement();
        }
        
        gameManager.LevelPassState();
    }

    void Movement()
    {
        rb.velocity = Vector3.forward * BallSpeed;

        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            transform.position =  Vector3.Lerp(transform.position, new Vector3(hit.point.x, transform.position.y, hit.point.z),  BallSpeed * Time.deltaTime);
        }
    }
    void ForwardMovement()
    {
        rb.velocity = Vector3.forward * BallSpeed;
        Vector2 delta = Vector2.zero;

        if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePos = Input.mousePosition;
            if (lastMousePos == Vector2.zero)
                lastMousePos = currentMousePos;
            delta = currentMousePos - lastMousePos;
            lastMousePos = currentMousePos;
            Vector3 force = new Vector3(0, 0, delta.y) * thrust;
            if (currentBallDist < maxBallDist && currentBallDist > minBallDist)
            {
                rb.AddForce(force);
            }
        }
        else
        {
            lastMousePos = Vector2.zero;
        }
    }
    
    void BallCamDist()
    {
        currentBallDist = Vector3.Distance(new Vector3(0,0,myCam.transform.position.z), new Vector3(0,0,transform.position.z));
    }
    
   

}
