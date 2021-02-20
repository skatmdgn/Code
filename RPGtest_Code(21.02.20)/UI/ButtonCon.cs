using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCon : MonoBehaviour
{
    private UIManager um;

    [Header("0번 기본창, 1번 가방창, 2번 캐릭터창")]
    [SerializeField] private GameObject[] obj = null;

    private void Start()
    {
        um = FindObjectOfType<UIManager>();
    }

    public void setAct(bool b)
    {
        um.returnSettingBox().SetActive(b);
        gameObject.SetActive(!b);
        if (obj[1].activeSelf)
        {
            Inven();
        }
    }

    public void Inven()
    {
        for (int i = 0; i < 2; ++i)
        {
            obj[i].SetActive(!obj[i].activeSelf);
        }
    }

    public void exit()
    {
        Application.Quit();
    }
}
