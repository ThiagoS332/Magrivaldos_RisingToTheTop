using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private GameObject playerObj = null;

    private Rigidbody2D playerRigidbody;

    public bool selected = false;

    [SerializeField]
    private Camera cam;

    private Vector3 dragOrigin;

    // Start is called before the first frame update
    void Start()
    {
        /*if (playerObj == null) {
            playerObj = GameObject.Find("Magrivaldos");
        }*/

        playerRigidbody = GetComponent<Rigidbody2D> ();

        //Debug.Log("StratingPlayerPos" + playerObj.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(selected){
            MovePlayer();
        }
        
    }

    private void OnMouseDown()
    {
        selected = true;
    }

    private void MovePlayer()
    {
        //Debug.Log("Move Player");

        if(Input.GetMouseButtonDown(0)) {
            dragOrigin = Input.mousePosition;
            //Debug.Log("StartingDragPos = " + dragOrigin);
        }

        // calculate distance between dragOrigin and new pos if the button it is still pressed

        if(Input.GetMouseButtonUp(0)) {
            Vector3 difference = dragOrigin - Input.mousePosition;

            //Debug.Log("Difference = " + difference);

            Vector3 normalized_difference = difference.normalized;

            //Debug.Log("Normalized Difference = " + normalized_difference);

            Vector3 force_generated = difference.magnitude * normalized_difference;

            //Debug.Log("Applied Force = " + force_generated);

            if(difference.x > 400.0f){
                difference.x = 400.0f;
            }
            else if(difference.x < -400.0f){
                difference.x = -400.0f;
            }

            if(difference.y > 400.0f){
                difference.y = 400.0f;
            }
            else if(difference.y < -400.0f){
                difference.y = -400.0f;
            }

            if(Input.GetKey("space")){
                playerRigidbody.AddForce (new Vector2(difference.x, difference.y));
                selected = false;
            }
            else{
                //Debug.Log("Difference = " + difference.normalized);
                playerRigidbody.AddForce (new Vector3(difference.x, difference.y));
                selected = false;
            }
            

            //Debug.Log("NewPlayerPos = " + playerObj.transform.position);
        }

    }

    // Doesn't work because after the player is deactivated it cannot be activated again as the this script stops running
    /*private void ChangeStates()
    {
        if(Input.GetKeyDown("d")){
            playerObj.SetActive(true);
            Debug.Log("Ativei o player");
        }

        if(Input.GetKeyUp("d")){
            playerObj.SetActive(false);
            Debug.Log("Desativei o player");
        }
        
    }*/

}
