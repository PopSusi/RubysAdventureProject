using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InnerBounds : MonoBehaviour
{
    public GameObject owner;
    private AICat _ownerScript; 

    private void Awake()
    {
        _ownerScript = owner.GetComponent<AICat>();
    }

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            _ownerScript.Target(other.GameObject());
            Debug.Log(other.GameObject().name + "Entered");
    } 
}
