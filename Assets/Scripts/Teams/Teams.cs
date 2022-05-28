
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teams : MonoBehaviour
{
    private bool player_turn;

    public bool player_turn_UI;

    private bool movement_counted;

    //private bool sideKickChecked;

    private int moves_left;

    //public int moves_left_UI;

    public int turns;

    private int moves = 8;

    private int Team1_moves;

    private int Team2_moves;

    private GameObject kicker;

    private GameObject ScriptHolder;

    private GameObject[] magrivaldos = null;

    private GameObject[] enemies = null;

    private GameObject fieldKick = null;
    
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

        movement_counted = false;

        //sideKickChecked = false;

        turns = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(player_turn_UI != player_turn){
            player_turn_UI = player_turn;
            turns++;
        }

        //moves_left_UI = moves_left;



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

        // Search for instances of Side Kicks or Corner Kicks
        fieldKick = GameObject.FindGameObjectWithTag("KickingArea");

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

    private void ChangeTeamsPlayability(){
        Debug.Log("Player Turn: " + player_turn);

        if(player_turn){
            Debug.Log("Deactivating playbility of Enemy players and activating playbility of Magrivaldos players (Teams.cs - ChangeTeamsPlayability())");
            // Makes all the enemy team players unplayable
            for(int i = 0; i < magrivaldos.Length; i++){
                magrivaldos[i].GetComponent<Player>().playable = true;
            }

            for(int i = 0; i < enemies.Length; i++){
                enemies[i].GetComponent<Enemy>().playable = false;
            }
        }
        else{
            Debug.Log("Deactivating playbility of Magrivaldos players and activating playbility of Enemy players (Teams.cs - ChangeTeamsPlayability())");
            // Makes all Magrivaldos players unplayable
            for(int i = 0; i < magrivaldos.Length; i++){
                magrivaldos[i].GetComponent<Player>().playable = false;
            }

            for(int i = 0; i < enemies.Length; i++){
                enemies[i].GetComponent<Enemy>().playable = true;
            }
        }
    }

    public void SideKick(){
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

    public void GoalScored(){
        if(moves_left > 0){
            player_turn = !player_turn;
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
}
