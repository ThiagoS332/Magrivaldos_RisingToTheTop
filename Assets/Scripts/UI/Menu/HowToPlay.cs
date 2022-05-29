using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    private GameObject HowToPlayWindow;

    // Start is called before the first frame update
    void Start()
    {
        if(HowToPlayWindow == null){
            HowToPlayWindow = GameObject.Find("HowToPlayWindow");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallHowToPlayPopUp(){
        for(int i = 0; i < HowToPlayWindow.transform.childCount; i++){
            HowToPlayWindow.transform.GetChild(i).gameObject.SetActive(true);
        }

        /*if(!HowToPlayWindow.activeSelf){
            for(int i = 0; i < HowToPlayWindow.transform.childCount; i++){
                HowToPlayWindow.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else{
            for(int i = 0; i < HowToPlayWindow.transform.childCount; i++){
                HowToPlayWindow.transform.GetChild(i).gameObject.SetActive(false);
            }
        }*/
    }

    public void CloseHowToPlayPopUp(){
        for(int i = 0; i < HowToPlayWindow.transform.childCount; i++){
            HowToPlayWindow.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
