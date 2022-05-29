
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teams : MonoBehaviour
{
    private bool player_turn;

    public bool player_turn_UI;

    private bool movement_counted;

    private int moves_left;

    public int turns;

    private int moves = 8;

    private int Team1_moves;

    private int Team2_moves;

    private int team1Goals;

    private int team2Goals;

    private GameObject kicker;

    private GameObject ScriptHolder;

    private GameObject[] magrivaldos = null;

    private GameObject[] enemies = null;

    private GameObject fieldKick = null;

    private GameObject R_Goal = null;

    private GameObject L_Goal = null;
    
    private GameObject ballObj = null;

    private Rigidbody2D ballRB;

    public Teams(){
        this.player_turn = true;

        this.Team1_moves = moves;

        this.Team2_moves = moves;
    }

    public Teams(bool player_turn){
        this.player_turn = player_turn;

        this.Team1_moves = moves;

        this.Team2_moves = moves;
    }

    public bool getPlayerTurn(){
        return this.player_turn;
    }

    public int getMovesLeft(){
        return this.moves_left;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(ScriptHolder == null){
            ScriptHolder = GameObject.Find("ScriptHolder");
        }

        if (magrivaldos == null) {
            magrivaldos = GameObject.FindGameObjectsWithTag("Team_1");
        }

        if (enemies == null) {
            enemies = GameObject.FindGameObjectsWithTag("Team_2");
        }

        if(R_Goal == null){
            R_Goal = GameObject.Find("RightGoalArea");
        }

        if(L_Goal == null){
            L_Goal = GameObject.Find("LeftGoalArea");
        }

        if (ballObj == null) {
            ballObj = GameObject.Find("Ball");
            ballRB = ballObj.GetComponent<Rigidbody2D>();
        }

        if(player_turn){
            moves_left = Team1_moves;
            kicker = magrivaldos[0];
            ScriptHolder.GetComponent<ScriptHolder>().LookForInitialKickers(magrivaldos);
        }
        else{
            moves_left = Team2_moves;
            kicker = enemies[0];
            ScriptHolder.GetComponent<ScriptHolder>().LookForInitialKickers(enemies);
        }

        this.team1Goals = 0;

        this.team2Goals = 0;

        this.movement_counted = false;

        this.turns = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameEnded();

        if(player_turn_UI != player_turn){
            player_turn_UI = player_turn;
            turns++;
        }

        // Search for instances of Side Kicks or Corner Kicks
        fieldKick = GameObject.FindGameObjectWithTag("KickingArea");

        if(fieldKick == null){
            if(!magrivaldos[0].GetComponent<Player>().getPlayable() && player_turn){
                ChangeTeamsPlayability();
            }
            else if(!enemies[0].GetComponent<Enemy>().getPlayable() && !player_turn){
                ChangeTeamsPlayability();
            }
        }



        if(player_turn){
            if(!kicker.GetComponent<Player>().selected || !kicker.GetComponent<Player>().getMoved()){
                for(int i = 0; i < magrivaldos.Length; i++){
                    if(magrivaldos[i].GetComponent<Player>().getMoved()){
                        kicker = magrivaldos[i];
                    }
                }
            }
        }
        else{
            for(int i = 0; i < enemies.Length; i++){
                if(enemies[i].GetComponent<Enemy>().getMoved()){
                    kicker = enemies[i];
                }
            }
        }

        if(kicker != null){
            if(player_turn){
                if(kicker.GetComponent<Player>().moving && !movement_counted){
                    moves_left--;
                    Debug.Log("Moves left: " + moves_left);
                    movement_counted = true;
                }

                if(!kicker.GetComponent<Player>().moving && movement_counted){
                    Debug.Log("Deactivating movement counted");
                    movement_counted = false;
                }
            }
            else{
                if(kicker.GetComponent<Enemy>().moving && !movement_counted){
                    moves_left--;
                    movement_counted = true;
                    Debug.Log("Moves left: " + moves_left);
                }

                if(!kicker.GetComponent<Enemy>().moving && movement_counted){
                    movement_counted = false;
                }
            }
        }

        /*if(fieldKick != null && !sideKickChecked){
            sideKickChecked = true;
            player_turn = !player_turn;
            if(player_turn){
                moves_left = Team1_moves + 1;

                // Sets the first player of the Magrivaldos array as the kicker (prevent errors in the debug)
                kicker = magrivaldos[0];
            }
            else{
                moves_left = Team2_moves + 1;

                // Sets the first player of the enemy team array as the kicker (prevent errors in the debug)
                kicker = enemies[0];
            }
        }
        else if(fieldKick == null){
            sideKickChecked = false;
        }*/
        
        // Chenges turn if the team spend all of it's movements or pass the ball to the enemy team
        if(/*fieldKick == null &&*/ ballRB.velocity == new Vector2 (0f, 0f)){
            if(player_turn && (moves_left <= 0 /*|| ballObj.tag == "EnemyLostBall"*/)){
                if(moves_left <= 0){
                    Debug.Log("Player Turn Ended, because you went out of moves");
                }
                else{
                    Debug.Log("Player Turn Ended, because you passed the ball to the other team");
                }

                player_turn = false;
                moves_left = Team2_moves;

                ChangeTeamsPlayability();

                // Sets the first player of the enemy team array as the kicker (prevent errors in the debug)
                kicker = enemies[0];

                // Remove the tag from the ball
                /*Debug.Log("Removing the tag from the ball");
                if(ballRB.velocity == new Vector2 (0f, 0f)){
                    Debug.Log("Ball speed = " + ballRB.velocity);
                    Debug.Log("Ball speed was 0, untagging");
                }*/
                ballObj.tag = "Untagged";
            }
            else if(!player_turn && (moves_left <= 0 /*|| ballObj.tag == "MagrivaldosLostBall"*/)){
                if(moves_left <= 0){
                    Debug.Log("Enemy Turn Ended, because you went out of moves");
                }
                else{
                    Debug.Log("Enemy Turn Ended, because you passed the ball to the other team");
                }

                player_turn = true;
                moves_left = Team1_moves;

                ChangeTeamsPlayability();

                // Sets the first player of the Magrivaldos array as the kicker (prevent errors in the debug)
                kicker = magrivaldos[0];

                // Remove the tag from the ball
                /*Debug.Log("Removing the tag from the ball");
                if(ballRB.velocity == new Vector2 (0f, 0f)){
                    Debug.Log("Ball speed = " + ballRB.velocity);
                    Debug.Log("Ball speed was 0, untagging");
                }*/
                ballObj.tag = "Untagged";
            }
        }
    }

    public void ChangeTeamsPlayability(){
        Debug.Log("Player Turn: " + player_turn);

        if(player_turn){
            Debug.Log("Deactivating playbility of Enemy players and activating playbility of Magrivaldos players (Teams.cs - ChangeTeamsPlayability())");
            // Makes all the enemy team players unplayable
            for(int i = 0; i < magrivaldos.Length; i++){
                magrivaldos[i].GetComponent<Player>().selected = false;
                magrivaldos[i].GetComponent<Player>().playable = true;
            }

            for(int i = 0; i < enemies.Length; i++){
                enemies[i].GetComponent<Enemy>().selected = false;
                enemies[i].GetComponent<Enemy>().playable = false;
            }
        }
        else{
            Debug.Log("Deactivating playbility of Magrivaldos players and activating playbility of Enemy players (Teams.cs - ChangeTeamsPlayability())");
            // Makes all Magrivaldos players unplayable
            for(int i = 0; i < magrivaldos.Length; i++){
                magrivaldos[i].GetComponent<Player>().selected = false;
                magrivaldos[i].GetComponent<Player>().playable = false;
            }

            for(int i = 0; i < enemies.Length; i++){
                enemies[i].GetComponent<Enemy>().selected = false;
                enemies[i].GetComponent<Enemy>().playable = true;
            }
        }
    }

    public void SideKick(string ballTagKickedOutOfBonds){
        if(ballTagKickedOutOfBonds == "BallTeam_1" || ballTagKickedOutOfBonds == "EnemyLostBall"){
            player_turn = false;
        }
        else if(ballTagKickedOutOfBonds == "BallTeam_2" || ballTagKickedOutOfBonds == "MagrivaldosLostBall"){
            player_turn = true;
        }

        if(player_turn){
            moves_left = Team1_moves + 1;

            // Sets the first player of the Magrivaldos array as the kicker (prevent errors in the debug)
            kicker = magrivaldos[0];
        }
        else{
            moves_left = Team2_moves + 1;

            // Sets the first player of the enemy team array as the kicker (prevent errors in the debug)
            kicker = enemies[0];
        }
    }

    public void GoalKick(){
        Debug.Log("GoalKick GoalKick GoalKick");

        player_turn = !player_turn;

        if(player_turn){
            moves_left = Team1_moves + 1;

            ChangeTeamsPlayability();

            // Sets the first player of the Magrivaldos array as the kicker (prevent errors in the debug)
            kicker = magrivaldos[0];
        }
        else{
            moves_left = Team2_moves + 1;

            ChangeTeamsPlayability();

            // Sets the first player of the enemy team array as the kicker (prevent errors in the debug)
            kicker = enemies[0];
        }
    }

    public void GoalScored(){
        Debug.Log("Magrivaldos Goals (Goal Script): " + R_Goal.GetComponent<Goal>().getMagrivaldosScoredGoals());
        Debug.Log("Magrivaldos Goals (Teams Script): " + team1Goals);

        Debug.Log("Enemy Goals (Goal Script): " + L_Goal.GetComponent<Goal>().getEnemyScoredGoals());
        Debug.Log("Enemy Goals (Goal Script): " + team2Goals);

        if(R_Goal.GetComponent<Goal>().getMagrivaldosScoredGoals() != team1Goals){
            Debug.Log("Difference in Magrivaldos score, so Magrivaldos have just scored");
            player_turn = false;
            team1Goals = R_Goal.GetComponent<Goal>().getMagrivaldosScoredGoals();
        }
        else if(L_Goal.GetComponent<Goal>().getEnemyScoredGoals() != team2Goals){
            Debug.Log("Difference in Shrek 2 score, so Shrek 2 have just scored");
            player_turn = true;
            team2Goals = L_Goal.GetComponent<Goal>().getEnemyScoredGoals();
        }
        else{
            Debug.Log("Weird shit happened");
        }

        if(player_turn){
            moves_left = Team1_moves + 1;

            ChangeTeamsPlayability();

            // Sets the first player of the Magrivaldos array as the kicker (prevent errors in the debug)
            kicker = magrivaldos[0];
        }
        else{
            moves_left = Team2_moves + 1;

            ChangeTeamsPlayability();

            // Sets the first player of the enemy team array as the kicker (prevent errors in the debug)
            kicker = enemies[0];
        }
    }

    private void CheckGameEnded(){
        if(turns > 30){
            if(team1Goals > team2Goals){
                SceneManager.LoadScene(3);
            }

            if(team2Goals > team1Goals){
                SceneManager.LoadScene(4);
            }
        }
        else{
            if(team1Goals - team2Goals > 2){
                SceneManager.LoadScene(3);
            }

            if(team2Goals - team1Goals > 2){
                SceneManager.LoadScene(4);
            }
        }
    }
}
