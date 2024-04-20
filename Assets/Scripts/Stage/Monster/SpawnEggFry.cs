using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEggFry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        yield return StartCoroutine(SpawnEggFryTimer());
    }

    IEnumerator SpawnEggFryTimer()
    {
        // 5�ʿ� ���� ��ȭ�Ѵ�
        // ���� �Ӿ����� (�ִ� �ӱ� 75%)
        yield return StartCoroutine(Hatching());

        // 5�ʰ� �����ٸ� EggFry ����
        StartCoroutine(SpawnManager.Instance.StartSpawnEggFry(this.transform.position));

        // �ð��� ������ �ı�
        Destroy(this.gameObject);
    }

    // ��ȭ�ϸ鼭 ���� �Ӿ�����
    IEnumerator Hatching()
    {
        for (int i = 0; i < 100; i++)
        {
            Color color = this.GetComponent<SpriteRenderer>().color;
            color.g = 1f - (i * 0.0075f);
            color.b = 1f - (i * 0.0075f);
            this.GetComponent<SpriteRenderer>().color = color;

            yield return new WaitForSeconds(0.05f);
        }
    }
}