using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    public void takeDamage(int damage)
    {
        enemyCurrentHealth -= damage;

        Debug.Log("当前生命" + enemyCurrentHealth);

        if (enemyCurrentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log(gameObject.name + "死亡");

        Destroy(gameObject);
    }
}
