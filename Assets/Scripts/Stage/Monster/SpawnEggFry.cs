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

    private void OnDestroy()
    {
        // 현재 생존 몬스터 리스트에서 삭제
        SpawnManager.Instance.GetCurrentMonsters().Remove(this.gameObject);
    }

    IEnumerator StartTimer()
    {
        yield return StartCoroutine(SpawnEggFryTimer());
    }

    IEnumerator SpawnEggFryTimer()
    {
        // 5초에 걸쳐 부화한다
        // 점점 붉어진다 (최대 붉기 65%)
        yield return StartCoroutine(Hatching());

        // 5초가 지났다면 EggFry 스폰
        StartCoroutine(SpawnManager.Instance.StartSpawnEggFry(this.transform.position));

        // 시간이 지나면 파괴
        Destroy(this.gameObject);
    }

    // 부화하면서 점점 붉어진다
    IEnumerator Hatching()
    {
        for (int i = 0; i < 100; i++)
        {
            Color color = this.GetComponent<SpriteRenderer>().color;
            color.g = 1f - (i * 0.0065f);
            color.b = 1f - (i * 0.0065f);
            this.GetComponent<SpriteRenderer>().color = color;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
