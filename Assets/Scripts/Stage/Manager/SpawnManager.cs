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

    // �ڷ�ƾ �Լ�
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

    private IEnumerator SpawnSandwich(float waitToStartTime, float spawnInterval)
    {
        yield return new WaitForSeconds(waitToStartTime);

        // ���� ������� �ݺ�
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = ChooseSpawnType();
            int monsterCount = Random.Range(3, 6);  // 5���� �������� �ּ�, �ִ� + 1�� �ұ�?
            
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
