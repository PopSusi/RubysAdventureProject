using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OuterBound : MonoBehaviour
{
    public GameObject owner;
    private Vector3 homePosition;
    
    private AICat _ownerScript;
    // Start is called before the first frame update
    private void Awake()
    {
        _ownerScript = owner.GetComponent<AICat>();
        homePosition = this.transform.position;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            _ownerScript.LoseTarget(this.GameObject());
    }
}
