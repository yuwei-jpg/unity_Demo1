using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourScript : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
  
    public new Rigidbody2D rigidbody2D;
    Animator animator;
    float timer;
    int direction = 1;
    bool broken = true;
    //注意浮点数在这里一定要加f
    public ParticleSystem smokeEffect;
    // 在第一次帧更新之前调用 Start
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                direction = -direction;
                timer = changeTime;
            }
        if (!broken)
        {
            return;
        }

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction; 
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);

        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction; 
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
           
        }

        rigidbody2D.MovePosition(position);
        if (!broken)
        {
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.changeHealth(-1);
        }
    }
    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }
}
