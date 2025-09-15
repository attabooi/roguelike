using UnityEngine;

public class NewMonoBehaviourScript : Pickup, ICollectible
{
    public int healthToRestore;
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthToRestore);
    }

}
