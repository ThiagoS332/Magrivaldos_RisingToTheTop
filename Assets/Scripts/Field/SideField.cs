using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideField : MonoBehaviour
{
    private GameObject ballObj = null;

    private Rigidbody2D ballRB;

    // Start is called before the first frame update
    void Start()
    {
        if (ballObj == null) {
            ballObj = GameObject.Find("Ball");
            ballRB = ballObj.GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SideKick(Vector3 ballPosEntrance)
    {
        Debug.Log("Side kick");

        ballRB.velocity = new Vector2(0f, 0f);
        ballRB.angularVelocity = 0f;

        if(ballPosEntrance.y > 0){
            ballObj.transform.position = new Vector3(ballPosEntrance.x, 1.8f, 0f);
        }
        else if(ballPosEntrance.y < 0){
            ballObj.transform.position = new Vector3(ballPosEntrance.x, -1.8f, 0f);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        Vector3 ballPosEntrance = ballObj.transform.position;

        if(ballPosEntrance.y > 1.8f || ballPosEntrance.y < -1.8f){
            Debug.Log("BallPos on entrance = " + ballPosEntrance);

            SideKick(ballPosEntrance);
        }

        
        
    }
}
