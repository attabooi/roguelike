using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    CharacterScriptableObject characterData;
    //current stats
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector] 
    public float currentMight;
    [HideInInspector] 
    public float currentProjectileSpeed; 
    [HideInInspector] 
    public float currentMagnet;


    //Exp and lvl
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    //class for defineing level ranges and experience cap increases
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;

    }

    //I-Frames
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;


    public List<LevelRange> levelRanges;

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    public GameObject secondWeaponTest;
    public GameObject firstPassiveItemTest, secondPassiveItemTest;


    void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        inventory = GetComponent<InventoryManager>();

        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;

        //Spawn the starting weapon
        SpawnWeapon(characterData.StartingWeapon);
        SpawnWeapon(secondWeaponTest);
        SpawnPassiveItem(firstPassiveItemTest);
        SpawnPassiveItem(secondPassiveItemTest);    

    }


    private void Start()
    {   
        //initialize the exp cap as the first exp cap increase
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    void Update()
    {
        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;

        }
        //if the invincibility timer has run out, set isInvincible to false
        else if (isInvincible)
        {
            isInvincible = false;
        }
        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if(experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            
            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if(level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }

            experienceCap += experienceCapIncrease;

        }
    }

    public void TakeDamage(float dmg)
    {
        //if the player is not currently invincible, take damage and start the invincibility timer
        if (!isInvincible)
        {
            currentHealth -= dmg;
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;


            if (currentHealth <= 0)
            {
                Kill();
            }

        }

    }
    public void Kill()
    {
        Debug.Log("Player Died");
    }

    public void RestoreHealth(float amount)
    {
        if(currentHealth < characterData.MaxHealth)
        {
            currentHealth += amount;
            if(currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
        
    }

    void Recover()
    {
        if(currentHealth < characterData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;
            //make sure current health does not exceed max health
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }

        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if (weaponIndex >= inventory.weaponSlots.Count - 1) //Must be -1 because a list starts from 0
        {
            Debug.LogError("Inventory slots already full");
            return;
        }
        //Spawn the starting weapon
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform); //set the weapon to be a child of the player
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>()); //Add the weapon to it's inventory slot

        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.passiveItemSlots.Count - 1) //Must be -1 because a list starts from 0
        {
            Debug.LogError("Inventory slots already full");
            return;
        }
        //Spawn the starting passive item
        GameObject spawnedWeaponPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedWeaponPassiveItem.transform.SetParent(transform); //set the weapon to be a child of the player
        inventory.AddPassiveItem(passiveItemIndex, spawnedWeaponPassiveItem.GetComponent<PassiveItem>()); //Add the weapon to it's inventory slot

        passiveItemIndex++;
    }
}
