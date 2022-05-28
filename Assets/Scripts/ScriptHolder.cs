using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptHolder : MonoBehaviour
{
    private GameObject[] magrivaldos = null;

    private GameObject[] enemies = null;

    private GameObject ballObj = null;

    private Rigidbody2D ballRB;

    //[SerializeField]
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        if (ballObj == null) {
            ballObj = GameObject.Find("Ball");
            ballRB = ballObj.GetComponent<Rigidbody2D>();
        }

        if (magrivaldos == null) {
            magrivaldos = GameObject.FindGameObjectsWithTag("Team_1");
        }

        if (enemies == null) {
            enemies = GameObject.FindGameObjectsWithTag("Team_2");
        }

        if(cam == null){
            cam = GameObject.Find("GameCamera").GetComponent<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetBallPos(){
        ballObj.transform.position = new Vector3(0f, 0f, 0f);

        ballRB.velocity = new Vector2(0f, 0f);
        ballRB.angularVelocity = 0f;
    }

    public void SetBallPos(Vector2 newBallPos){
        ballObj.transform.position = newBallPos;

        ballRB.velocity = new Vector2(0f, 0f);
        ballRB.angularVelocity = 0f;
    }

    /*public void SetTeamAttackFormation(string TeamTag){
        Vector2[] playersPos = [];

        if(TeamTag == "Team_1"){

        }
        else{

        }

    }*/

    public void LookForInitialKickers(GameObject[] team){
        for(int i = 0; i < team.Length; i++){
            if(team[i].ToString().Contains("_6")){
                if(team[i].transform.position.x < 0){
                    team[i].transform.position = new Vector2 (-0.25f, 0f);
                }
                else{
                    team[i].transform.position = new Vector2 (0.25f, 0f);
                }
                
            }
            else if(team[i].ToString().Contains("_7")){
                if(team[i].transform.position.x < 0){
                    team[i].transform.position = new Vector2 (0.25f, -0.25f);
                }
                else{
                    team[i].transform.position = new Vector2 (-0.25f, -0.25f);
                }
            }
        }
    }

    public void ResetPlayersPos(){
        int i;

        for(i = 0; i < magrivaldos.Length; i++){
            magrivaldos[i].transform.position = magrivaldos[i].GetComponent<Player>().getOriginalPos();
            magrivaldos[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            magrivaldos[i].GetComponent<Rigidbody2D>().angularVelocity = 0f;
        }

        for(i = 0; i < enemies.Length; i++){
            enemies[i].transform.position = enemies[i].GetComponent<Enemy>().getOriginalPos();
            enemies[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            enemies[i].GetComponent<Rigidbody2D>().angularVelocity = 0f;
        }
    }

    public void ResetCamPos(){
        cam.transform.position = cam.GetComponent<CameraMovement>().getOriginalPos();
    }

    public void SetCamPos(Vector3 newCamPos){
        cam.transform.position = newCamPos;
    }
}
