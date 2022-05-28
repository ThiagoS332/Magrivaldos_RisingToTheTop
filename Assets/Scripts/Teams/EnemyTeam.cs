using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeam : MonoBehaviour
{
    private GameObject closestEnemy;

    private GameObject[] enemies = null;

    private GameObject Teams = null;

    private GameObject ballObj = null;

    private Rigidbody2D ballRB;

    // Start is called before the first frame update
    void Start()
    {
        if (enemies == null) {
            enemies = GameObject.FindGameObjectsWithTag("Team_2");
            closestEnemy = enemies[0];
        }

        if(Teams == null){
            Teams = GameObject.Find("Teams");
        }

        if (ballObj == null) {
            ballObj = GameObject.Find("Ball");
            ballRB = ballObj.GetComponent<Rigidbody2D>();
        }
    }

    private float CalculateVectorDist(Vector2 vector1, Vector2 vector2){
        return Mathf.Sqrt(Mathf.Pow((vector1.x - vector2.x), 2) + (Mathf.Pow((vector1.y - vector2.y), 2)));
    }

    private bool CheckDistance(Vector2 closestEnemyPos, Vector2 enemyPos, Vector2 ballPos){
        float closestDist = CalculateVectorDist(closestEnemyPos, ballPos);

        float newDist = CalculateVectorDist(enemyPos, ballPos);

        if(closestDist < newDist){
            return true;
        }

        return false;
    }

    private IEnumerator WaitToTakeDecisions(){
        for(int i = 0; i < enemies.Length; i++){
            if(CheckDistance(closestEnemy.transform.position, enemies[i].transform.position, ballObj.transform.position)){
                closestEnemy = enemies[i];
                Debug.Log("Found closest enemy");
            }
        }

        Debug.Log("Wait");

        yield return new WaitForSeconds(5);

        Debug.Log("Waited");

        if(!closestEnemy.GetComponent<Enemy>().getMoving()){
            Debug.Log("Moving enemy");
            closestEnemy.GetComponent<Enemy>().setSelected(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(!Teams.GetComponent<Teams>().getPlayerTurn() && !ballObj.GetComponent<Ball>().getMoving()){
            StartCoroutine(WaitToTakeDecisions());
        }*/
    }
}
