using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public enum PlayerState
    {
        Idel,
        Move,
        Jump,
        Attack,
        Dead
    }
    public PlayerState currentState = PlayerState.Idel;
    public Transform attackPoint;
    public float attackRange = 1.5f;
    public LayerMask ememyLayer;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //if(currentState == PlayerState.Attack)
            //    return;
            //currentState = PlayerState.Attack;
            animator.SetTrigger("Attack");
            AttackEvent();
        }
    }
    public void AttackEvent()
    {
        Debug.Log("检测是否触发攻击");

        Collider[] hitEnemies = Physics.OverlapSphere(
            attackPoint.position,
            attackRange,
            ememyLayer
            );

        foreach(Collider enemy in hitEnemies)
        {
            enemy health = enemy.GetComponent<enemy>();
            Debug.Log("打到了：" + enemy.name);

            if (health !=null)
            {
                health.takeDamage(1);
            }
        }
    }
    public void AttackFinish()
    {
        currentState = PlayerState.Idel;
    }
}
