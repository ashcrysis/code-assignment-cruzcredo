using System.Collections;
using Settings;
using VFX;

namespace Enemy
{
 using UnityEngine;
using UnityEngine.AI;

public class RatEnemy : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 5;
    public int health;

    [Header("Detection")]
    public float detectRange = 10f;
    public float attackRange = 1.2f;
    public float fleeDistance = 5f;

    [Header("Attack")]
    public float attackCooldown = 1.2f;
    float lastAttackTime;

    [Header("References")]
    NavMeshAgent agent;
    Animator anim;
    Transform player;

    [Header("State")]
    bool isDead;
    bool isStunned;
    public GameObject attackHitbox;
    
    [Header("Damage Feedback")]
    public float knockbackForce = 6f;
    public float stunTime = 0.2f;

    Rigidbody2D rb;
    FlashTexture flash;
    
    [Header("Wander")]
    public float wanderRadius = 5f;
    public float idleChance = 0.4f;
    public float wanderWaitTime = 2f;

    float wanderTimer;
    bool wandering;
    
    bool isAttacking = false;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        flash = GetComponent<FlashTexture>();
    }
    Vector3 RandomNavmeshPoint(float radius)
    {
        Vector2 randomDirection = Random.insideUnitCircle * radius;
        Vector3 randomPoint = transform.position + new Vector3(randomDirection.x, randomDirection.y, 0);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas))
            return hit.position;

        return transform.position;
    }
    void Wander()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer > 0)
            return;

        wanderTimer = wanderWaitTime;

        if (Random.value < idleChance)
        {
            Idle();
            wandering = false;
        }
        else
        {
            Vector3 dest = RandomNavmeshPoint(wanderRadius);
            agent.SetDestination(dest);
            anim.Play("Idle");
            wandering = true;
        }
    }
    public void EnableHitbox()
    {
        attackHitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        attackHitbox.SetActive(false);
    }
    void Start()
    {
        health = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (isAttacking) return;
        if (isDead || isStunned) return;
        if (!agent.enabled || !agent.isOnNavMesh)
            return;
        if (GameState.Paused) return;
        
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attackRange)
        {
            Attack();
            Flip();
            return;
        }

        if (health == 1)
        {
            Flee();
        }
        else if (dist <= detectRange)
        {
            Chase();
        }
        else
        {
            Wander();
        }

        Flip();
    }

    void Idle()
    {
        anim.Play("Idle");
        agent.ResetPath();
    }

    void Chase()
    {
        if (!agent.enabled || !agent.isOnNavMesh) return;
        anim.Play("Idle");
        agent.SetDestination(player.position);
    }

    void Flee()
    {
        anim.Play("Idle");

        Vector3 dir = (transform.position - player.position).normalized;
        Vector3 fleePos = transform.position + dir * fleeDistance;

        agent.SetDestination(fleePos);
    }

    void Attack()
    {
        if (Time.time < lastAttackTime + attackCooldown || isAttacking)
            return;

        agent.ResetPath();

        int attack = Random.Range(0, 2);

        if (attack == 0)
            anim.Play("Attack1");
        else
            anim.Play("Attack2");

        lastAttackTime = Time.time;
        isAttacking = true;
    }

    public void OnAttackAnimationEnd()
    {
        isAttacking = false;
    }

    public void TakeDamage(int damage, Vector2 direction)
    {
        if (isDead || isStunned) return;

        health -= damage;

        flash?.Flash();

        if (health <= 0)
        {
            Die();
            return;
        }

        isStunned = true;

        anim.Play("Hurt");

        StartCoroutine(Knockback(direction));
        
    }
    IEnumerator Knockback(Vector2 direction)
    {
        agent.enabled = false;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(stunTime);

        rb.linearVelocity = Vector2.zero;

        agent.enabled = true;

        isStunned = false;
    }
    void Die()
    {
        isDead = true;

        agent.ResetPath();

        anim.Play("Death");
    }
    
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    void Flip()
    {
        float x = agent.enabled ? agent.velocity.x : rb.linearVelocity.x;

        if (x > 0.05f)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (x < -0.05f)
            transform.localScale = new Vector3(1, 1, 1);
    }
}
}