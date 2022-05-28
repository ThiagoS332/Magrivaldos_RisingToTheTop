using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheAndHammer : MonoBehaviour
{
    public AudioSource URSSHymn;

    public AudioSource menuSong;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!URSSHymn.isPlaying && !menuSong.isPlaying){
            menuSong.Play();
        }
    }

    public void PlaySong(){
        menuSong.Stop();
        URSSHymn.Play();
    }
}
