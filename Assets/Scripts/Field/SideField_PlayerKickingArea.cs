using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideField_PlayerKickingArea : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private GameObject playerObj;

    private GameObject[] players = null;

    private GameObject ballObj;

    private Rigidbody2D ballRigidbody;

    public SideField_PlayerKickingArea(float x, float y)
    {
        this.transform.position = new Vector3(x, y, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (players == null) {
            players = GameObject.FindGameObjectsWithTag("Magrivaldo");
        }

        if(ballObj == null){
            ballObj = GameObject.Find("Ball");
            ballRigidbody = ballObj.GetComponent<Rigidbody2D>();
            ballRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }

        Debug.Log("SideFieldKickingAreaPos = " + this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerObj == null || !playerObj.GetComponent<Player>().selected){
            for(int i = 0; i < players.Length; i++)
            {
                if(players[i].GetComponent<Player>().selected){
                    Debug.Log("Localizei o Player");
                    playerObj = players[i];
                    if(this.transform.position.y > 0){
                        playerObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, 0f);
                    }
                    else{
                        playerObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.25f, 0f);
                    }
                    ballRigidbody.constraints = RigidbodyConstraints2D.None;
                    DestroyGameObject();
                    break;
                }
                else{
                    playerObj = null;
                }
            }
        }
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        Debug.Log("Cliquei na área de escanteio");
        if(playerObj != null)
            playerObj.transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
