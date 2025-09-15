using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollector;
    public float pullSpeed;


    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        playerCollector.radius = player.currentMagnet;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        //check if the other game object has the ICollectible interface
        if (col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            //pulling animation
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            //gets the Rigidbody2D component on the item
            Vector2 forceDirection = (transform.position - col.transform.position).normalized;
            //Vector2 pointing from the item to the player
            //Applies force to the item in the forseDirection at the pullSpeed
            rb.AddForce(forceDirection * pullSpeed);

            // if it does, call the collect method
            collectible.Collect();
        }
    }
}
