//AUDIO MANAGER
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject spawnAudioBox; //Our Audio Box for Spawn
    private AudioClip currentClip; // Starting Music on initialization, will contain the playing audio later
    private AudioSource outputAudio;
    public static AudioManager instance; //To be called by other scripts

    private void Awake(){
        instance = this;
    }
    private void Start(){
        currentClip = spawnAudioBox.GetComponent<AudioBox>().myTrack;
        outputAudio = GetComponent<AudioSource>();
        outputAudio.clip = currentClip;
        outputAudio.Play();
    }
    public void SwapTracks(AudioClip newClip){
        outputAudio.Stop();
        Debug.Log("swapping to" + newClip + "from " + currentClip);
        outputAudio.clip = newClip;
        outputAudio.Play();
        
    }

    
}