
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teams : MonoBehaviour
{
    private bool player_turn;

    private bool sideKickChecked;

    private int moves_left = 0;

    private int Team1_moves;

    private int Team2_moves;

    private GameObject[] players = null;

    private GameObject[] enemies = null;

    private GameObject fieldKick = null;
    
    private GameObject ballObj = null;

    private Rigidbody2D ballRB;

    private Vector2 vector_zero = new Vector2(0f, 0f);

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
        if (players == null) {
            players = GameObject.FindGameObjectsWithTag("Team_1");
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
        }
        else{
            moves_left = Team2_moves;
        }

        sideKickChecked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ballObj.GetComponent<Ball>().moving){
            Debug.Log("OH SHIT THE BALL IS MOVING!!! WTF?!");
            moves_left--;
            Debug.Log("Moves left: " + moves_left);
        }

        fieldKick = GameObject.FindGameObjectWithTag("KickingArea");

        if(fieldKick != null && !sideKickChecked){
            sideKickChecked = true;
            player_turn = !player_turn;
            if(player_turn){
                moves_left = Team1_moves + 1;
            }
            else{
                moves_left = Team2_moves + 1;
            }
        }
        else if(fieldKick == null){
            sideKickChecked = false;
        }

        if(((moves_left <= 0 && player_turn) || ballObj.gameObject.tag == "BallTeam_2") && fieldKick == null){
            player_turn = false;
            moves_left = Team2_moves;

            for(int i = 0; i < players.Length; i++){
                players[i].GetComponent<Player>().playable = false;
            }

            for(int i = 0; i < enemies.Length; i++){
                enemies[i].GetComponent<Enemy>().playable = true;
            }
        }
        else if(((moves_left <= 0 && !player_turn) || ballObj.gameObject.tag == "BallTeam_1") && fieldKick == null){
            player_turn = true;
            moves_left = Team1_moves;

            for(int i = 0; i < players.Length; i++){
                players[i].GetComponent<Player>().playable = true;
            }

            for(int i = 0; i < enemies.Length; i++){
                enemies[i].GetComponent<Enemy>().playable = false;
            }
        }
    }
}
