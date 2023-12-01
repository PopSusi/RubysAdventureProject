using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour, IDamageable<int>, IKillable
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
    public ParticleSystem HealthHeart;
    public ParticleSystem Damage;
    public bool isBuffer = false;
    float bufferTimer;

    Animator animator;
    public AudioSource audioSource;
    public AudioClip hitClip;
    public AudioClip throwClip;
    public AudioSource footSource;
    Vector2 lookDirection = new Vector2(1,0);
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

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
        if(move.magnitude > .05){
            if(!footSource.isPlaying){
                footSource.Play();
            }
        } else{
            footSource.Stop();
        }
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
            PlaySound(throwClip);
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb2d.position + Vector2.up * .2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null){
                var objInt = hit.collider.GetComponent<IInteractable>();
                if (objInt != null){
                    objInt.Interact();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && (GameManager.instance.lostOpen || GameManager.instance.levelWin))
        {
            SceneManager.LoadScene("MainScene");
            Time.timeScale = 1;
        }
        
        if (Input.GetButtonDown("Cancel"))
        {
            if (!GameManager.instance.pauseOpen)
            {
                GameManager.instance.Pause();
            } else if (GameManager.instance.pauseOpen)
            {
                GameManager.instance.Continue();
            }
        }
        
    }
    void FixedUpdate(){
        Vector2 position = rb2d.position;
        position.x = position.x + speed * horDir * Time.deltaTime;
        position.y = position.y + speed * verDir * Time.deltaTime;
        rb2d.MovePosition(position);
    }
    public void HealthUpdate(int amnt){
        if(amnt < 0){
            if (isBuffer) return;
            
            isBuffer = true;
            bufferTimer = bufferTime;
            animator.SetTrigger("Hit");
            Instantiate(Damage, rb2d.position + Vector2.up * 0.5f, Quaternion.identity);
            PlaySound(hitClip);
            Damage.Play();
        }
        else if (amnt > 0)
        {
            Instantiate(HealthHeart, rb2d.position + Vector2.up * 0.5f, Quaternion.identity);
            HealthHeart.Play();
        } 
        curHealth = Mathf.Clamp(curHealth + amnt, 0, maxHealth);
        UIHealthBar.instance.SetValue(curHealth / (float) maxHealth);
        if (health <= 0) Dies(); //Kills if under 5
    }

    public void Dies()
    {
        GameManager.instance.Lost();
    }
    
    void Launch(){
        GameObject projectileObject = Instantiate(projectilePrefab,rb2d.position + Vector2.up * .5f, Quaternion.identity);
        
        CogBullet projectile = projectileObject.GetComponent<CogBullet>();
        projectile.Launch(lookDirection, 300);
        animator.SetTrigger("Launch");
    }
    public void PlaySound(AudioClip clip){
        audioSource.PlayOneShot(clip);
    }
    
}
