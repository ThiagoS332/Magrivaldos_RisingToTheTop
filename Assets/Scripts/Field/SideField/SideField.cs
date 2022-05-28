using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideField : MonoBehaviour
{
    private string ballTag;

    private GameObject ballObj = null;

    private Rigidbody2D ballRB;

    public GameObject playerKickingArea;

    private GameObject fieldKick = null;

    private SideField_PlayerKickingArea instanceKickArea;

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
        if (fieldKick == null) {
            fieldKick = GameObject.FindGameObjectWithTag("KickingArea");
        }
    }

    private void SideKick(Vector2 ballPosEntrance)
    {
        if (fieldKick == null) {
            Debug.Log("Side kick");

            ballTag = ballObj.tag;
            Debug.Log("Ball speed set to zero");
            ballRB.velocity = new Vector2(0f, 0f);
            ballRB.angularVelocity = 0f;

            if(ballPosEntrance.y > 0){
                ballObj.transform.position = new Vector2(ballPosEntrance.x, ballPosEntrance.y - 0.15f);
                Instantiate(playerKickingArea, new Vector3(ballPosEntrance.x, ballPosEntrance.y, 0f), Quaternion.identity, this.transform);
            }
            else if(ballPosEntrance.y < 0){
                ballObj.transform.position = new Vector2(ballPosEntrance.x, ballPosEntrance.y + 0.15f);
                Instantiate(playerKickingArea, new Vector3(ballPosEntrance.x, ballPosEntrance.y, 0f), Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, 180f), this.transform);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        Vector2 ballPosEntrance = ballObj.transform.position;

        /*if(ballPosEntrance.y > 2f || ballPosEntrance.y < -2f){
            Debug.Log("BallPos on entrance = " + ballPosEntrance);

            SideKick(ballPosEntrance);
        }*/

        if(other.gameObject.name == "Ball"){
            SideKick(ballPosEntrance);
        }
        
    }
}
