using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject playerObj = null;

    private Rigidbody2D playerRigidbody;

    [SerializeField]
    private Camera cam;

    private Vector3 dragOrigin;

    // Start is called before the first frame update
    void Start()
    {
        if (playerObj == null) {
            playerObj = GameObject.Find("Player");
        }

        playerRigidbody = GetComponent<Rigidbody2D> ();

        //Debug.Log("StratingPlayerPos" + playerObj.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private bool CheckDragDistanceFromPlayer(float MaxDistance)
    {
        if((Input.mousePosition.x >= playerObj.transform.position.x - MaxDistance || Input.mousePosition.x <= playerObj.transform.position.x + MaxDistance) &&
            (Input.mousePosition.y >= playerObj.transform.position.y - MaxDistance || Input.mousePosition.y <= playerObj.transform.position.y + MaxDistance)) 
        {
            return true;
        }

        return false;
        
    }

    private void MovePlayer()
    {
        if(Input.GetMouseButtonDown(0)) {
            dragOrigin = Input.mousePosition;
            //Debug.Log("StartingDragPos = " + dragOrigin);
        }

        // calculate distance between dragOrigin and new pos if the button it is still pressed

        if(Input.GetMouseButtonUp(0)) {
            Vector3 difference = dragOrigin - Input.mousePosition;

            //Debug.Log("EndingDragPos = " + Input.mousePosition);

            //Debug.Log("Difference = " + difference);

            //playerObj.transform.position += difference;

            if(Input.GetKey("space")){
                playerRigidbody.AddForce (new Vector2(2 * difference.x, 2 * difference.y));
            }
            else{
                playerRigidbody.AddForce (new Vector2(difference.x, difference.y));
            }
            

            //Debug.Log("NewPlayerPos = " + playerObj.transform.position);
        }

    }

}
