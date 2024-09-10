using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform player;
    [SerializeField] private Transform patrolRoute;
    [SerializeField] private List<Transform> locations;
    [SerializeField] private int locationIndex = 0;
    [SerializeField] private float stoppingDistance = 0.2f;
    private UnityEngine.AI.NavMeshAgent agent;
    private int _lives = 5;
    public int EnemyLives
    {
        get { return _lives; }
        private set
        {
            _lives = value;
            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                
            }
        }
    }

    private GameManager2 _gameBehavior;

    // Chase state related fields
    private bool _canDetectPlayer;

    // Attack state related fields
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attacksPerSecond = 1f;
    private float attackRate => 1 / attacksPerSecond;
    private float _attackCooldown;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("Critical hit!");
        }
    }

    void Start()
    {
        _gameBehavior = FindFirstObjectByType<GameManager2>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        InitializePatrolRoute();

        MoveToNextPatrolLocation();
    }

    void Update()
    {
        _attackCooldown -= Time.deltaTime;

        if (CanDetectPlayer())
        {
            if (CanAttackPlayer())
            {
                AttackState();
            }
            else
            {
                ChaseState();
            }
        }
        else
        {
            PatrolState();
        }
    }

    void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }


    private void PatrolState()
    {
        if (agent.remainingDistance < stoppingDistance && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    private void ChaseState()
    {
        agent.SetDestination(player.position);
    }

    private void AttackState()
    {
        agent.SetDestination(player.position);
        Attack();
    }

    private void Attack()
    {
        if (_attackCooldown <= 0)
        {
            _attackCooldown = attackRate;
            _gameBehavior.HP -= attackDamage;
            Debug.Log("Attacked player");
        }
    }

    private bool CanDetectPlayer()
    {
        return _canDetectPlayer;
    }

    private bool CanAttackPlayer()
    {
        var directionToPlayer = player.position - transform.position;
        var distanceToPlayer = directionToPlayer.magnitude;
        return distanceToPlayer <= attackRange;
    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canDetectPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canDetectPlayer = false;
        }
    }
}
