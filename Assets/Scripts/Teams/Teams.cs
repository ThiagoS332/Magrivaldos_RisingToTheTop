
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teams : MonoBehaviour
{
    private bool player_turn;

    public bool player_turn_UI;

    private bool movement_counted;

    private bool sideKickChecked;

    private int moves_left;

    public int moves_left_UI;

    private int Team1_moves;

    private int Team2_moves;

    private GameObject kicker;

    private GameObject[] magrivaldos = null;

    private GameObject[] enemies = null;

    private GameObject fieldKick = null;
    
    private GameObject ballObj = null;

    private Rigidbody2D ballRB;

    public Teams(){
        this.player_turn = true;

        this.Team1_moves = 6;

        this.Team2_moves = 6;
    }

    public Teams(bool player_turn){
        this.player_turn = player_turn;

        this.Team1_moves = 6;

        this.Team2_moves = 6;
    }

    public Teams(int Team1_moves, int Team2_moves){
        this.player_turn = true;

        this.Team1_moves = Team1_moves;

        this.Team2_moves = Team2_moves;
    }

    public Teams(bool player_turn, int Team1_moves, int Team2_moves){
        this.player_turn = player_turn;

        this.Team1_moves = Team1_moves;

        this.Team2_moves = Team2_moves;
    }

    // Start is called before the first frame update
    void Start()
    {
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
        }
        else{
            moves_left = Team2_moves;
            kicker = enemies[0];
        }

        movement_counted = false;

        sideKickChecked = false;
    }

    // Update is called once per frame
    void Update()
    {
        player_turn_UI = player_turn;

        moves_left_UI = moves_left;



        if(player_turn){
            if(!kicker.GetComponent<Player>().selected || !kicker.GetComponent<Player>().moved){
                for(int i = 0; i < magrivaldos.Length; i++){
                    if(magrivaldos[i].GetComponent<Player>().selected){
                        kicker = magrivaldos[i];
                    }
                }
            }
            
        }
        else{
            for(int i = 0; i < enemies.Length; i++){
                if(enemies[i].GetComponent<Enemy>().selected){
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
                    Debug.Log("Movement counted = " + movement_counted);
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

        if(fieldKick != null && !sideKickChecked){
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
        }
        
        // Chenges turn if the team spend all of it's movements or pass the ball to the enemy team
        if(player_turn && (moves_left <= 0 || ballObj.gameObject.tag == "BallTeam_2") && fieldKick == null){
            if(moves_left <= 0){
                Debug.Log("Player Turn Ended, because you went out of moves");
            }
            else{
                Debug.Log("Player Turn Ended, because you passed the ball to the other team");
            }

            player_turn = false;
            moves_left = Team2_moves;

            Debug.Log("Deactivating playbility of Magrivaldos players (Teams.cs - Update())");
            // Makes all Magrivaldos players unplayable
            for(int i = 0; i < magrivaldos.Length; i++){
                magrivaldos[i].GetComponent<Player>().playable = false;
            }

            for(int i = 0; i < enemies.Length; i++){
                enemies[i].GetComponent<Enemy>().playable = true;
            }

            // Sets the first player of the enemy team array as the kicker (prevent errors in the debug)
            kicker = enemies[0];

            // Remove the tag from the ball
            ballObj.tag = "Untagged";
        }
        else if(!player_turn && (moves_left <= 0 || ballObj.gameObject.tag == "BallTeam_1") && fieldKick == null){
            if(moves_left <= 0){
                Debug.Log("Enemy Turn Ended, because you went out of moves");
            }
            else{
                Debug.Log("Enemy Turn Ended, because you passed the ball to the other team");
            }

            player_turn = true;
            moves_left = Team1_moves;
            
            Debug.Log("Deactivating playbility of enemy players (Teams.cs - Update())");
            // Makes all the enemy team players unplayable
            for(int i = 0; i < magrivaldos.Length; i++){
                magrivaldos[i].GetComponent<Player>().playable = true;
            }

            for(int i = 0; i < enemies.Length; i++){
                enemies[i].GetComponent<Enemy>().playable = false;
            }

            // Sets the first player of the Magrivaldos array as the kicker (prevent errors in the debug)
            kicker = magrivaldos[0];

            // Remove the tag from the ball
            ballObj.tag = "Untagged";
        }
    }
}
