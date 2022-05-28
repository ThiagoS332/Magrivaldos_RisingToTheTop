using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int magrivaldosScoredGoals = 0;

    public int enemyScoredGoals = 0;

    private GameObject ScriptHolder;

    private GameObject Teams;

    private GameObject[] magrivaldos = null;

    private GameObject[] enemies = null;

    private GameObject ballObj = null;

    private Rigidbody2D ballRB;

    //[SerializeField]
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        if(Teams == null){
            Teams = GameObject.Find("Teams");
        }

        if(ScriptHolder == null){
            ScriptHolder = GameObject.Find("ScriptHolder");
        }

        if (magrivaldos == null) {
            magrivaldos = GameObject.FindGameObjectsWithTag("Team_1");
        }

        if (enemies == null) {
            enemies = GameObject.FindGameObjectsWithTag("Team_2");
        }

        if (ballObj == null) {
            ballObj = GameObject.Find("Ball");
            ballRB = ballObj.GetComponent<Rigidbody2D>();
        }


        if(cam == null){
            cam = GameObject.Find("GameCamera").GetComponent<Camera>();
        }
        
        magrivaldosScoredGoals = 0;

        enemyScoredGoals = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void PlayRefereeWhistle(){
        refereeWhistle.Play();
    }*/

    /*private void ResetBall(){
        ballObj.transform.position = new Vector3(0f, 0f, 0f);

        ballRB.velocity = new Vector2(0f, 0f);
        ballRB.angularVelocity = 0f;
    }

    private void ResetPlayers(){
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

    private void ResetCam(){
        cam.transform.position = cam.GetComponent<CameraMovement>().getOriginalPos();
    }*/

    private void OnTriggerStay2D(Collider2D other)
    {
        Vector2 ballPosEntrance = ballObj.transform.position;

        if(other.gameObject.name == "Ball"){
            ScriptHolder.GetComponent<ScriptHolder>().ResetPlayersPos();

            if(ballPosEntrance.x > 0){
                magrivaldosScoredGoals++;
                Debug.Log("Magrivaldos Goals: " + magrivaldosScoredGoals);
                ScriptHolder.GetComponent<ScriptHolder>().LookForInitialKickers(enemies);
            }
            else{
                enemyScoredGoals++;
                Debug.Log("Enemy Goals: " + enemyScoredGoals);
                ScriptHolder.GetComponent<ScriptHolder>().LookForInitialKickers(magrivaldos);
            }
            
            //PlayRefereeWhistle();

            ScriptHolder.GetComponent<ScriptHolder>().ResetBallPos();
            ScriptHolder.GetComponent<ScriptHolder>().ResetCamPos();
            Teams.GetComponent<Teams>().GoalScored();
            
        }
    }
}
