using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomCardSet : MonoBehaviour
{
    [SerializeField] private int line = 0;
    [SerializeField] private GameObject cardPrefab = null;

    private GameObject[] cardCount;
    private List<string> card = new List<string>();

    private GameManager gm;
    private BoxCollider2D col;

    private void OnEnable()
    {
        gm = FindObjectOfType<GameManager>();
        col = GetComponent<BoxCollider2D>();
        cardCount = new GameObject[line];
        cardSpawn();
    }

    private void cardSpawn()
    {
        for (int i = 0; i < line; i++)
        {
            cardCount[i] = Instantiate(cardPrefab, new Vector2(-7.5f, 3f), transform.rotation, transform);
            cardCount[i].GetComponent<SpriteRenderer>().sortingOrder = i;
            card.Add(gm.returnCardStr());
            cardCount[i].name = card[i];
            StartCoroutine(MoveTo(cardCount[i], new Vector3(transform.position.x, transform.position.y - 0.5f * i, transform.position.z)));
        }
    }

    IEnumerator MoveTo(GameObject obj, Vector3 pos)
    {
        while (true)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, pos, Time.deltaTime * 20f);
            if (obj.transform.position == pos)
            {
                if (obj == cardCount[line - 1])
                {
                    cngFront();
                }
                break;
            }
            yield return null;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < line)
        {
            line--;
            if (line > 0)
            {
                cngFront();
            }
            else
            {
                col.enabled = true;
                gameObject.layer = 9;
            }
        }
    }

    private void cngFront()
    {
        gm.cngTag(cardCount[line - 1]);
        cardCount[line - 1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(card[line - 1]);
        cardCount[line - 1].GetComponent<BoxCollider2D>().enabled = true;
        cardCount[line - 1].AddComponent<CardCon>();
        cardCount[line - 1].layer = 9;
    }

    public void upLine()
    {
        line++;
    }
}
