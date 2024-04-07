using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private GameObject spawnPrefab;

    private void Start()
    {
        var spawned = Instantiate(spawnPrefab);
        spawned.transform.position = spawnTransform.position;
    }
}
