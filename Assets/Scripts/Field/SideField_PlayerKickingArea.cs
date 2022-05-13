using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideField_PlayerKickingArea : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private GameObject playerObj;

    private GameObject[] players = null;

    private Rigidbody2D playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        if (players == null) {
            players = GameObject.FindGameObjectsWithTag("Magrivaldo");
        }

        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i].GetComponent<Player>().selected){
                Debug.Log("Localizei o Player");
                playerObj = players[i];
                break;
            }
            else{
                playerObj = null;
            }
        }
    }

    private /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        if(playerObj != null)
            playerObj.transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
