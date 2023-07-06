using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    public bool StartCounter;
    public int Seconds, Minutes, Hours;
    public TMP_Text text;

    string t;

    void Start()
    {
        t = "0 hrs 0 min 0 s";
        /*if (StartCounter)
        {
            StartCoroutine("Counter");
        } */
        StartC();
    }

    // Update is called once per frame
    void Update()
    {
        if (Seconds == 0 && Minutes == 0 && Hours == 0)
        {
            StartC();
        }
        text.text = t;
    }

    void StartC()
    {
        StartCoroutine("Counter");
    }

    IEnumerator Counter()
    {

        if (StartCounter)
        {
            for (int i = 0; i < i + 1; i++)
            {
                Seconds++;
                if (Seconds >= 60)
                {
                    Minutes++;
                    Seconds = 1;
                }
                if (Minutes >= 60)
                {
                    Hours++;
                    Minutes = 0;
                }
                t = Hours.ToString() + " hrs " + Minutes.ToString() + " min " + Seconds.ToString() + " s";
                if (!StartCounter)
                {
                    yield break;
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
