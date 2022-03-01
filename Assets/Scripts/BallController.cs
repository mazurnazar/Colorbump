using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BallController : MonoBehaviour
{
    public GameObject Floor;
    public Rigidbody rb;
    public Rigidbody camRb;
    public Renderer Player;
    public Material FriendMaterial, EnemyMaterial;
    public float thrust = 100f;
    public float minCamDistance = 3f;
    
    public float BallSpeed;
    public float camSpeed;
    public Camera myCam;

    public float minBallDist;
    public float maxBallDist;
    public float currentBallDist;

    private Vector2 lastMousePos;
   
    void Start()
    {
        Floor.transform.tag = rb.tag;
    }
    void Update()
    {
        if (!GameManager.gameManager.GameStarted)
            return;
        if (Input.GetMouseButton(0))
        {
            Movement();
        }
        BallCamDist();
        ForwardMovement();
        CameraMovement();
        GameManager.gameManager.LevelPassState();

    }
    void Movement()
    {
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,100))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(hit.point.x, transform.position.y, transform.position.z), BallSpeed * Time.deltaTime);
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
            Vector3 force = new Vector3(0, 0, delta.y)*thrust;
            if (currentBallDist < maxBallDist && currentBallDist > minBallDist)
            {
                rb.AddForce(force);
            }
            else
            {
                if(currentBallDist>=maxBallDist && delta.y<0)
                {
                    rb.AddForce(force);
                }
                if(currentBallDist<=minBallDist && delta.y>0)
                {
                    rb.AddForce(force);
                }
            }
        }
        else
        {
            lastMousePos = Vector2.zero;
        }
    }
 
    private void OnCollisionEnter(Collision collision)
    {
        if (rb.tag == collision.gameObject.tag)
            return;
        else
        {
            Destroy(this.gameObject);
            GameManager.gameManager.lossPanel.SetActive(true);
            camRb.velocity =  Vector3.zero;
            GameManager.gameManager.GameStarted = false;
        }
        
    }
    void CameraMovement()
    {
        
        if (currentBallDist < minCamDistance)
            camRb.velocity = Vector3.forward * 0;
        else camRb.velocity = Vector3.forward * camSpeed;
    }
    
    
    void BallCamDist()
    {
        currentBallDist = Vector3.Distance(new Vector3(0,0,myCam.transform.position.z), new Vector3(0,0,transform.position.z));
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "FinishLine")
        {
            GameManager.gameManager.winPanel.SetActive(true);
            camRb.velocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            GameManager.gameManager.GameStarted = false;
           
        }
        if(other.tag == "ChangeColor")
        {
            rb.transform.tag = "Enemy";
            Player.material = EnemyMaterial;
            Floor.transform.tag = "Enemy";
        }
    }

}
