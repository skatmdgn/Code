using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NpcCon : MonoBehaviour, IPointerClickHandler
{
    private CamManager cm;
    private UIManager um;
    private GameObject QuestIcon;
    private SpriteRenderer MapCon;

    private void Start()
    {
        cm = FindObjectOfType<CamManager>();
        um = FindObjectOfType<UIManager>();
        QuestIcon = transform.GetChild(2).gameObject;
        MapCon = transform.GetChild(3).GetComponent<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData e)
    {
        cm.returnMainCam().SetActive(false);
        um.returnUI().SetActive(false);
        cm.return2ndCam().SetActive(true);
        um.returnTextBox().SetActive(true);
        QuestIcon.SetActive(false);
        MapCon.color = Color.green;
        Destroy(this);
    }
}
