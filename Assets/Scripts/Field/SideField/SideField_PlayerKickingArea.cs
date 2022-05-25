using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideField_PlayerKickingArea : MonoBehaviour
{
    private bool kick = false;

    //private bool ball_kicked = false;

    [SerializeField]
    private Camera cam;

    //private GameObject Teams = null;

    //private GameObject playerObj = null;

    private GameObject[] magrivaldos = null;

    //private GameObject enemyObj = null;

    private GameObject[] enemies = null;

    private GameObject kickerObj = null;

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
        /*if(Teams == null){
            Teams = GameObject.Find("Teams");
            Teams.GetComponent<Teams>().player_turn = !player_turn;
        }*/

        if (magrivaldos == null) {
            magrivaldos = GameObject.FindGameObjectsWithTag("Team_1");
        }

        if (enemies == null) {
            enemies = GameObject.FindGameObjectsWithTag("Team_2");
        }

        if(ballObj == null){
            ballObj = GameObject.Find("Ball");
            ballPos = ballObj.transform.position;
            ballRigidbody = ballObj.GetComponent<Rigidbody2D>();
            ballRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }

        if(cam == null){
            cam = GameObject.Find("GameCamera").GetComponent<Camera>();
        }

        Debug.Log("Deactivating playbility of Magrivaldos players (SideField.cs - Start())");
        // Deactivate the playbility of the Magrivaldos's players
        for(int i = 0; i < magrivaldos.Length; i++){
            magrivaldos[i].GetComponent<Player>().playable = false;
        }

        Debug.Log("Deactivating playbility of enemy players (SideField.cs - Start())");
        // Deactivate the playbility of the enemy's players
        for(int i = 0; i < enemies.Length; i++){
            enemies[i].GetComponent<Enemy>().playable = false;
        }

        Debug.Log("SideFieldKickingAreaPos = " + this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(kickerObj == null){
            //Debug.Log("Looking for kicker");
            if(ballObj.tag == "BallTeam_1"){
                for(int i = 0; i < enemies.Length; i++){
                    if(enemies[i].GetComponent<Enemy>().selected){
                        kickerObj = enemies[i];
                        break;
                    }
                }
            }
            else{
                for(int i = 0; i < magrivaldos.Length; i++){
                    if(magrivaldos[i].GetComponent<Player>().selected){
                        kickerObj = magrivaldos[i];
                        break;
                    }
                }
            }
        }

        if(kickerObj != null){
            this.kick = true;

            Debug.Log("Object parent: " + this.transform.parent.name);

            string object_parent = this.transform.parent.name;

            if(this.transform.position.y > 0){
                if(object_parent == "Upper"){
                    Debug.Log("Side kick in the upper field");
                    kickerObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, 0f);
                }
                else if(object_parent == "UpperCorner"){
                    if(this.transform.position.x > 0){
                        Debug.Log("Corner kick in the upper RIGHT side of the field");
                        kickerObj.transform.position = new Vector3(this.transform.position.x + 0.25f, this.transform.position.y + 0.25f, 0f);
                    }
                    else{
                        Debug.Log("Corner kick in the upper LEFT side of the field");
                        kickerObj.transform.position = new Vector3(this.transform.position.x - 0.25f, this.transform.position.y + 0.25f, 0f);
                    }
                }
                
            }
            else{
                if(object_parent == "Lower"){
                    Debug.Log("Side kick in the lower field");
                    kickerObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.25f, 0f);
                }
                else if(object_parent == "LowerCorner"){
                    if(this.transform.position.x > 0){
                        Debug.Log("Corner kick in the lower RIGHT side of the field");
                        kickerObj.transform.position = new Vector3(this.transform.position.x + 0.25f, this.transform.position.y - 0.25f, 0f);
                    }
                    else{
                        Debug.Log("Corner kick in the lower LEFT side of the field");
                        kickerObj.transform.position = new Vector3(this.transform.position.x - 0.25f, this.transform.position.y - 0.25f, 0f);
                    }
                }
            }

            //ballObj.tag = "Untagged";

        }
        

        

        /*if((playerObj == null || !playerObj.GetComponent<Player>().selected) && ballObj.gameObject.tag == "BallTeam_2"){
            for(int i = 0; i < magrivaldos.Length; i++){
                if(magrivaldos[i].GetComponent<Player>().selected){
                    playerObj = magrivaldos[i];
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
            ballObj.tag = "Untagged";
            DestroyGameObject();
        }
        else if(this.kick && kickerObj == null){
            this.kick = false;
        }
    }

    private void DeactivateConstrains()
    {
        ballRigidbody.constraints = RigidbodyConstraints2D.None;

        if(ballObj.tag == "BallTeam_2"){
            Debug.Log("Activating playbility of Magrivaldos players (SideField.cs - DeactivateConstrains())");
            for(int i = 0; i < magrivaldos.Length; i++){
                //Debug.Log("Activating player " + magrivaldos[i].ToString());
                //magrivaldos[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                magrivaldos[i].GetComponent<Player>().selected = false;
                magrivaldos[i].GetComponent<Player>().playable = true;
            }
        }

        if(ballObj.tag == "BallTeam_1"){
            Debug.Log("Activating playbility of enemy players (SideField.cs - DeactivateConstrains())");
            for(int i = 0; i < enemies.Length; i++){
                //Debug.Log("Activating enemy "+ enemies[i].ToString());
                //enemies[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                enemies[i].GetComponent<Enemy>().selected = false;
                enemies[i].GetComponent<Enemy>().playable = true;
            }
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
        /*if(playerObj != null)
            playerObj.transform.position = cam.ScreenToWorldPoint(Input.mousePosition);*/
    }
}
