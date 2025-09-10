using UnityEngine;


/// <summary>
/// base script of all melee weapons
/// </summary>
public class MeleeWeaponBehaviour : MonoBehaviour
{

    public float destroyAfterSeconds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

}
