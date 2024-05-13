using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ITemCon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private UIManager um; //무기텍스트 가져오기
    private PlyManager pm; //방패, 무기 가져오기
    private bool onClk = false;
    private Vector3 beforePos = Vector3.zero;
    private RectTransform tr;
    private bool onAct = false;

    private void Start()
    {
        um = FindObjectOfType<UIManager>();
        pm = FindObjectOfType<PlyManager>();
        tr = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (onClk)
        {
            tr.position = Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData e)
    {
        onClk = true;
        beforePos = tr.position;
    }

    public void OnPointerUp(PointerEventData e)
    {
        onClk = false;
        tr.position = beforePos;
        if (onAct)
        {
            setAct(true);
        }
        else if (!um.returnWeaponText().activeSelf)
        {
            setAct(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Equip")
        {
            beforePos = collision.GetComponent<RectTransform>().position;
            onAct = true;
        }
        else if (collision.tag == "Inven")
        {
            beforePos = collision.GetComponent<RectTransform>().position;
            onAct = false;
        }
    }

    private void setAct(bool b)
    {
        um.returnWeaponText().SetActive(!um.returnWeaponText().activeSelf);
        for (int i = 0; i < 2; ++i)
        {
            pm.returnEquip(i).SetActive(!pm.returnEquip(i).activeSelf);
        }
        pm.setWeapon(b);
    }
}
