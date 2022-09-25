using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyMove : MonoBehaviour
{
    [SerializeField] private Transform _nextFlag = null;
    [SerializeField] private float speed = 0f;
    private Vector3 dir;

    private void Start()
    {
        CalDir();
    }

    private void Update()
    {
        if (_nextFlag != null)
            Move_Angle();
    }

    private void CalDir()
    {
        dir = (_nextFlag.position - transform.position).normalized;
    }

    private void Move_Angle()
    {
        transform.position += dir * speed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir),  Time.deltaTime * speed);
    }

    private void cngFlag(Transform tr)
    {
        _nextFlag = tr;
        CalDir();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Flag")
            cngFlag(other.GetComponent<NextFlag>().returnFlag());
        else if(other.tag == "Flag2")
            cngFlag(other.GetComponent<NextFlag2>().returnFlag());
    }
}
