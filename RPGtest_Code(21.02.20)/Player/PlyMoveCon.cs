using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlyMoveCon : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float RunSpeed = 0f;

    private RectTransform bgImg;
    private RectTransform stickImg;
    private float radius;
    private Vector2 pos = Vector2.zero;
    private Vector3 moveDis = Vector3.zero;
    
    private bool onTouch = false;

    private PlyManager pm;

    private void Start()
    {
        bgImg = GetComponent<RectTransform>();
        radius = bgImg.rect.width * 0.5f;
        stickImg = transform.GetChild(0).GetComponent<RectTransform>();
        pm = FindObjectOfType<PlyManager>();
    }

    private void Update()
    {
        if (onTouch)
        {
            Run();
            if (pos != null)
                pm.transform.rotation = Quaternion.Euler(0, Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg, 0);
        }
    }

    public void OnDrag(PointerEventData e)
    {
        pos = e.position - (Vector2)bgImg.position;
        pos = Vector2.ClampMagnitude(pos, radius);
        stickImg.localPosition = pos;

        pos = pos.normalized;
        moveDis = new Vector3(pos.x, 0f, pos.y);
    }

    public void OnPointerDown(PointerEventData e)
    {
        onTouch = true;
        pm.returnPlyAni().SetBool("Run", true);
    }

    public void OnPointerUp(PointerEventData e)
    {
        onTouch = false;
        pm.returnPlyAni().SetBool("Run", false);
        stickImg.localPosition = Vector3.zero;
        moveDis = Vector3.zero;
    }

    private void Run()
    {
        pm.returnPlyRigi().MovePosition(pm.returnPlyRigi().position + moveDis * RunSpeed * Time.deltaTime);
    }
}
