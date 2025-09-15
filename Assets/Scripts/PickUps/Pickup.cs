using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        //아이템 빨아들이는 기능
        if (col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
