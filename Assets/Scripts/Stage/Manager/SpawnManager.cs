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

    // 0 = ������ġ, 1 = ����
    public GameObject[] monsterPrefabs;
    private GameObject warningSign;
    private GameObject copy;

    public List<GameObject> currentMonsters = new();

    private int currentNumber = 1;

    public IEnumerator startSpawn;
    private IEnumerator spawnMonster;

    // �ڷ�ƾ �Լ�
    IEnumerator spawnSandwich; // 1 ~ round
    IEnumerator spawnWatermelon;
    IEnumerator spawnSalad; // 4 ~

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
        // ���� ����
        if (!GameRoot.Instance.GetIsRoundClear())
        {
            StartCoroutine(startSpawn);
        }
        // ���� ����
        else
        {
            StopCoroutine(startSpawn);
        }
    }

    // ���� ���� �� �ش� ������ ���� ���̺��� �ҷ��� �����Ѵ�
    public IEnumerator StartSpawn(int currentRound)
    {
        float sandwichSpawnInterval;
        float watermelonSpawnInterval;

        switch (currentRound)
        {
            // 1����
            case 1:
                
                sandwichSpawnInterval = 3.0f;
                /*
                // ������ ����
                sandwichSpawnInterval = ActivateItemRelatedtSpawnInterval(sandwichSpawnInterval);
                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 3);
                StartCoroutine(spawnSandwich);
                */
                spawnSalad = SpawnSalad(1.0f, sandwichSpawnInterval, 3);
                StartCoroutine(spawnSalad);
                break;
            // 2����
            case 2:
                sandwichSpawnInterval = 6.0f;
                watermelonSpawnInterval = 3.0f;

                sandwichSpawnInterval = ActivateItemRelatedtSpawnInterval(sandwichSpawnInterval);
                watermelonSpawnInterval = ActivateItemRelatedtSpawnInterval(watermelonSpawnInterval);

                spawnSandwich = SpawnSandwich(9.0f, sandwichSpawnInterval, 4);
                spawnWatermelon = SpawnWatermelon(1.0f, watermelonSpawnInterval, 2);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnWatermelon);
                break;
            // 3����
            case 3:
                sandwichSpawnInterval = 4.0f;
                watermelonSpawnInterval = 7.0f;

                sandwichSpawnInterval = ActivateItemRelatedtSpawnInterval(sandwichSpawnInterval);
                watermelonSpawnInterval = ActivateItemRelatedtSpawnInterval(watermelonSpawnInterval);

                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 5);
                spawnWatermelon = SpawnWatermelon(9.0f, watermelonSpawnInterval, 2);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnWatermelon);
                break;
            // 4����
            case 4:

                break;
            default:
                break;
        }

        yield return null;
    }

    // ���� ���� �� ��� ���� �ڷ�ƾ�� �����Ѵ�.
    private void StopSpawn()
    {

    }

    // ���� Ÿ���� ���Ѵ� (�갳 or ��ħ)
    private bool ChooseSpawnType()
    {
        bool ret = false;

        int random = UnityEngine.Random.Range(0, 2);

        // ���� ���� 0�̸� �갳, 1�̸� ��ħ
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

        // x�� -8 ~ 8, y�� -7 ~ 7 ���� ������ ������ �ʵ��� ����
        // ���� �߾Ӻαٿ� �����Ǹ� ������ ������ ���� �� ����
        while(-8.0f <= spawnPosX && spawnPosX <= 8.0f
           && -7.0f <= spawnPosY && spawnPosY <= 7.0f)
        {
            spawnPosX = UnityEngine.Random.Range(-18f, 18f);
            spawnPosY = UnityEngine.Random.Range(-15f, 15f);
        }

        Vector2 tmp = new Vector2(spawnPosX, spawnPosY);

        return tmp;
    }

    // ������ ������ �����ϴ� �Լ�
    private void SetMonsterStatus(MonsterInfo monsterInfo, string monsterName)
    {
        int currentRound = GameRoot.Instance.GetCurrentRound();

        switch (monsterName)
        {
            case "Sandwich":
                monsterInfo.SetMonsterHP(3f + 2f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 0.6f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(Random.Range(4.4f, 6.6f));
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.01f);
                monsterInfo.SetMonsterLootDropRate(0.01f);
                break;
            case "Watermelon":
                monsterInfo.SetMonsterHP(1f + 1f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 0.6f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(7.6f);
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.02f);
                monsterInfo.SetMonsterLootDropRate(0.03f);
                break;
            case "Salad":
                monsterInfo.SetMonsterHP(8f + 1f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 0.6f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(3.8f);
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.03f);
                monsterInfo.SetMonsterLootDropRate(0.1f);
                break;
            default:
                break;
        }
    }

    // ���͸� ���� ���� ���Ŀ� �����ϴ� �ڷ�ƾ
    private IEnumerator SpawnMonster(GameObject monster, Vector2 spawnPos)
    {
        // �ش� ��ġ�� ���� ������ ���� 1�� �Ŀ� ���� ����
        GameObject sign = Instantiate(warningSign, spawnPos, monster.transform.rotation);

        yield return new WaitForSeconds(1.0f);
        GameObject copy = Instantiate(monster, spawnPos, monster.transform.rotation);

        MonsterInfo monsterInfo = copy.GetComponent<MonsterInfo>();
        // ���� ��ȣ �ο�
        monsterInfo.SetMonsterNumber(currentNumber);
        // ���� �ɷ�ġ ����
        SetMonsterStatus(monsterInfo, monster.name);
        currentNumber++;
        currentMonsters.Add(copy);

        yield return null;
    }

    private IEnumerator SpawnSandwich(float waitToStartTime, float spawnInterval, int monsterCount)
    {
        yield return new WaitForSeconds(waitToStartTime);

        // ���� ������� �ݺ�
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = ChooseSpawnType();
            monsterCount = monsterCount + Random.Range(0, 3);  // 5���� �������� �ּ�, �ִ� + 1�� �ұ�?
            
            // �갳 ����
            if (spawnType)
            {
                // ���� ������ ��ŭ ���� ��ҿ� ����
                for (int i = 0; i < monsterCount; i++)
                {
                    Vector2 spawnLocation = SetSpawnPos();

                    // ���� ����
                    spawnMonster = SpawnMonster(monsterPrefabs[0], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }
            // ��ħ ����
            if (!spawnType)
            {
                // �����Ǵ� ��Ҹ� Ư���� ��
                Vector2 spawnLocation = SetSpawnPos();
                for (int i = 0; i < monsterCount; i++)
                {
                    // �� ��ü�� �� ���� �������� �ʵ��� �������ش�
                    float posX = Random.Range(-2.0f, 2.0f);
                    float posY = Random.Range(-2.0f, 2.0f);
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

    private IEnumerator SpawnWatermelon(float waitToStartTime, float spawnInterval, int monsterCount)
    {
        yield return new WaitForSeconds(waitToStartTime);

        // ���� ������� �ݺ�
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = ChooseSpawnType();
            monsterCount = monsterCount + Random.Range(0, 3);  // 5���� �������� �ּ�, �ִ� + 1�� �ұ�?

            // �갳 ����
            if (spawnType)
            {
                // ���� ������ ��ŭ ���� ��ҿ� ����
                for (int i = 0; i < monsterCount; i++)
                {
                    Vector2 spawnLocation = SetSpawnPos();

                    // ���� ����
                    spawnMonster = SpawnMonster(monsterPrefabs[1], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }
            // ��ħ ����
            if (!spawnType)
            {
                // �����Ǵ� ��Ҹ� Ư���� ��
                Vector2 spawnLocation = SetSpawnPos();
                for (int i = 0; i < monsterCount; i++)
                {
                    // �� ��ü�� �� ���� �������� �ʵ��� �������ش�
                    float posX = Random.Range(-2.0f, 2.0f);
                    float posY = Random.Range(-2.0f, 2.0f);
                    spawnLocation.x += posX; spawnLocation.y += posY;

                    spawnMonster = SpawnMonster(monsterPrefabs[1], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    private IEnumerator SpawnSalad(float waitToStartTime, float spawnInterval, int monsterCount)
    {
        yield return new WaitForSeconds(waitToStartTime);

        // ���� ������� �ݺ�
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = true;

            // �갳 ����
            if (spawnType)
            {
                // ���� ������ ��ŭ ���� ��ҿ� ����
                for (int i = 0; i < monsterCount; i++)
                {
                    Vector2 spawnLocation = SetSpawnPos();

                    // ���� ����
                    spawnMonster = SpawnMonster(monsterPrefabs[2], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    private float ActivateItemRelatedtSpawnInterval(float spawnInterval)
    {
        float tmp = spawnInterval;
        tmp = ActivateNormalItem40(tmp);
        tmp = ActivateRareItem37(tmp);
        tmp = ActivateEpicItem28(tmp);

        return tmp;
    }

    // NormalItem40 ���� �� �� ���� �� +5%
    private float ActivateNormalItem40(float spawnInterval)
    {
        float tmp = spawnInterval;
        int itemCount = ItemManager.Instance.GetOwnNormalItemList()[40];

        if (itemCount > 0)
        {
            tmp -= spawnInterval * (5f * itemCount / (100f + 5f * itemCount));
        }

        return tmp;
    }

    // RareItem37 ���� �� �� ���� �� -5%
    private float ActivateRareItem37(float spawnInterval)
    {
        float tmp = spawnInterval;
        int itemCount = ItemManager.Instance.GetOwnRareItemList()[37];

        if (itemCount > 0)
        {
            tmp += spawnInterval * (5f * itemCount / (100f + 5f * itemCount));
        }

        return tmp;
    }

    // EpicItem28 ���� �� �� ���� �� +10%
    private float ActivateEpicItem28(float spawnInterval)
    {
        float tmp = spawnInterval;
        int itemCount = ItemManager.Instance.GetOwnEpicItemList()[28];

        if (itemCount > 0)
        {
            tmp -= spawnInterval * (10f * itemCount / (100f + 10f * itemCount));
        }

        return tmp;
    }

    public List<GameObject> GetCurrentMonsters()
    {
        return this.currentMonsters;
    }
}
