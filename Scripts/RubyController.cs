using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public GameObject projectilePrefab;

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
    bool isBuffer = false;
    float bufferTimer;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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
        Vector2 move = new Vector2(horDir, verDir);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        if(isBuffer){
            bufferTimer -= Time.deltaTime;
            if(bufferTimer < 0)
                isBuffer = false;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
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
            animator.SetTrigger("Hit");
        }
        curHealth = Mathf.Clamp(curHealth + amnt, 0, maxHealth);
    }
    
    void Launch(){
        GameObject projectileObject = Instantiate(projectilePrefab,rb2d.position + Vector2.up * .5f, Quaternion.identity);
        
        CogBullet projectile = projectileObject.GetComponent<CogBullet>();
        projectile.Launch(lookDirection, 300);
        Debug.Log("Dir " + lookDirection);
        animator.SetTrigger("Launch");
    }
}
