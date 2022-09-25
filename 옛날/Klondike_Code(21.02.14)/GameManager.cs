using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour
{
    private List<string> card = new List<string>();
    private int clearCard = 0;

    [SerializeField] private GameObject txt = null;

    private void Awake()
    {
        Screen.SetResolution(720, 960, false);

        for (int i = 1; i <= 13; i++)
        {
            card.Add("C" + i);
            card.Add("D" + i);
            card.Add("H" + i);
            card.Add("S" + i);
        }

        for (int i = 0; i < card.Count - 1; i++)
        {
            int j = Random.Range(i, card.Count);
            card.Insert(0, card[j]);
            card.RemoveAt(j + 1);
        }
    }

    void Start()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (clearCard == 52)
        {
            clearCard++;
            Time.timeScale = 0;
            txt.SetActive(true);
        }
    }

    public string returnCardStr()
    {
        string str = card[0];
        card.RemoveAt(0);
        return str;
    }

    public void reScene()
    {
        SceneManager.LoadScene(0);
    }

    public void cngTag(GameObject obj)
    {
        string str = Regex.Replace(obj.name, @"\d", "");
        if (str == "S" || str == "C")
        {
            obj.tag = "Black";
        }
        else
        {
            obj.tag = "Red";
        }
    }

    public void cngClear(int i)
    {
        clearCard += i;
    }
}
