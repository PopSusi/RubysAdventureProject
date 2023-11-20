using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable<int>
{
    public static float speed {get;} = 1;
    [SerializeField] bool vertical;
    [SerializeField] float changeTime = 3.0f;

    bool broken = true;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    Animator animator;
    public ParticleSystem smokeEffect;

    public AudioClip[] hitClips;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        timer = changeTime;
    }

    void Update()
    {
        if(!broken){
            return;
        }
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        if(vertical){
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", direction);
        } else {
            animator.SetFloat("Horizontal", direction);
            animator.SetFloat("Vertical", 0);
        }
    }
    
    void FixedUpdate()
    {
        if (!broken) return;
        Vector2 position = rigidbody2D.position;
        
        if (vertical) {
            position.y = position.y + Time.deltaTime * speed * direction;
        } else {
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();
        if (player != null) player.HealthUpdate(-1); //Hit Player
    }

    public void HealthUpdate(int amnt){
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        audioSource.PlayOneShot(hitClips[Random.Range(0, hitClips.Length)]);
    }
}
