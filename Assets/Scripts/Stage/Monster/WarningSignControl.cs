using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSignControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlinkWarningSign());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ¿ö´× »çÀÎÀÌ 1ÃÊ µ¿¾È ±ôºýÀÎ´Ù
    private IEnumerator BlinkWarningSign()
    {
        float duration = 0.25f;
        bool isReverse = false;

        for (int i = 0; i < 4; i++)
        {
            if (i / 2 == 0)
                isReverse = false;
            else
                isReverse = true;

            float startTime = Time.time;
            StartCoroutine(Blink(startTime, duration, isReverse));
            yield return new WaitForSeconds(duration);
        }

        Destroy(this.gameObject);

        yield return null;
    }

    private IEnumerator Blink(float startTime, float duration, bool isReverse)
    {
        Color color = Color.white;
        float alpha = 0f;
        float startAlpha = this.gameObject.GetComponent<SpriteRenderer>().color.a;

        while (Time.time < startTime + duration)
        {
            float normalizedTime = (Time.time - startTime) / (duration);

            if (!isReverse)
                alpha = Mathf.Lerp(0f, startAlpha, normalizedTime);
            else
                alpha = Mathf.Lerp(startAlpha, 0f, normalizedTime);

            color.a = alpha;
            this.gameObject.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }
    }
}
