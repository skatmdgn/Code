using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class CardCon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Rigidbody2D rigi;
    private SpriteRenderer spr;
    private Vector2 beforePos = Vector2.zero;

    private bool onClk = false;
    private bool onHit = false;

    private GameObject hitObj = null;

    private GameManager gm;
    private NewCardSpawn ncs;

    private int beforeLayer = 9;
    private string beforeTag;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        gm = FindObjectOfType<GameManager>();
        ncs = FindObjectOfType<NewCardSpawn>();
        beforeTag = tag;
    }

    void Update()
    {
        if (onClk)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        setPos();
        onClk = true;
        cngSort("Front", spr.sortingOrder);
        rigi.bodyType = RigidbodyType2D.Kinematic;
        if (gameObject.layer != 9)
        {
            beforeLayer = gameObject.layer;
            gameObject.layer = 9;
        }
        if (tag == "Top" || tag == "New")
        {
            gm.cngTag(gameObject);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onHit)
        {
            if (hitObj.tag != "Top")
            {
                if (returnNum(name) + 1 == returnNum(hitObj.name))
                {
                    if (hitObj.tag == "Bottom")
                    {
                        hitObj.GetComponent<BottomCardSet>().upLine();
                        beforePos = hitObj.transform.position;
                    }
                    else
                    {
                        beforePos = hitObj.transform.position + Vector3.down / 2;
                    }
                    cngNew();
                    afterHit();
                }
            }
            else
            {
                if ((returnNum(name) == 1 && returnName(hitObj.name) == "Top") || (returnName(name) == returnName(hitObj.name) && returnNum(name) == returnNum(hitObj.name) + 1))
                {
                    onTopHit();
                    afterHit();
                }
            }
        }

        transform.position = beforePos;
        gameObject.layer = beforeLayer;
        tag = beforeTag;
        onClk = false;
        cngSort("Obj", spr.sortingOrder);
        rigi.bodyType = RigidbodyType2D.Static;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (tag != collision.tag && collision.tag != "New" && collision.gameObject.layer == 9)
        {
            onHit = true;
            hitObj = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onHit = false;
    }

    private void cngSort(string str, int i)
    {
        spr.sortingLayerName = str;
        spr.sortingOrder = i;
        Transform child = transform;
        while (child.childCount != 0)
        {
            child = child.GetChild(0).transform;
            i++;
            child.GetComponent<SpriteRenderer>().sortingOrder = i;
            child.GetComponent<SpriteRenderer>().sortingLayerName = str;
            child.gameObject.layer = 8;
        }
        if (str == "Obj")
        {
            child.gameObject.layer = 9;
        }
    }

    private int returnNum(string str)
    {
        return int.Parse(Regex.Replace(str, @"\D", ""));
    }

    private string returnName(string str)
    {
        return Regex.Replace(str, @"\d", "");
    }

    private void onTopHit()
    {
        cngNew();
        beforePos = hitObj.transform.position;
        tag = "Top";
        gm.cngClear(1);
    }

    private void afterHit()
    {
        beforeTag = tag;
        if (transform.parent.gameObject.layer == 8)
        {
            transform.parent.gameObject.layer = 9;
        }
        transform.parent = hitObj.transform;
        cngSort("", hitObj.GetComponent<SpriteRenderer>().sortingOrder + 1);
        hitObj.layer = 8;
    }

    private void cngNew()
    {
        if (beforeTag == "New")
        {
            ncs.miList(name);
            gm.cngTag(gameObject);
        }
        if (beforeTag == "Top")
        {
            gm.cngClear(-1);
        }
    }

    private void setPos()
    {
        beforePos = transform.position;
        Transform child = transform;
        while (child.childCount != 0)
        {
            child = child.GetChild(0).transform;
            child.GetComponent<CardCon>().setBeforePos(child.position);
        }
    }

    public void setBeforePos(Vector2 vec)
    {
        beforePos = vec;
    }
}
