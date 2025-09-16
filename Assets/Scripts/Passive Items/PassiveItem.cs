using UnityEngine;

public class PassiveItem : MonoBehaviour
{

    protected PlayerStats player;
    public PassiveItemScriptableObject passiveItemData;

    protected virtual void ApplyModifier()
    {
        //Apply the boost value to the asppropriate stat in the child classes
    }

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        ApplyModifier();
    }
 

}
