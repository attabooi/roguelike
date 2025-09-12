using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public EnemyScriptableObjects enemyData;

    //current stats
    float currentHealth;
    float currentMoveSpeed;
    float currentDamage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = enemyData.MaxHealth;
        currentMoveSpeed = enemyData.MoveSpeed;
        currentDamage = enemyData.Damage;

    }

    // Update is called once per frame
    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
