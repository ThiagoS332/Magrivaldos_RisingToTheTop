using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideField_PlayerKickingArea : MonoBehaviour
{
    private bool kick = false;

    private bool ball_kicked = false;

    [SerializeField]
    private Camera cam;

    private GameObject playerObj = null;

    private GameObject[] players = null;

    private GameObject enemyObj = null;

    private GameObject[] enemies = null;

    private GameObject kickerObj = null;

    //private GameObject[] kickingTeamObj = null;

    //private Component[] kickingTeamComp;

    private GameObject ballObj;

    private Rigidbody2D ballRigidbody;

    private Vector3 ballPos;

    public SideField_PlayerKickingArea(float x, float y)
    {
        this.transform.position = new Vector3(x, y, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (players == null) {
            players = GameObject.FindGameObjectsWithTag("Team_1");
        }

        if (enemies == null) {
            enemies = GameObject.FindGameObjectsWithTag("Team_2");
        }

        /*if(ballObj.gameObject.tag == "BallTeam_2"){
            kickingTeamObj = GameObject.FindGameObjectsWithTag("Team_1");

            kickingTeamComp = new Component[kickingTeamObj.Length];
            for(int i = 0; i < kickingTeamObj.Length; i++){
                kickingTeamComp[i] = kickingTeamObj[i].GetComponent<Player>();
            }
        }
        else{
            kickingTeamObj = GameObject.FindGameObjectsWithTag("Team_2");

            kickingTeamComp = new Component[kickingTeamObj.Length];
            for(int i = 0; i < kickingTeamObj.Length; i++){
                kickingTeamComp[i] = kickingTeamObj[i].GetComponent<Enemy>();
            }
        }*/

        if(ballObj == null){
            ballObj = GameObject.Find("Ball");
            ballPos = ballObj.transform.position;
            ballRigidbody = ballObj.GetComponent<Rigidbody2D>();
            ballRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }

        if(cam == null){
            cam = GameObject.Find("GameCamera").GetComponent<Camera>();
        }

        for(int i = 0; i < players.Length; i++){
            //Debug.Log("Deactivating player " + players[i].ToString());
            //players[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            players[i].GetComponent<Player>().playable = false;
        }

        /*if(ballObj.gameObject.tag == "BallTeam_1" && players != null){
            for(int i = 0; i < players.Length; i++){
                Debug.Log("Deactivating player ", players[i].GetComponent<Enemy>());
                players[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            }
        }*/

        for(int i = 0; i < enemies.Length; i++){
            //Debug.Log("Deactivating enemy " + enemies[i].ToString());
            //enemies[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            enemies[i].GetComponent<Enemy>().playable = false;
        }

        /*if(ballObj.gameObject.tag == "BallTeam_2" && enemies != null){
            for(int i = 0; i < enemies.Length; i++){
                Debug.Log("Deactivating enemy ", enemies[i].GetComponent<Enemy>());
                enemies[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            }
        }*/

        Debug.Log("SideFieldKickingAreaPos = " + this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        /*if((kickerObj == null)){
            for(int i = 0; i < kickingTeamComp.Length; i++)
            {
                if(kickingTeamComp[i].selected){
                    kickerObj = kickingTeamObj[i];
                    if(this.transform.position.y > 0){
                        kickerObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, 0f);
                    }
                    else{
                        kickerObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.25f, 0f);
                    }
                    Kick(kickerObj);
                    DeactivateConstrains();
                    DestroyGameObject();
                    break;
                }
            }
        }
        else{
            kickerObj = null;
        }*/

        if(kickerObj == null){
            if(ballObj.gameObject.tag == "BallTeam_1"){
                for(int i = 0; i < enemies.Length; i++){
                    if(enemies[i].GetComponent<Enemy>().selected){
                        kickerObj = enemies[i];
                        break;
                    }
                }
            }
            else{
                for(int i = 0; i < players.Length; i++){
                    if(players[i].GetComponent<Player>().selected){
                        kickerObj = players[i];
                    }
                }
            }
        }

        if(kickerObj != null){
            this.kick = true;
            if(this.transform.position.y > 0){
                kickerObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, 0f);
            }
            else{
                kickerObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.25f, 0f);
            }
        }
        

        

        /*if((playerObj == null || !playerObj.GetComponent<Player>().selected) && ballObj.gameObject.tag == "BallTeam_2"){
            for(int i = 0; i < players.Length; i++){
                if(players[i].GetComponent<Player>().selected){
                    playerObj = players[i];
                    if(this.transform.position.y > 0){
                        playerObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, 0f);
                    }
                    else{
                        playerObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.25f, 0f);
                    }
                    Kick(playerObj);
                    DeactivateConstrains();
                    DestroyGameObject();
                    break;
                }
                else{
                    playerObj = null;
                }
            }
        }*/

        

        /*if((enemyObj == null || !enemyObj.GetComponent<Enemy>().selected) && ballObj.gameObject.tag == "BallTeam_1"){
            for(int i = 0; i < enemies.Length; i++){
                if(enemies[i].GetComponent<Enemy>().selected){
                    Debug.Log("Encontrei o inimigo selecionado");
                    enemyObj = enemies[i];
                    if(this.transform.position.y > 0){
                        enemyObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, 0f);
                    }
                    else{
                        enemyObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.25f, 0f);
                    }
                    Kick(enemyObj);
                    DeactivateConstrains();
                    DestroyGameObject();
                    break;
                }
                else{
                    enemyObj = null;
                }
            }
        }*/

        if(this.kick){
            DeactivateConstrains();
            DestroyGameObject();
        }

        /*if(this.kick && kickerObj != null && !ball_kicked){
            Kick(kickerObj);
        }
        else if(this.kick && kickerObj == null){
            this.kick = false;
        }

        if(ball_kicked){
            DestroyGameObject();
        }*/
    }

    private void Kick(GameObject kicker)
    {
        if(ballObj.gameObject.tag == "BallTeam_1"){
            kicker.GetComponent<Enemy>().playable = true;
        }
        else{
            kicker.GetComponent<Player>().playable = true;
        }

        Vector3 ballPosSideKick = ballObj.transform.position;

        ballRigidbody.constraints = RigidbodyConstraints2D.None;
        
        if(ballPosSideKick != ballPos){
            ball_kicked = true;
            DeactivateConstrains();
            if(ballObj.gameObject.tag == "BallTeam_1"){
                kicker.GetComponent<Player>().playable = false;
            }
            else{
                kicker.GetComponent<Enemy>().playable = false;
            }
        }
    }

    private void DeactivateConstrains()
    {
        ballRigidbody.constraints = RigidbodyConstraints2D.None;

        for(int i = 0; i < players.Length; i++){
            Debug.Log("Activating player " + players[i].ToString());
            //players[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            players[i].GetComponent<Player>().selected = false;
            players[i].GetComponent<Player>().playable = true;
        }

        for(int i = 0; i < enemies.Length; i++){
            Debug.Log("Activating enemy "+ enemies[i].ToString());
            //enemies[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            enemies[i].GetComponent<Enemy>().selected = false;
            enemies[i].GetComponent<Enemy>().playable = true;
        }
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
    private /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        Debug.Log("Cliquei na área de lateral");
        if(playerObj != null)
            playerObj.transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
