using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFlag2 : MonoBehaviour
{
    [SerializeField] private Transform[] _nextFlags = null;
    private List<Transform> flagList = new List<Transform>();

    private void Start()
    {
        addList();
    }

    private void addList()
    {
        for (int i = 0; i < _nextFlags.Length; ++i)
        {
            flagList.Add(_nextFlags[i]);
        }
    }

    public Transform returnFlag()
    {
        Transform tr = flagList[0];
        flagList.RemoveAt(0);
        if (flagList.Count == 0)
            addList();
        return tr;
    }
}
