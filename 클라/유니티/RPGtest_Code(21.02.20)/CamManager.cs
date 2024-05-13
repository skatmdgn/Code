using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    private GameObject[] obj = new GameObject[2];

    private void Start()
    {
        for (int i = 0; i < 2; ++i)
        {
            obj[i] = transform.GetChild(i).gameObject;
        }
    }

    public GameObject returnMainCam()
    {
        return obj[0];
    }

    public GameObject return2ndCam()
    {
        return obj[1];
    }
}
