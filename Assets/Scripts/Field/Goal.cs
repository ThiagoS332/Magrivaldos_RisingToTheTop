using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
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

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Debug.Log("depois da espera");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //StartCoroutine(ExecuteAfterTime(10));

        Debug.Log("GOOOOOOOOOOOOOOOOOL da alemanha");

        ballObj.transform.position = new Vector3(0f, 0f, 0f);

        ballRB.velocity = new Vector2(0f, 0f);
        ballRB.angularVelocity = 0f;
    }
}
