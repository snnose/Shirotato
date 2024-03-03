using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Singleton
    private static SpawnManager instance = null;

    public static SpawnManager Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    public GameObject[] monsterPrefabs;
    private GameObject copy;

    public List<GameObject> currentMonsters = new();

    private int currentNumber = 1;

    // 코루틴 함수
    IEnumerator spawnSandwich; // 1 ~ 2 round

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        StartSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartSpawn()
    {
        spawnSandwich = SpawnSandwich();

        StartCoroutine(spawnSandwich);
    }

    private IEnumerator SpawnSandwich()
    {
        // 라운드가 끝나기 전까지 리스폰
        // while(!GameRoot.Instance.IsRoundEnded())
        // {
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            float spawnPosX = Random.Range(-18, 18);
            float spawnPosY = Random.Range(-15, 15);
            float spawnInterval = Random.Range(0.4f, 1.0f);

            Vector2 spawnLocation = new Vector2(spawnPosX, spawnPosY);

            copy = Instantiate(monsterPrefabs[0], spawnLocation, monsterPrefabs[0].transform.rotation);
            // 몬스터 번호 부여
            copy.GetComponent<MonsterInfo>().SetMonsterNumber(currentNumber);
            currentNumber++;

            currentMonsters.Add(copy);

            yield return new WaitForSeconds(spawnInterval);
        }
        // }
    }

    public List<GameObject> GetCurrentMonsters()
    {
        return this.currentMonsters;
    }
}
