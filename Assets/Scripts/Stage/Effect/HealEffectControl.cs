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

    // �̹����� 0.5�ʿ� ���� ���������鼭 �������
    private IEnumerator DestroyImage()
    {
        float startAlpha = this.GetComponent<SpriteRenderer>().color.a;
        Color startColor = this.GetComponent<SpriteRenderer>().color;

        float startTime = Time.time;
        float duration = 0.5f;

        while (Time.time < startTime + duration)
        {
            float normalizedTime = (Time.time - startTime) / duration;
            // ������ �ð��� ����� ���� �����ϰ� �Ѵ�
            Color currentColor = startColor;
            currentColor.a = Mathf.Lerp(startAlpha, 0f, normalizedTime);

            this.GetComponent<SpriteRenderer>().color = currentColor;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
