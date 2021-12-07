using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// This Enemy AI was made with help from Dave/GameDevelopment. Video used: https://youtu.be/UjkSFoLxesw
public class EnAI : MonoBehaviour
{
    [Header("Navigation")]
    //navigation, player & ground recognition
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

    //Enemy sight and attack range, and bools to check if the player is in said range
    public float sightRange, attackRange;
    public bool PlrInSight, PlrInAttack;

    


    private void Awake()
    {
        //find the player in the scene, the navigation mesh, and the weapon script
        curHP = maxHP;
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        weapon = GetComponent<Weapon>();
        //make sure the enemy doesn't instantly go after the player
        PlrInSight = false;
       
        
        
        
    }
    public void TakeDamage(int damage)
    {
        //focus on the player
        PlrInSight = true;
        //subtract the bullet's damage value from current HP
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
        //these two cast an invisible sphere, try to find the player in that sphere
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
        
        //Start heading to the patrol point
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
        //works similar to the patrol points, but the player is the patrol point until player's out of range
        agent.SetDestination(player.position);
    }
   private void AttackPlayer()
    {
        //stop in place
        agent.SetDestination(transform.position);

        //look at the player, start firing
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //fire my weapon
            if(weapon.CanShoot())
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
