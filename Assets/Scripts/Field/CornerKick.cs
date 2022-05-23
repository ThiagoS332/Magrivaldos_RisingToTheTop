using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerKick : MonoBehaviour
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
}
