using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        //������ ���Ƶ��̴� ���
        if (col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
