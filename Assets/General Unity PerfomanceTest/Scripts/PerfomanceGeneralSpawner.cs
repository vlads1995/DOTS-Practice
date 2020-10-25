using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfomanceGeneralSpawner : MonoBehaviour
{
  public int spawnCount;
  public GameObject prefab;

  private void Awake()
  {
    for (int i = 0; i < spawnCount; i++)
    {
      Instantiate(prefab);
    }
  }
}
