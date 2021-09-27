using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    public Transform player;

    public float moveSpeed;

    private Rigidbody2D rb;

    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg ;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;



    }
void MoveEnemy(Vector2 direction)
{
    rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
}

void FixedUpdate()
{
    MoveEnemy(movement);
}

//Dies when touched by projectile
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
         Destroy(gameObject);//add a comma & a number here to add a death delay 

         if (other.CompareTag("Player"))
         {
             print("Player hit Enemy!");
         }
        
    }
}
