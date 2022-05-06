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

            Debug.Log("Difference = " + difference);

            //playerObj.transform.position += difference;

            //if(difference)

            if(difference.x > 100.0f){
                Debug.Log("Difference X is above 100");
                difference.x = 100.0f;
            }
            else if(difference.x < -100.0f){
                Debug.Log("Difference X is lower than -100");
                difference.x = -100.0f;
            }

            if(difference.y > 100.0f){
                difference.y = 100.0f;
            }
            else if(difference.y < -100.0f){
                Debug.Log("Difference Y is lower than -100");
                difference.y = -100.0f;
            }

            if(Input.GetKey("space")){
                playerRigidbody.AddForce (new Vector2(2 * difference.x, 2 * difference.y));
            }
            else{
                //Debug.Log("Difference = " + difference.normalized);
                playerRigidbody.AddForce (new Vector3(difference.x, difference.y));
            }
            

            //Debug.Log("NewPlayerPos = " + playerObj.transform.position);
        }

    }

    // Doesn't work because after the player is deactivated it cannot be activated again as the this script stops running
    private void ChangeStates()
    {
        if(Input.GetKeyDown("d")){
            playerObj.SetActive(true);
            Debug.Log("Ativei o player");
        }

        if(Input.GetKeyUp("d")){
            playerObj.SetActive(false);
            Debug.Log("Desativei o player");
        }
        
    }

}
