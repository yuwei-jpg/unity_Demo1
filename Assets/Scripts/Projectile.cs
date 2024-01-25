using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidBody2D;
    public float timer;
    // Start is called before the first frame update
    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }
    public void Launch(Vector2 direction, float force)
    {
        rigidBody2D.AddForce(direction * force);
        timer = 4.0f;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyBehaviourScript e = other.collider.GetComponent<EnemyBehaviourScript>();
        if (e != null)
        {
            e.Fix();
            Destroy(gameObject);
        }

        
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
            Destroy(gameObject);

    }
}
