using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnAI : MonoBehaviour
{
    //navigation, recognition
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask Ground, Player;

    //Patrolling
    public Vector3 walkPoint;
    bool WalkPointSet;
    public float walkRange;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //Enemy states
    public float sightRange, attackRange;
    public bool PlrInSight, PlrInAttack;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        PlrInSight = false;
        
    }
 // Update is called once per frame
    void Update()
    {
        PlrInSight = Physics.CheckSphere(transform.position, sightRange, Player);
        PlrInAttack = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!PlrInSight && !PlrInAttack) Patrolling();
        if (PlrInSight && !PlrInAttack) ChasePlayer();
        if (PlrInSight && PlrInAttack) AttackPlayer();

    }
    
    private void Patrolling ()
    {
        if (!WalkPointSet) SearchWalkPoint();
        
        if (WalkPointSet)
        agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //Walkpoint Reached
        if (distanceToWalkPoint.magnitude < 1f)
            WalkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkRange, walkRange);
        float randomX = Random.Range(-walkRange, walkRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

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
            print ("attacking Player!"); //put some attack code here later lmao
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
            
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }


   
}
