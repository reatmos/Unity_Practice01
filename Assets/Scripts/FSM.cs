using UnityEngine;

public class FSM : MonoBehaviour {
    public enum EnemyState {
        Idle,
        Patrol,
        Chase,
        Attack
    }

    public EnemyState currentState;
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float attackRange = 1f;
    public float chaseRange = 5f;

    private int currentPatrolPoint = 0;
    private Transform player;

    void Start() {
        currentState = EnemyState.Idle;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        switch (currentState) {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }
    }

    void Idle() {
        if (patrolPoints.Length > 0) {
            currentState = EnemyState.Patrol;
        }
    }

    void Patrol() {
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < 0.1f) {
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPatrolPoint].position, patrolSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, player.position) < chaseRange) {
            currentState = EnemyState.Chase;
        }
    }

    void Chase() {
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, player.position) < attackRange) {
            currentState = EnemyState.Attack;
        } else if (Vector3.Distance(transform.position, player.position) > chaseRange) {
            currentState = EnemyState.Patrol;
        }
    }

    void Attack() {
        if (Vector3.Distance(transform.position, player.position) > attackRange) {
            currentState = EnemyState.Chase;
        }
    }
}
