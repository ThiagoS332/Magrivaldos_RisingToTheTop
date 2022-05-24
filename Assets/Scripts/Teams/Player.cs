using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool selected;

    public bool playable;

    private float maxPullDist;

    private float minPullDist;

    private Vector3 dragOrigin;

    private Rigidbody2D playerRigidbody;

    public AudioSource grunt;


    public Player(){
        this.maxPullDist = 400.0f;

        this.minPullDist = -400.0f;

        this.playable = true;

        this.selected = false;
    }

    public Player(float maxPullDist, float minPullDist){
        this.maxPullDist = maxPullDist;

        this.minPullDist = minPullDist;

        this.playable = true;

        this.selected = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*if (playerObj == null) {
            playerObj = GameObject.Find("Magrivaldos");
        }*/

        playerRigidbody = this.GetComponent<Rigidbody2D> ();

        //Debug.Log("StratingPlayerPos" + playerObj.transform.position);

        /*Component[] playerComponents = this.GetComponents(typeof(Component));

        for(int i = 0; i < playerComponents.Length; i++){
            Debug.Log("i = " + i);
            Debug.Log("Component = " + playerComponents[i]);
        }*/

        /*Debug.Log("Name = " + this.ToString());
        Debug.Log("Playable = " + playerComponents[4].playable);
        Debug.Log("Selected = " + playerComponents[4].selected);*/
    }

    // Update is called once per frame
    void Update()
    {
        if(this.selected && this.playable){
            MovePlayer();
        }
        
    }

    private void OnMouseDown()
    {
        selected = !selected;

        Debug.Log("Clicked on " + this.ToString());
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Team_2" && !this.playable)
        {
            grunt.Play();
        }
    }

}
