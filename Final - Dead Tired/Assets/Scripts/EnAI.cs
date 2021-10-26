using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnAI : MonoBehaviour
{
    [Header("Navigation")]
    //navigation, recognition
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask Ground, Player;

    //Patrolling
    public Vector3 walkPoint;
    bool WalkPointSet;
    public float walkRange;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    [Header("Health")]
    public int curHP;
    public int maxHP;
    private Weapon weapon;

    //Enemy states
    public float sightRange, attackRange;
    public bool PlrInSight, PlrInAttack;

    private void Awake()
    {
        curHP = maxHP;
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        weapon = GetComponent<Weapon>();
        PlrInSight = false;
        
    }
    public void TakeDamage(int damage)
    {
        PlrInSight = true;
        curHP -= damage;
        if (curHP <= 0)
            Die();

    }

    public void Die()
    {
        Destroy(gameObject);
    }

 // Update is called once per frame
    void Update()
    {
        PlrInSight = Physics.CheckSphere(transform.position, sightRange, Player);
        PlrInAttack = Physics.CheckSphere(transform.position, attackRange, Player);

        //if I can't see player, patrol
        if (!PlrInSight && !PlrInAttack) Patrolling();
        //If I can see player, chase
        if (PlrInSight && !PlrInAttack) ChasePlayer();
        //If I'm in range, attack
        if (PlrInSight && PlrInAttack) AttackPlayer();

    }
    
    private void Patrolling ()
    {
        //find a patrol point if there isn't one
        if (!WalkPointSet) SearchWalkPoint();
        
        if (WalkPointSet)
        agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //Walkpoint Reached, remove patrol point
        if (distanceToWalkPoint.magnitude < 1f)
            WalkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Generate a patrol point
        float randomZ = Random.Range(-walkRange, walkRange);
        float randomX = Random.Range(-walkRange, walkRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        //if I'm touching the ground, start walking
        if(Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
            WalkPointSet = true;
    }
    
    private void ChasePlayer()
    { 
        agent.SetDestination(player.position);
    }
   private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            if(weapon.CanShoot())//fire my weapon
                weapon.Shoot();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
            
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //creates visual example of attackRange and sightRange in unity editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }


   
}
