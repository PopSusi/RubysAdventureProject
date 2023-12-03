// AUDIO
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBox : MonoBehaviour
{
    public AudioClip myTrack;

    public void OnTriggerEnter2D(Collider2D other){ //When Colliding
        if(other.CompareTag("Player")){         //Check for Player
            AudioManager.instance.SwapTracks(myTrack); //Tell the audiomanager to swap tracks with this AudioBox's track
        }
        Debug.Log("TAGGED");
    }

    
}