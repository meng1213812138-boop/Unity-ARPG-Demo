using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyObject : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    private int atk = 1;

    private bool isDead = false;
    private GameObject player;
    public float attackRange = 1f;
    public float attackInterval = 2f;
    private float attackTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player == null)
            return;
        if (isDead)
            return;
        attackTimer -= Time.deltaTime;
        float distance = Vector3.Distance(
            transform.position,
            player.transform.position
        );
        if (distance > attackRange)
        {
            agent.isStopped = false;

            agent.SetDestination(player.transform.position);

            animator.SetBool("CanRun", true);
        }
        else
        {
            agent.isStopped = true;

            animator.SetBool("CanRun", false);

            if (attackTimer <= 0)
            {
                animator.SetTrigger("Attack");

                attackTimer = attackInterval;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
        Debug.Log("当前生命" + enemyCurrentHealth);
        if (enemyCurrentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("GetHit");
        }
    }
    void Die()
    {
        isDead = true;
        animator.SetBool("CanRun", false);
        animator.SetBool("IsDead",true);
        agent.isStopped = true;
    }

    public void DeadEvent()
    {
        Destroy(gameObject);
    }

    public void AttackEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(
            transform.position + transform.forward + transform.up,
            1,
            1 << LayerMask.NameToLayer("Player")
            );
        for (int i = 0; i < colliders.Length; i++)
        {
            PlayerController playerController = colliders[i].gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(atk);
            }
            else
                return;
        }
    }
}
