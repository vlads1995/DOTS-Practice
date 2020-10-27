using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfomanceGeneralSpawner : MonoBehaviour
{
    public int spawnCount;
    public GameObject prefab;

    private Mover[] array;

    private void Awake()
    {
        array = new Mover[spawnCount];

        for (int i = 0; i < spawnCount; i++)
        {
            var go = Instantiate(prefab);
            array[i] = go.GetComponent<Mover>();
        }
    }

    private void Update()
    {
        foreach (var go in array)
        {
            go.transform.Translate(go.dir * Time.deltaTime);
        }
    }
}
