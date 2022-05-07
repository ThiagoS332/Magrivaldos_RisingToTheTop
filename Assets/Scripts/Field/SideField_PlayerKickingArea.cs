using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideField_PlayerKickingArea : MonoBehaviour
{
    private GameObject playerObj = null;

    private Rigidbody2D playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        if (playerObj == null) {
            playerObj = GameObject.Find("Player");
        }

        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        playerObj.transform.position = Input.mousePosition;
    }
}
