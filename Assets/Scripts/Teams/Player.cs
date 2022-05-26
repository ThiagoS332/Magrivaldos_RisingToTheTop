using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool block_selection;

    public bool selected;

    public bool playable;

    public bool moving;

    public bool moved;

    private float maxPullDist;

    private float minPullDist;

    private Vector3 dragOrigin;

    private Rigidbody2D playerRigidbody;

    public AudioSource grunt;

    public SpriteRenderer spriteRenderer;
    
    public Sprite unselectedSprite;

    public Sprite selectedSprite;

    private GameObject Teams = null;

    public Player(){
        this.maxPullDist = 400.0f;

        this.minPullDist = -400.0f;
    }

    public Player(float maxPullDist, float minPullDist){
        this.maxPullDist = maxPullDist;

        this.minPullDist = minPullDist;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Teams == null){
            Teams = GameObject.Find("Teams");
        }

        this.playerRigidbody = this.GetComponent<Rigidbody2D> ();

        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        this.playable = true;

        this.selected = false;

        this.block_selection = false;

        this.moved = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Teams.GetComponent<Teams>().player_turn_UI){
            block_selection = false;
        }
        else{
            block_selection = true;
        }*/

        if(this.selected){
            spriteRenderer.sprite = selectedSprite; 
        }
        else{
            spriteRenderer.sprite = unselectedSprite; 
        }

        if(this.selected && this.playable){
            MovePlayer();
        }

        if(Input.GetKey("escape")){
            Debug.Log("Deselected player");
            this.selected = false;
        }

        if(this.GetComponent<Rigidbody2D>().velocity == new Vector2(0f, 0f)){
            this.moving = false;
            this.moved = false;
            this.block_selection = false;
        }
        else{
            this.moving = true;
            this.block_selection = true;
        }

        /*if(this.moved){
            Debug.Log("Moving " + this.ToString());
        }*/
        
    }

    private void OnMouseDown()
    {
        if(!block_selection){
            this.selected = !this.selected;
        }
        else{
            this.selected = false;
        }

        //Debug.Log("Clicked on " + this.ToString());
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

            /*if(Input.GetKey("escape")){
                selected = false;
            }
            else{
                //Debug.Log("Difference = " + difference.normalized);
                this.moved = true;
                playerRigidbody.AddForce (new Vector3(difference.x, difference.y));
            }*/

            this.moved = true;

            if(moved){
                Debug.Log("Moved has been set to true");
            }

            playerRigidbody.AddForce (new Vector3(difference.x, difference.y));

            this.selected = false;

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
