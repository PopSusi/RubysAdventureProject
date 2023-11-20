using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jambi : MonoBehaviour, IInteractable
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
        displayTimer -= Time.deltaTime;
        if (displayTimer < 0){
            dialogueBox.SetActive(false);
        }
    }

    public void Interact()
    {
        displayTimer = displayTime;
        dialogueBox.SetActive(true);
    }
}
