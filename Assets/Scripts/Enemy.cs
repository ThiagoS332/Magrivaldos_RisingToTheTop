using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D enemyRigidbody;

    public bool selected;

    private float maxPullDist;

    private float minPullDist;

    private GameObject ballObj = null;

    //private Rigidbody2D ballRB;

    private GameObject[] goalObj = null;

    public Enemy(){
        this.maxPullDist = 400.0f;

        this.minPullDist = -400.0f;

        this.selected = false;
    }

    public Enemy(float maxPullDist, float minPullDist){
        this.maxPullDist = maxPullDist;

        this.minPullDist = minPullDist;

        this.selected = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D> ();

        if (ballObj == null) {
            ballObj = GameObject.Find("Ball");
            //ballRB = ballObj.GetComponent<Rigidbody2D>();
        }

        if (goalObj == null) {
            goalObj = GameObject.FindGameObjectsWithTag("LeftGoal");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(selected){
            MoveEnemy();
        }
    }

    private void OnMouseDown()
    {
        selected = true;
    }

    private void MoveEnemy()
    {
        Vector3 ballPos = ballObj.transform.position;
    
        Vector3 leftGoalPos = goalObj[0].transform.position;

        Vector3 enemyPos = this.transform.position;



        Debug.Log("BallPos = " + ballPos);

        Debug.Log("GoalPos = " + leftGoalPos);

        Debug.Log("EnemyPos = " + enemyPos);



        Vector3 enemy_to_ball = ballPos - enemyPos;

        Vector3 ball_to_goal = leftGoalPos - ballPos;

        Vector3 enemy_to_goal = leftGoalPos - enemyPos;



        Debug.Log("EnemyToBall = " + enemy_to_ball);

        Debug.Log("BallToGoal = " + ball_to_goal);

        Debug.Log("EnemyToGoal = " + enemy_to_goal);



        float enemyToBall_magnitude = enemy_to_ball.magnitude;

        float ballToGoal_magnitude = ball_to_goal.magnitude;

        float enemyToGoal_magnitude = enemy_to_goal.magnitude;



        Debug.Log("EnemyToBall Magnitude = " + enemyToBall_magnitude);

        Debug.Log("BallToGoal Magnitude = " + ballToGoal_magnitude);

        Debug.Log("EnemyToGoal Magnitude = " + enemyToGoal_magnitude);



        if(enemyToBall_magnitude < 1){
            enemyToBall_magnitude = 1;
        }

        if(ballToGoal_magnitude < 1){
            ballToGoal_magnitude = 1;
        }

        if(enemyToGoal_magnitude < 1){
            enemyToGoal_magnitude = 1;
        }



        Vector2 forceApplied;

        // for X
        if(enemy_to_ball.x * (enemyToGoal_magnitude * ballToGoal_magnitude) > maxPullDist){
            forceApplied.x = maxPullDist;
        }
        else if(enemy_to_ball.x * (enemyToGoal_magnitude * ballToGoal_magnitude) < minPullDist){
            forceApplied.x = minPullDist;
        }
        else{
            forceApplied.x = enemy_to_ball.x * (enemyToGoal_magnitude * ballToGoal_magnitude);
        }

        // for Y
        if(enemy_to_ball.y * (enemyToGoal_magnitude * ballToGoal_magnitude) > maxPullDist){
            forceApplied.y = maxPullDist;
        }
        else if(enemy_to_ball.y * (enemyToGoal_magnitude * ballToGoal_magnitude) < minPullDist){
            forceApplied.y = minPullDist;
        }
        else{
            forceApplied.y = enemy_to_ball.y * (enemyToGoal_magnitude * ballToGoal_magnitude);
        }

        Debug.Log("ForceGenerated = " + forceApplied);

        this.GetComponent<Rigidbody2D>().AddForce (new Vector2(forceApplied.x, forceApplied.y));

        selected = false;
    }
}
