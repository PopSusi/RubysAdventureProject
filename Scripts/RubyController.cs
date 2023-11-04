using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // Start is called before the first frame update
    public float walkSpeed;
    public float speed;
    public float runSpeed;
    float horDir;
    float verDir;
    Rigidbody2D rb2d;

    public int maxHealth;
    public int health {get{return curHealth;}}
    int curHealth;
    
    public float bufferTime = 2.0f;
    bool isBuffer;
    float bufferTimer;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        speed = walkSpeed;
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horDir = Input.GetAxis("Horizontal");
        verDir = Input.GetAxis("Vertical");

        if(isBuffer){
            bufferTimer -= Time.deltaTime;
            if(bufferTimer < 0)
                isBuffer = false;
        }
    }
    void FixedUpdate(){
        Vector2 position = rb2d.position;
        position.x = position.x + speed * horDir * Time.deltaTime;
        position.y = position.y + speed * verDir * Time.deltaTime;
        rb2d.MovePosition(position);
    }
    public void ChangeHealth(int amnt){
        if(amnt < 0){
            if (isBuffer){
                return;
            }
            curHealth += amnt;
            isBuffer = true;
            bufferTimer = bufferTime;
        }
        curHealth = Mathf.Clamp(curHealth + amnt, 0, maxHealth);
    }
}
