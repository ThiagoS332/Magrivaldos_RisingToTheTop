using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool block_selection;

    public bool selected;

    public bool playable;

    public bool moving;

    private bool moved;

    private float maxPullDist;

    private float minPullDist;

    private Vector2 originalPos = new Vector2 (0f, 0f);

    private Vector3 dragOrigin;

    private Rigidbody2D playerRigidbody;

    public AudioSource grunt;

    public SpriteRenderer spriteRenderer;
    
    public Sprite unselectedSprite;

    public Sprite selectedSprite;

    private GameObject Teams = null;

    private GameObject[] Magrivaldos = null;

    private GameObject ballObj = null;

    private Rigidbody2D ballRB;

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

        if(Magrivaldos == null){
            Magrivaldos = GameObject.FindGameObjectsWithTag("Team_1");
        }

        if (ballObj == null) {
            ballObj = GameObject.Find("Ball");
            ballRB = ballObj.GetComponent<Rigidbody2D>();
        }

        this.originalPos = this.transform.position;

        this.playerRigidbody = this.GetComponent<Rigidbody2D> ();

        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        this.playable = true;

        this.selected = false;

        this.block_selection = false;

        this.moved = false;
    }

    public bool getMoved(){
        return this.moved;
    }

    public bool getMoving(){
        return this.moving;
    }

    public bool getPlayable(){
        return this.playable;
    }

    public Vector2 getOriginalPos(){
        return this.originalPos;
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

        for(int i = 0; i < Magrivaldos.Length; i++){
            if(Magrivaldos[i].GetComponent<Player>().getMoving()){
                this.selected = false;
                this.block_selection = true;
                break;
            }
            else{
                this.block_selection = false;
            }
        }

        if(!ballObj.GetComponent<Ball>().getMoving()){
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
                this.selected = false;
            }

            if(ballRB.velocity == new Vector2 (0f, 0f)){
                StartCoroutine (CheckSpeed());
            }
        }
        
        
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

    private IEnumerator CheckSpeed(){
        yield return new WaitForSeconds(1);
        if(this.GetComponent<Rigidbody2D>().velocity == new Vector2(0f, 0f)){
            this.moving = false;
            this.moved = false;
            this.block_selection = false;
        }
        else{
            this.moving = true;
            this.moved = true;            
            this.block_selection = true;
        }
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
                playerRigidbody.AddForce (new Vector3(difference.x, difference.y));
            }*/

            this.moved = true;

            Debug.Log("Moved: " + moved + "(MovePlayer)");

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
