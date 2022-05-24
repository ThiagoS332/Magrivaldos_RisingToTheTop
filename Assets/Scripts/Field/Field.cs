using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{

    private GameObject[] fieldKick = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fieldKick = GameObject.FindGameObjectsWithTag("KickingArea");

        if(fieldKick.Length > 1){
            Debug.Log("Found more than one instance of Side Kicks or Corner Kicks");
            for(int i = 1; i < fieldKick.Length; i++){
                Destroy(fieldKick[i]);
            }
        }
    }
}
