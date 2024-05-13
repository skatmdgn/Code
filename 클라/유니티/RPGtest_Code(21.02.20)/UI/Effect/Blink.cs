using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    [SerializeField] private float timer = 0f;
    private float timerReset;
    private Text txt;

    private void Start()
    {
        txt = GetComponent<Text>();
        timerReset = timer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        txt.color = new Color(1, 1, 1, timer);
        if (timer <= 0f)
        {
            timer = timerReset;
        }
    }
}
