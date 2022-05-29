using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideField_PlayerKickingArea : MonoBehaviour
{
    private bool kick = false;

    private string ballTag;

    private string object_parent;

    //private bool ball_kicked = false;

    [SerializeField]
    private Camera cam;

    private GameObject Teams = null;

    //private GameObject playerObj = null;

    private GameObject[] magrivaldos = null;

    //private GameObject enemyObj = null;

    private GameObject[] enemies = null;

    private GameObject kickerObj = null;

    private GameObject ballObj;

    private GameObject HelperUI;

    private Rigidbody2D ballRigidbody;

    private Vector3 ballPos;

    /*public SideField_PlayerKickingArea(float x, float y)
    {
        this.transform.position = new Vector3(x, y, 0f);
    }*/

    // Start is called before the first frame update
    void Start()
    {
        if(Teams == null){
            Teams = GameObject.Find("Teams");
        }

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
            this.ballTag = ballObj.tag;
            Debug.Log("Ball Tag: " + ballTag + " (SideField_PlayerKickingArea.cs - Start())");
        }

        if(cam == null){
            cam = GameObject.Find("GameCamera").GetComponent<Camera>();
        }

        if(HelperUI == null){
            HelperUI = GameObject.Find("Helper");
        }

        object_parent = this.transform.parent.name;

        Debug.Log("Deactivating playbility of Magrivaldos players (SideField_PlayerKickingArea.cs - Start())");
        // Deactivate the playbility of the Magrivaldos's players
        for(int i = 0; i < magrivaldos.Length; i++){
            magrivaldos[i].GetComponent<Player>().playable = false;
        }

        Debug.Log("Deactivating playbility of enemy players (SideField_PlayerKickingArea.cs - Start())");
        // Deactivate the playbility of the enemy's players
        for(int i = 0; i < enemies.Length; i++){
            enemies[i].GetComponent<Enemy>().playable = false;
        }

        Teams.GetComponent<Teams>().SideKick(ballTag);

        //Debug.Log("SideFieldKickingAreaPos = " + this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(kickerObj == null){
            //Debug.Log("Looking for kicker");
            if(ballTag == "BallTeam_1" || ballTag == "EnemyLostBall"){
                for(int i = 0; i < enemies.Length; i++){
                    if(enemies[i].GetComponent<Enemy>().selected){
                        kickerObj = enemies[i];
                        break;
                    }
                }
            }
            else if(ballTag == "BallTeam_2" || ballTag == "MagrivaldosLostBall"){
                SetUITextState(true);
                for(int i = 0; i < magrivaldos.Length; i++){
                    if(magrivaldos[i].GetComponent<Player>().selected){
                        kickerObj = magrivaldos[i];
                        break;
                    }
                }
            }
        }

        if(kickerObj != null){
            SetUITextState(false);

            this.kick = true;

            Debug.Log("Object parent: " + this.transform.parent.name);

            float kicking_area_posY = this.transform.position.y;

            float kicking_area_posX = this.transform.position.x;

            if(kicking_area_posY > 0){
                if(object_parent == "Upper"){
                    Debug.Log("Side kick in the upper field");
                    kickerObj.transform.position = new Vector3(kicking_area_posX, kicking_area_posY + 0.25f, 0f);
                }
                else if(object_parent == "UpperCorner"){
                    if(kicking_area_posX > 0){
                        Debug.Log("Corner kick in the upper RIGHT side of the field");
                        kickerObj.transform.position = new Vector3(kicking_area_posX + 0.25f, kicking_area_posY + 0.25f, 0f);
                    }
                    else{
                        Debug.Log("Corner kick in the upper LEFT side of the field");
                        kickerObj.transform.position = new Vector3(kicking_area_posX - 0.25f, kicking_area_posY + 0.25f, 0f);
                    }
                }
                
            }
            else{
                if(object_parent == "Lower"){
                    Debug.Log("Side kick in the lower field");
                    kickerObj.transform.position = new Vector3(kicking_area_posX, kicking_area_posY - 0.25f, 0f);
                }
                else if(object_parent == "LowerCorner"){
                    if(kicking_area_posX > 0){
                        Debug.Log("Corner kick in the lower RIGHT side of the field");
                        kickerObj.transform.position = new Vector3(kicking_area_posX + 0.25f, kicking_area_posY - 0.25f, 0f);
                    }
                    else{
                        Debug.Log("Corner kick in the lower LEFT side of the field");
                        kickerObj.transform.position = new Vector3(kicking_area_posX - 0.25f, kicking_area_posY - 0.25f, 0f);
                    }
                }
            }
        }
        


        if(this.kick){
            DeactivateConstrains();
            //ballTag = "Untagged";
            DestroyGameObject();
        }
        else if(this.kick && kickerObj == null){
            this.kick = false;
        }
    }

    private void SetUITextState(bool state){
        for(int i = 0; i < HelperUI.transform.childCount; i++){
            HelperUI.transform.GetChild(i).gameObject.SetActive(state);
        }

        GameObject HelperUIText = GameObject.Find("HelperText");

        if(HelperUIText != null){
            HelperUIText.GetComponent<HelperText>().WriteSelectPlayerText();
        }
    }

    private void DeactivateConstrains()
    {
        Debug.Log("Deactivate constraints");

        ballRigidbody.constraints = RigidbodyConstraints2D.None;

        Teams.GetComponent<Teams>().ChangeTeamsPlayability();

        /*if(ballTag == "BallTeam_2" || ballTag == "MagrivaldosLostBall"){
            Debug.Log("Activating playbility of Magrivaldos players (SideField_PlayerKickingArea.cs - DeactivateConstrains())");
            for(int i = 0; i < magrivaldos.Length; i++){
                //Debug.Log("Activating player " + magrivaldos[i].ToString());
                //magrivaldos[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                magrivaldos[i].GetComponent<Player>().selected = false;
                magrivaldos[i].GetComponent<Player>().playable = true;
            }
        }

        if(ballTag == "BallTeam_1" || ballTag == "EnemyLostBall"){
            Debug.Log("Activating playbility of enemy players (SideField_PlayerKickingArea.cs - DeactivateConstrains())");
            for(int i = 0; i < enemies.Length; i++){
                //Debug.Log("Activating enemy "+ enemies[i].ToString());
                //enemies[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                enemies[i].GetComponent<Enemy>().selected = false;
                enemies[i].GetComponent<Enemy>().playable = true;
            }
        }*/
    }

    private void DestroyGameObject()
    {
        Debug.Log("Destroying kicking area");
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
