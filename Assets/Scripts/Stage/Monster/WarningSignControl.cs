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
        Color color = Color.white;
        float startAlpha = this.gameObject.GetComponent<SpriteRenderer>().color.a;
        float startTime = Time.time;
        float duration = 1.0f;

        while (Time.time < startTime + duration)
        {
            float normalizedTime = (Time.time - startTime) / (duration);
            float alpha = Mathf.Lerp(startAlpha, 0f, normalizedTime);
            color.a = alpha;
            this.gameObject.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }
        
        /*
        while (Time.time < startTime + duration)
        {
            float normalizedTime = (Time.time - startTime) / (duration / 2);
            float alpha = Mathf.Lerp(0f, startAlpha, normalizedTime);
            color.a = alpha;
            this.gameObject.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }
        */
        yield return new WaitForSeconds(1.0f);

        Destroy(this.gameObject);

        yield return null;
    }
}
