using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlyAtkCon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private UIManager um;
    private PlyManager pm;
    
    [SerializeField] private bool onAtk = false;

    private void Start()
    {
        um = FindObjectOfType<UIManager>();
        pm = FindObjectOfType<PlyManager>();
    }

    private void Update()
    {
        if (onAtk)
            pm.returnPlyAni().SetBool("Atk", true);
    }

    public void OnPointerDown(PointerEventData e)
    {
        if (!pm.returnWeaponBool())
        {
            um.returnWarningBox().SetActive(true);
            return;
        }
        onAtk = true;
    }

    public void OnPointerUp(PointerEventData e)
    {
        onAtk = false;
    }
}
