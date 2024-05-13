using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    private Image box;
    private Text txt;
    private Color boxColor;
    private Color txtColor;

    [SerializeField] private float timer = 0f;
    private float timerReset;

    private void Start()
    {
        box = GetComponent<Image>();
        txt = transform.GetChild(0).GetComponent<Text>();
        boxColor = box.color;
        txtColor = txt.color;
        timerReset = timer;
    }

    private void Update()
    {
        if (gameObject.activeSelf == true)
        {
            timer -= Time.deltaTime;
            setColor(timer);
            if (timer <= 0f)
            {
                gameObject.SetActive(false);
                setColor(1f);
                timer = timerReset;
            }
        }
    }

    private void setColor(float f)
    {
        boxColor.a = f;
        txtColor.a = f;
        box.color = boxColor;
        txt.color = txtColor;
    }
}
