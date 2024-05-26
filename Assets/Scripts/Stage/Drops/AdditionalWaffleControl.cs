using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalWaffleControl : MonoBehaviour
{
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAllCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartAllCoroutine()
    {
        yield return StartCoroutine(UpriseImage());
        yield return StartCoroutine(DestroyImage());
    }

    private IEnumerator UpriseImage()
    {
        // 이미지가 0.2초에 걸쳐 커지면서 떠오른다
        float startTime = Time.time;
        float duration = 0.2f;

        while (Time.time < startTime + duration)
        {
            this.transform.position += new Vector3(0f, 0.01f);
            this.transform.localScale += new Vector3(0.0025f, 0.0025f);
            yield return null;
        }
    }

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


    /*
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
    */
}
