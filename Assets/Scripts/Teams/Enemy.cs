using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool moving;

    private bool moved;

    public bool selected;

    public bool playable;

    private float maxPullDist;

    private float minPullDist;

    private Vector2 originalPos;

    private Rigidbody2D enemyRigidbody;

    private GameObject ballObj = null;

    private GameObject[] goalObj = null;

    private GameObject sideKickObj = null;

    public AudioSource grunt;

    public Enemy(){
        this.maxPullDist = 400.0f;

        this.minPullDist = -400.0f;

        this.playable = true;

        this.selected = false;
    }

    public Enemy(float maxPullDist, float minPullDist){
        this.maxPullDist = maxPullDist;

        this.minPullDist = minPullDist;

        this.playable = true;

        this.selected = false;
    }

    public void setSelected(bool selected){
        this.selected = selected;
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

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = this.GetComponent<Rigidbody2D>();

        if (ballObj == null) {
            ballObj = GameObject.Find("Ball");
            //ballRB = ballObj.GetComponent<Rigidbody2D>();
        }

        if (goalObj == null) {
            goalObj = GameObject.FindGameObjectsWithTag("LeftGoal");
        }

        this.originalPos = this.transform.position;
    }

    public bool getMoved(){
        return this.moved;
    }

    // Update is called once per frame
    void Update()
    {
        sideKickObj = GameObject.FindWithTag("KickingArea");

        // i know this makes no f***ing sens, but if i remove this, the code breaks. So leave it alone.
        if(this.selected && this.playable && sideKickObj != null){
            this.selected = true;
        }

        /*if(this.selected && sideKickObj == null && this.playable){
            MoveEnemy();
        }*/

        if(this.selected && this.playable && !this.moving){
            MoveEnemy();
        }

        StartCoroutine (CheckSpeed());
    }

    public void OnMouseDown()
    {
        selected = !selected;
    }

    private IEnumerator CheckSpeed(){
        yield return new WaitForSeconds(1);
        if(this.GetComponent<Rigidbody2D>().velocity == new Vector2(0f, 0f)){
            this.moving = false;
            this.moved = false;
        }
        else{
            this.moving = true;
            this.moved = true;
        }
    }

    private void MoveEnemy()
    {
        Vector3 ballPos = ballObj.transform.position;
    
        Vector3 leftGoalPos = goalObj[0].transform.position;

        Vector3 enemyPos = this.transform.position;



        /*Debug.Log("BallPos = " + ballPos);

        Debug.Log("GoalPos = " + leftGoalPos);

        Debug.Log("EnemyPos = " + enemyPos);*/



        Vector3 enemy_to_ball = ballPos - enemyPos;

        Vector3 ball_to_goal = leftGoalPos - ballPos;

        Vector3 enemy_to_goal = leftGoalPos - enemyPos;



        /*Debug.Log("EnemyToBall = " + enemy_to_ball);

        Debug.Log("BallToGoal = " + ball_to_goal);

        Debug.Log("EnemyToGoal = " + enemy_to_goal);*/



        float enemyToBall_magnitude = enemy_to_ball.magnitude;

        float ballToGoal_magnitude = ball_to_goal.magnitude;

        float enemyToGoal_magnitude = enemy_to_goal.magnitude;



        /*Debug.Log("EnemyToBall Magnitude = " + enemyToBall_magnitude);

        Debug.Log("BallToGoal Magnitude = " + ballToGoal_magnitude);

        Debug.Log("EnemyToGoal Magnitude = " + enemyToGoal_magnitude);*/



        if(enemyToBall_magnitude < 1){
            enemyToBall_magnitude = 1;
        }

        if(ballToGoal_magnitude < 1){
            ballToGoal_magnitude = 1;
        }

        if(enemyToGoal_magnitude < 1){
            enemyToGoal_magnitude = 1;
        }



        Vector3 forceApplied;

        // for X
        if(25 * ballToGoal_magnitude * enemy_to_ball.x > maxPullDist){
            forceApplied.x = maxPullDist;
        }
        else if(25 * ballToGoal_magnitude * enemy_to_ball.x < minPullDist){
            forceApplied.x = minPullDist;
        }
        else{
            forceApplied.x = 25 * ballToGoal_magnitude * enemy_to_ball.x;
        }

        // for Y
        if(25 * ballToGoal_magnitude * enemy_to_ball.y > maxPullDist){
            forceApplied.y = maxPullDist;
        }
        else if(25 * ballToGoal_magnitude * enemy_to_ball.y < minPullDist){
            forceApplied.y = minPullDist;
        }
        else{
            forceApplied.y = 25 * ballToGoal_magnitude * enemy_to_ball.y;
        }

        forceApplied.z = 0f;

        Debug.Log("ForceApplied = " + forceApplied);

        this.GetComponent<Rigidbody2D>().AddForce (new Vector3 (forceApplied.x, forceApplied.y));

        moved = true;

        selected = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Team_1" && !this.playable)
        {
            Debug.Log("Oh, it hurts");
            grunt.Play();
        }
    }
}
