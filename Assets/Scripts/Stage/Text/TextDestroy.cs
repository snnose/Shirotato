using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 텍스트가 0.5초에 걸쳐 투명해진 후 사라진다
    private IEnumerator DestroyText()
    {
        float startAlpha = this.GetComponent<TextMeshPro>().alpha;
        float startTime = Time.time;
        float duration = 0.5f;

        while (Time.time < startTime + duration)
        {
            float normalizedTime = (Time.time - startTime) / duration;
            this.GetComponent<TextMeshPro>().alpha = Mathf.Lerp(startAlpha, 0f, normalizedTime);
            yield return null;
        }
        
        Destroy(this.gameObject);
    }
}
