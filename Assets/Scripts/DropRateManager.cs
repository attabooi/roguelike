using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;

    }

    public List<Drops> drops;


    void OnDestroy()
    {

        if (!gameObject.scene.isLoaded) //Stops the spawning error from appearing when stopping play mode
        {
            return;
        }
        float randomNumber = Random.Range(0f, 100f);
        List<Drops> possibleDrops = new List<Drops>();
        foreach (Drops rate in drops)
        {
            if (randomNumber <= rate.dropRate)
            {
                possibleDrops.Add(rate);
            }

        }
        //check if there are any possible drops
        if (possibleDrops.Count > 0)
        {
            Drops drop = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
            Instantiate(drop.itemPrefab, transform.position, Quaternion.identity);
        }
    }
}



