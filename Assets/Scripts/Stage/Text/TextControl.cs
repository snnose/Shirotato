using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextControl : MonoBehaviour
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

    void PrintText(string text, Color color)
    {
        // 받은 대미지 텍스트 출력
        GameObject textObject = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro pro = textObject.GetComponent<TextMeshPro>();

        pro.text = text;
        pro.color = color;

        GameObject copy = Instantiate(textObject);
        Vector3 randomPos = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = this.gameObject.transform.position + randomPos;
    }
}
