using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestCon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject SwordShieldTem = null;

    private CamManager cm;
    private UIManager um;

    private void Start()
    {
        cm = FindObjectOfType<CamManager>();
        um = FindObjectOfType<UIManager>();
    }

    public void OnPointerClick(PointerEventData e)
    {
        Instantiate(SwordShieldTem, um.returnInvenBox().transform);
        cm.return2ndCam().SetActive(false);
        um.returnTextBox().SetActive(false);
        cm.returnMainCam().SetActive(true);
        um.returnUI().SetActive(true);
    }
}
