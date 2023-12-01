using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class AICat : MonoBehaviour
{
    public GameObject outerBounds;
    public GameObject innerBounds;

    private GameObject _gtarget;
    public float speed;
    private float _distance;
    private NavMeshAgent _navMeshAgent;
    public bool targeting;
    // Start is called before the first frame update
    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targeting)
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
        RubyController player = other.gameObject.GetComponent<RubyController>();
        if (player != null) player.HealthUpdate(-1); //Hit Player
    }
}
