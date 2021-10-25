using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
public class EnemyAI : MonoBehaviour
{
    //Enemy stats
    public int curHP, maxHP, scoreToGive;
    //movement
    public float moveSpeed, attackRange, yPathOffset;

    //Coordinates for travel path
    private List<Vector3> path;
    //Enemy weapon
    private Weapon weapon;
    //Target to follow
    private GameObject target;


    

    // Start is called before the first frame update
    void Start()
    {
        //Get components
        weapon = GetComponent<Weapon>();
        target = FindObjectOfType<playerController>().gameObject;
        InvokeRepeating("UpdatePath", 0.0f, 0.5f);

        curHP = maxHP;
    }

    void UpdatePath()
    {
        //calculate a path to target
        NavMeshPath navMeshPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, navMeshPath);
        path = navMeshPath.corners.ToList();
    }

    void ChaseTarget()
    {
        if(path.Count == 0)
            return;
        //move towards target
        transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0,yPathOffset,0),moveSpeed * Time.deltaTime);

        if(transform.position == path[0] + new Vector3(0, yPathOffset, 0))
            path.RemoveAt(0);
    }
    
    public void TakeDamage(int damage)//applies damage to the AI
    {
        curHP -= damage;
        if(curHP <= 0)
            Die();
    }

    void Die()//deletes the enemy
    {
        Destroy(gameObject);
    }
    void Update()
    {
        //look at target
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.up * angle;
        
        //calculate distance between self and target
        float dist = Vector3.Distance(transform.position, target.transform.position);
        // attack when target is within distance
        if(dist <= attackRange)
        {
            if(weapon.CanShoot())
                weapon.Shoot();
        }
        // otherwise chase after target
        else
        {
            ChaseTarget();
        }

        
    }
}
