using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("0번 WarningBox, 1번 Setting, 2번 UI, 3번 TextBox")]
    [SerializeField] private GameObject[] obj = null;

    public GameObject returnWarningBox()
    {
        return obj[0];
    }

    public GameObject returnSettingBox()
    {
        return obj[1];
    }

    public GameObject returnInvenBox()
    {
        return obj[1].transform.GetChild(1).GetChild(1).gameObject;
    }

    public GameObject returnWeaponText()
    {
        return obj[1].transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
    }

    public GameObject returnUI()
    {
        return obj[2];
    }

    public GameObject returnTextBox()
    {
        return obj[3];
    }
}
