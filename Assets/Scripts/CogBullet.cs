using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogBullet : MonoBehaviour
{
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 1000f) Destroy(gameObject); //Destroy if too far
    }

    public void Launch(Vector2 direction, float force){
        rb2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other){
        EnemyController enemy = other.collider.GetComponent<EnemyController>();
        if(enemy != null) enemy.HealthUpdate(-1); //Damage if Enemy
        Destroy(gameObject);
    }
}
