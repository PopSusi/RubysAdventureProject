using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICat : MonoBehaviour, IEnemy
{
    public GameObject outerBounds;
    public GameObject innerBounds;

    private GameObject _gtarget;
    public float speed;
    private float _distance;
    private NavMeshAgent _navMeshAgent;
    public bool targeting;
    private AudioSource _audioSource;
    public AudioClip hitClip;
    private Rigidbody2D _rb2d;

    private bool activeAI = true;
    // Start is called before the first frame update
    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targeting && activeAI)
        {
            _distance = Vector2.Distance(transform.position, _gtarget.transform.position);
            Vector2 direction = _gtarget.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.position = Vector2.MoveTowards(this.transform.position, _gtarget.transform.position,
                speed * Time.deltaTime);
        }
    }
    public void Target(GameObject ruby)
    {
        _gtarget = ruby;
        targeting = true;
    }
    public void LoseTarget(GameObject home)
    {
        _gtarget = home;
        targeting = false;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (activeAI)
        {
            RubyController player = other.gameObject.GetComponent<RubyController>();
            if (player != null) player.HealthUpdate(-1); //Hit Player
        }
    }
    public void HealthUpdate(int amnt){
        if (activeAI)
        {
            GameManager.instance.UpdateObjective();

            _rb2d.simulated = false;
            activeAI = false;
            _audioSource.PlayOneShot(hitClip);
            Debug.Log("deactivated");
        }
        Debug.Log("hit");
    }
}
