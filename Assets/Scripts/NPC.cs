using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float displayTime = 4;
    public GameObject dialogueBox;
    float displayTimer;
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
        displayTimer = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (displayTimer >= 0){
            displayTimer -= Time.deltaTime;
            if (displayTimer < 0){
                dialogueBox.SetActive(false);
            }
        }
    }
    public void DisplayDialogue(){
        displayTimer = displayTime;
        dialogueBox.SetActive(true);
    }
}
