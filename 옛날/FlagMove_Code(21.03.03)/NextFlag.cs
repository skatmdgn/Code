using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFlag : MonoBehaviour
{
    [SerializeField] private Transform _nextFlag = null;

    public Transform returnFlag()
    {
        return _nextFlag;
    }
}
