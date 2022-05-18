using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerBackField : MonoBehaviour
{
    public GameObject ballObj;

    private Rigidbody2D ballRB;

    // Start is called before the first frame update
    void Start()
    {
        if (ballObj == null) {
            ballObj = GameObject.Find("Ball");
            
        }

        ballRB = ballObj.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CornerKick(Vector3 ballPosEntrance)
    {

        Debug.Log("Escanteio ou Tiro de Meta? Eis a questão");

        if(ballPosEntrance.x > 0){
            if(ballObj.gameObject.tag == "BallTeam_2"){
                Debug.Log("Escanteio para o MAAAAAAAGRIIIIVAAAALDOOOOOOOOOOOS");

                ballObj.transform.position = new Vector3(4.93f, -1.91f, 0f);

                ballRB.velocity = new Vector2(0f, 0f);
                ballRB.angularVelocity = 0f;
            }
            else{
                Debug.Log("O árbitro ta roubando");

                ballObj.transform.position = new Vector3(4.205f, -0.01f, 0f);

                ballRB.velocity = new Vector2(0f, 0f);
                ballRB.angularVelocity = 0f;
            }
        }
        else{
            if(ballObj.gameObject.tag == "BallTeam_1"){
                Debug.Log("O árbitro ta roubando");

                ballObj.transform.position = new Vector3(-4.92f, -1.91f, 0f);

                ballObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                ballObj.GetComponent<Rigidbody2D>().angularVelocity = 0f;
            }
            else{
                Debug.Log("Tiro de meta pro MAAAAAAAGRIIIIVAAAALDOOOOOOOOOOOS");

                ballObj.transform.position = new Vector3(-4.195f, -0.01f, 0f);

                ballRB.velocity = new Vector2(0f, 0f);
                ballRB.angularVelocity = 0f;
            }
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Vector3 ballPosEntrance = ballObj.transform.position;

        if((ballPosEntrance.x > 5.0f || ballPosEntrance.x < -5.0f) && ballPosEntrance.y < -0.5f){
            Debug.Log("Bola de escanteio = " + ballPosEntrance);

            CornerKick(ballPosEntrance);
        }
        
    }
}
