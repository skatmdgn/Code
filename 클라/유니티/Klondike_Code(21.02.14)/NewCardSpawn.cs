using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewCardSpawn : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject cardPrefab = null;

    private GameManager gm;

    private int cardNum = 0;
    private List<string> card = new List<string>();

    private GameObject obj;
    private SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        spr = GetComponent<SpriteRenderer>();

        for (int i = 0; i < 24; i++)
        {
            card.Add(gm.returnCardStr());
        }

        obj = gameObject;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (card.Count != 0)
        {
            if (cardNum < card.Count)
            {
                obj.layer = 8;
                obj = Instantiate(cardPrefab, new Vector2(-5f, transform.position.y), transform.rotation, obj.transform);
                obj.layer = 9;
                obj.name = card[cardNum];
                obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(card[cardNum]);
                obj.GetComponent<SpriteRenderer>().sortingOrder = cardNum;
                obj.GetComponent<BoxCollider2D>().enabled = true;
                obj.AddComponent<CardCon>();
                cardNum++;
                if (cardNum == card.Count)
                {
                    spr.enabled = false;
                }
            }
            else
            {
                obj = gameObject;
                spr.enabled = true;
                cardNum = 0;
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }

    public void miList(string str)
    {
        card.Remove(str);
        cardNum--;
        Transform child = transform;
        while (child.childCount != 0)
        {
            child = child.GetChild(0).transform;
        }
        obj = child.parent.gameObject;
        obj.layer = 9;
    }
}
