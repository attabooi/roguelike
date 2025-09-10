using UnityEngine;


/// <summary>
/// Base script of all projectile behaviours to be palced on a prefab of a weapon that is a projectile
/// </summary>
public class ProjectileWeaponBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSeconds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0) // left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry < 0) // down
        {
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry > 0) // up
        {
            scale.x = scale.x * -1;
        }
        else if (dirx > 0 && diry > 0) // right up
        {
            rotation.z = 0f;
        }
        else if (dirx < 0 && diry > 0) // left up
        {
            rotation.z = -90f;
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        else if (dirx > 0 && diry < 0) // right down
        {
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry < 0) // left down
        {
            rotation.z = 0f;
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
            transform.localScale = scale;
            transform.rotation = Quaternion.Euler(rotation);
    }
}
