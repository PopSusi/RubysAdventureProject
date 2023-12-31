using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollect : MonoBehaviour
{
    public AudioClip collectClip;
    void OnTriggerEnter2D(Collider2D other){
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null && controller.health < controller.maxHealth){
            controller.HealthUpdate(1);
            Destroy(gameObject);
            controller.PlaySound(collectClip);
        }
    }
}
