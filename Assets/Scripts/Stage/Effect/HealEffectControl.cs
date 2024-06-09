using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffectControl : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyImage());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 이미지가 0.5초에 걸쳐 투명해지면서 사라진다
    private IEnumerator DestroyImage()
    {
        float startAlpha = this.GetComponent<SpriteRenderer>().color.a;
        Color startColor = this.GetComponent<SpriteRenderer>().color;

        float startTime = Time.time;
        float duration = 0.5f;

        while (Time.time < startTime + duration)
        {
            float normalizedTime = (Time.time - startTime) / duration;
            // 투명도를 시간의 경과에 따라 투명하게 한다
            Color currentColor = startColor;
            currentColor.a = Mathf.Lerp(startAlpha, 0f, normalizedTime);

            this.GetComponent<SpriteRenderer>().color = currentColor;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
