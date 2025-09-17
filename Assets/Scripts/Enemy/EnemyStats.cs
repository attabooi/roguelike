using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public EnemyScriptableObjects enemyData;

    //current stats
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentMoveSpeed;
    [HideInInspector] public float currentDamage;


    public float despawnDistance = 20f;
    Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = enemyData.MaxHealth;
        currentMoveSpeed = enemyData.MoveSpeed;
        currentDamage = enemyData.Damage;

    }
    void Start()
    {
       
        player = FindObjectOfType<PlayerStats>().transform;

    }

    private void Update()
    {
        
        if(Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
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


    private void OnCollisionStay2D(Collision2D col)
    {
        //Reference the script from the collided collider and deal damage using TakeDamage()
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage); //make sure to use current damage instead of weaponData in case any damage multipliers in the future

        }
    }


    private void OnDestroy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        if (es != null)
        {
            es.OnenemyKilled();
        }

    }

    void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
}
