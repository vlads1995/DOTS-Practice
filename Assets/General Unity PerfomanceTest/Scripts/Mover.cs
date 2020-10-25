using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Vector3 dir;

    private void Awake()
    {
        dir = new Vector3(UnityEngine.Random.Range(-1f,1f),UnityEngine.Random.Range(-1f,1f),UnityEngine.Random.Range(-1f,1f));
    }

    void Update()
    {
        transform.Translate(dir * Time.deltaTime / 5);
    }
}
