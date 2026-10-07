using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Move,
        Jump,
        Attack,
        Dead
    }
    private HashSet<EnemyObject> hitEnemies = new HashSet<EnemyObject>();
    public PlayerState currentState = PlayerState.Idle;
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
            if (currentState != PlayerState.Attack)
            {
                hitEnemies.Clear();
                currentState = PlayerState.Attack;
                animator.SetTrigger("Attack");
            }
        }
    }
    public void AttackEvent()
    {
        //Debug.Log("检测是否触发攻击");

        Collider[] hitColliders = Physics.OverlapSphere(
            attackPoint.position,
            attackRange,
            ememyLayer
            );

        foreach(Collider hitCollider in hitColliders)
        {
            EnemyObject health = hitCollider.GetComponent<EnemyObject>();
            if (health != null && !hitEnemies.Contains(health)) 
            {
                hitEnemies.Add(health);
                health.TakeDamage(1);
                //Debug.Log("打到了：" + hitCollider.name);
            }
        }
    }
    /// <summary>
    /// 播放攻击动画完成后的方法
    /// </summary>
    public void AttackFinish()
    {
        currentState = PlayerState.Idle;
    }
    public void EnableCombo()
    {
        //Debug.Log(1);
    }
}
