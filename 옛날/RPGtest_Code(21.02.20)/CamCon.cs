using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCon : MonoBehaviour
{
    private PlyManager pm;
    private Vector3 vec;

    private void Start()
    {
        pm = FindObjectOfType<PlyManager>();
        vec = transform.position - pm.transform.position;
    }

    private void Update()
    {
        transform.position = pm.transform.position + vec;
    }
}
