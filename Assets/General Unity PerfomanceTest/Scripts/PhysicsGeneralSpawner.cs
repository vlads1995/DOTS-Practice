using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PhysicsGeneralSpawner : MonoBehaviour
{
    public GameObject physicsCube;

    public int height = 100;
    public int width = 10;
    public int length = 10;

    void Start()
    {
        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                for (int l = 0; l < length; l++)
                {
                    if (w != 0 && l != 0 && l != length - 1 && w != width - 1)
                    {
                        continue;
                    }

                    Instantiate(physicsCube, new Vector3(l - length/2 , h + 0.5f, w - width/2), quaternion.identity);
                }
            }
        }
    }
}
