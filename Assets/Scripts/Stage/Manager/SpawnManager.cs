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
    private GameObject warningSign;
    private GameObject copy;

    public List<GameObject> currentMonsters = new();

    private int currentNumber = 1;

    public IEnumerator startSpawn;
    private IEnumerator spawnMonster;

    // 코루틴 함수
    IEnumerator spawnSandwich; // 1 ~ 2 round

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        warningSign = Resources.Load<GameObject>("Prefabs/Monsters/WarningSign");
    }

    void Start()
    {
        startSpawn = StartSpawn(GameRoot.Instance.GetCurrentRound());
    }

    // Update is called once per frame
    void Update()
    {
        // 스폰 시작
        if (!GameRoot.Instance.GetIsRoundClear())
        {
            StartCoroutine(startSpawn);
        }
        // 스폰 종료
        else
        {
            StopCoroutine(startSpawn);
        }
    }

    // 라운드 시작 시 해당 라운드의 몬스터 테이블을 불러와 스폰한다
    public IEnumerator StartSpawn(int currentRound)
    {
        switch(currentRound)
        {
            case 1:
                spawnSandwich = SpawnSandwich(1.0f, 2.0f);
                StartCoroutine(spawnSandwich);
                break;
            case 2:
                break;
            default:
                break;
        }

        yield return null;
    }

    // 라운드 종료 시 모든 스폰 코루틴을 종료한다.
    private void StopSpawn()
    {

    }

    // 스폰 타입을 정한다 (산개 or 뭉침)
    private bool ChooseSpawnType()
    {
        bool ret = false;

        int random = UnityEngine.Random.Range(0, 2);

        // 랜덤 값이 0이면 산개, 1이면 뭉침
        if (random == 0)
            ret = true;
        else
            ret = false;

        return ret;
    }

    private Vector2 SetSpawnPos()
    { 
        float spawnPosX = 0;
        float spawnPosY = 0;

        // x값 -8 ~ 8, y값 -7 ~ 7 사이 범위가 나오지 않도록 조정
        // 맵의 중앙부근에 스폰되면 불쾌한 경험이 많을 것 같음
        while(-8.0f <= spawnPosX && spawnPosX <= 8.0f
           && -7.0f <= spawnPosY && spawnPosY <= 7.0f)
        {
            spawnPosX = UnityEngine.Random.Range(-18f, 18f);
            spawnPosY = UnityEngine.Random.Range(-15f, 15f);
        }

        Vector2 tmp = new Vector2(spawnPosX, spawnPosY);

        return tmp;
    }

    // 몬스터의 스탯을 설정하는 함수
    private void SetMonsterStatus(MonsterInfo monsterInfo, string monsterName)
    {
        switch (monsterName)
        {
            case "Sandwich":
                monsterInfo.SetMonsterHP(3f + 2f * GameRoot.Instance.GetCurrentRound());
                monsterInfo.SetMonsterDamage(1f + 0.6f * GameRoot.Instance.GetCurrentRound());
                monsterInfo.SetMonsterMovementSpeed(Random.Range(4.4f, 6.6f));
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.5f);
                monsterInfo.SetMonsterLootDropRate(0.00f);
                break;
            default:
                break;
        }
    }

    // 몬스터를 워닝 사인 이후에 스폰하는 코루틴
    private IEnumerator SpawnMonster(GameObject monster, Vector2 spawnPos)
    {
        // 해당 위치에 워닝 사인을 띄우고 1초 후에 몬스터 스폰
        GameObject sign = Instantiate(warningSign, spawnPos, monster.transform.rotation);

        yield return new WaitForSeconds(1.0f);
        GameObject copy = Instantiate(monster, spawnPos, monster.transform.rotation);

        MonsterInfo monsterInfo = copy.GetComponent<MonsterInfo>();
        // 몬스터 번호 부여
        monsterInfo.SetMonsterNumber(currentNumber);
        // 몬스터 능력치 설정
        SetMonsterStatus(monsterInfo, monster.name);
        currentNumber++;
        currentMonsters.Add(copy);

        yield return null;
    }

    private IEnumerator SpawnSandwich(float waitToStartTime, float spawnInterval)
    {
        yield return new WaitForSeconds(waitToStartTime);

        // 라운드 종료까지 반복
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = ChooseSpawnType();
            int monsterCount = Random.Range(3, 6);  // 5라운드 단위마다 최소, 최대 + 1씩 할까?
            
            // 산개 스폰
            if (spawnType)
            {
                // 몬스터 마릿수 만큼 랜덤 장소에 스폰
                for (int i = 0; i < monsterCount; i++)
                {
                    Vector2 spawnLocation = SetSpawnPos();

                    // 몬스터 스폰
                    spawnMonster = SpawnMonster(monsterPrefabs[0], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }
            // 뭉침 스폰
            if (!spawnType)
            {
                // 스폰되는 장소를 특정한 후
                Vector2 spawnLocation = SetSpawnPos();
                for (int i = 0; i < monsterCount; i++)
                {
                    // 각 개체가 한 곳에 스폰되지 않도록 조정해준다
                    float posX = Random.Range(-1.0f, 1.0f);
                    float posY = Random.Range(-1.0f, 1.0f);
                    spawnLocation.x += posX; spawnLocation.y += posY;

                    spawnMonster = SpawnMonster(monsterPrefabs[0], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    public List<GameObject> GetCurrentMonsters()
    {
        return this.currentMonsters;
    }
}
