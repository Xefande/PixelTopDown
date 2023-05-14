using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    [System.Serializable]
    public class WeightedPrefab
    {
        public GameObject prefab;
        public int weight;
    }

    public List<WeightedPrefab> prefabs;
    public bool SpawnOnAwake = true;

    private void Awake()
    {
        if (SpawnOnAwake)
        {
            SpawnRandomPrefab();
        }
    }

    public void SpawnRandomPrefab()
    {
        int totalWeight = 0;

        foreach (WeightedPrefab wp in prefabs)
        {
            totalWeight += wp.weight;
        }

        int randomWeight = Random.Range(0, totalWeight);
        int currentWeight = 0;

        foreach(WeightedPrefab wp in prefabs)
        {
            currentWeight += wp.weight;
            if (randomWeight < currentWeight)
            {
                Instantiate(wp.prefab, transform.position, Quaternion.identity);
                break;
            }

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
