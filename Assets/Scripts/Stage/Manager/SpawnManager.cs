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

    // 0 = ������ġ, 1 = ���Ʈ
    public GameObject[] monsterPrefabs;
    public GameObject[] bossMonsterPrefabs;
    private GameObject warningSign;
    private GameObject greenSign;
    private GameObject copy;

    public List<GameObject> currentMonsters = new();

    private int currentNumber = 1;
    private int difficulty = 0;

    public IEnumerator startSpawn;
    private IEnumerator stopSpawn;
    private IEnumerator spawnMonster;

    // �ڷ�ƾ �Լ�
    IEnumerator spawnPotato;        // ��������� ���� ���� ������
    IEnumerator spawnSandwich;      // 1 ~
    IEnumerator spawnYogurt;    // 2 ~
    IEnumerator spawnStrongYogurt;
    IEnumerator spawnWatermelon;     // 3 ~
    IEnumerator spawnSalad;         // 4 ~
    IEnumerator spawnDaepe;
    IEnumerator spawnStrongDaepe;
    IEnumerator spawnSpaghetti;
    IEnumerator spawnSeaweedroll;
    IEnumerator spawnTofuLarge;
    IEnumerator spawnFrenchFries;   // fly
    IEnumerator spawnTakoyaki;      // healer
    IEnumerator spawnDumpling;      // buffer
    IEnumerator spawnEgg;

    // ���� ����
    IEnumerator spawnSalmonSushi;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        warningSign = Resources.Load<GameObject>("Prefabs/Monsters/WarningSign");
        greenSign = Resources.Load<GameObject>("Prefabs/Monsters/GreenSign");
    }

    void Start()
    {
        startSpawn = StartSpawn(GameRoot.Instance.GetCurrentRound());
        difficulty = RoundSetting.Instance.GetDifficulty();
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
            StartCoroutine(stopSpawn);
        }
    }

    // ���� ���� �� �ش� ������ ���� ���̺��� �ҷ��� �����Ѵ�
    public IEnumerator StartSpawn(int currentRound)
    {
        // ���� ���� �ڷ�ƾ ����
        stopSpawn = StopSpawn();
        // ��� ���忡�� ���� ����
        spawnPotato = SpawnPotatoes();
        StartCoroutine(spawnPotato);

        float sandwichSpawnInterval;
        float yogurtSpawnInterval;
        float strongYogurtSpawnInterval;
        float saladSpawnInterval;
        float watermelonSpawnInterval;
        float daepeSpawnInterval;
        float strongDaepeSpawnInterval;
        float spaghettiSpawnInterval;
        float seaweedrollSpawnInterval;
        float tofuLargeSpawnInterval;
        float frenchFriesSpawnInterval;
        float takoyakiSpawnInterval;
        float dumplingSpawnInterval;
        float eggSpawnInterval;

        switch (currentRound)
        {
            // 1����
            case 1:
                // ������ �ð� ����
                sandwichSpawnInterval = 3.0f;
                // �׽�Ʈ
                //takoyakiSpawnInterval = 20.0f;

                // ������ ���� ������ ����
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(sandwichSpawnInterval);
                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 5);
                StartCoroutine(spawnSandwich);

                // �׽�Ʈ
                //spawnSalmonSushi = SpawnSalmonSushi(1.0f, 999f, 1);
                //StartCoroutine(spawnSalmonSushi);

                break;
            // 2����
            case 2:
                // ������ �ð� ����
                sandwichSpawnInterval = 6.0f;
                yogurtSpawnInterval = 3.0f;

                // ������ ���� ������ ����
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(sandwichSpawnInterval);
                yogurtSpawnInterval = ActivateItemRelatedSpawnInterval(yogurtSpawnInterval);

                spawnSandwich = SpawnSandwich(9.0f, sandwichSpawnInterval, 5);
                spawnYogurt = SpawnYogurt(1.0f, yogurtSpawnInterval, 4);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnYogurt);
                break;
            // 3����
            case 3:
                // ������ �ð� ����
                sandwichSpawnInterval = 4.0f;
                yogurtSpawnInterval = 7.0f;

                // ������ ���� ������ ����
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(sandwichSpawnInterval);
                yogurtSpawnInterval = ActivateItemRelatedSpawnInterval(yogurtSpawnInterval);

                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 5);
                spawnYogurt = SpawnYogurt(9.0f, yogurtSpawnInterval, 5);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnYogurt);

                // ���̵� ����� �̻��� �� �����Ǵ� ���� ����
                if (difficulty >= 2)
                {
                    watermelonSpawnInterval = 5.0f;

                    watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(watermelonSpawnInterval);

                    spawnWatermelon = SpawnWatermelon(10f, watermelonSpawnInterval, 2);
                    spawnFrenchFries = SpawnFrenchFries(10f, 999f, 1);

                    StartCoroutine(spawnWatermelon);
                    StartCoroutine(spawnFrenchFries);
                }
                break;
            // 4����
            case 4:
                // ������ �ð� ����
                saladSpawnInterval = 5.0f;
                sandwichSpawnInterval = 3f;

                // ������ ���� ������ ����
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(saladSpawnInterval);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(sandwichSpawnInterval);

                spawnSalad = SpawnSalad(1.0f, saladSpawnInterval, 2);
                spawnSandwich = SpawnSandwich(4.0f, sandwichSpawnInterval, 6);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnSalad);

                // ���̵� ����� �̻��� �� �����Ǵ� ���� ����
                if (difficulty >= 2)
                {
                    seaweedrollSpawnInterval = ActivateItemRelatedSpawnInterval(4.0f);

                    spawnSeaweedroll = SpawnSeaweedroll(3f, seaweedrollSpawnInterval, 2);
                    spawnFrenchFries = SpawnFrenchFries(15f, 999f, 2);

                    StartCoroutine(spawnSeaweedroll);
                    StartCoroutine(spawnFrenchFries);
                }
                break;
            // 5����
            case 5:
                // ������ �ð� ����
                sandwichSpawnInterval = 4f;
                yogurtSpawnInterval = 5f;
                saladSpawnInterval = 6.0f;

                // ������ ���� ������ ����
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(sandwichSpawnInterval);
                yogurtSpawnInterval = ActivateItemRelatedSpawnInterval(yogurtSpawnInterval);
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(saladSpawnInterval);

                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 5);
                spawnYogurt = SpawnYogurt(3.0f, yogurtSpawnInterval, 4);
                spawnSalad = SpawnSalad(25.0f, saladSpawnInterval, 2);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnYogurt);
                StartCoroutine(spawnSalad);

                // ���̵� ����� �̻��� �� �����Ǵ� ���� ����
                if (difficulty >= 2)
                {
                    watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(4f);

                    spawnWatermelon = SpawnWatermelon(15f, watermelonSpawnInterval, 3);

                    StartCoroutine(spawnWatermelon);
                }
                break;
            // 6����
            case 6:
                // ������ �ð� ����
                watermelonSpawnInterval = 5f;
                yogurtSpawnInterval = 3f;
                sandwichSpawnInterval = 3f;

                // ������ ���� ������ ����
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(watermelonSpawnInterval);
                yogurtSpawnInterval = ActivateItemRelatedSpawnInterval(yogurtSpawnInterval);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(sandwichSpawnInterval);

                spawnWatermelon = SpawnWatermelon(1.0f, watermelonSpawnInterval, 4);
                spawnYogurt = SpawnYogurt(3.0f, yogurtSpawnInterval, 4);
                spawnSandwich = SpawnSandwich(25.0f, sandwichSpawnInterval, 5);

                StartCoroutine(spawnWatermelon);
                StartCoroutine(spawnYogurt);
                StartCoroutine(spawnSandwich);

                // ���̵� ����� �̻��� �� �����Ǵ� ���� ����
                if (difficulty >= 2)
                {
                    watermelonSpawnInterval = 10.0f;
                    frenchFriesSpawnInterval = 5.0f;

                    watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(watermelonSpawnInterval);
                    frenchFriesSpawnInterval = ActivateItemRelatedSpawnInterval(frenchFriesSpawnInterval);

                    spawnWatermelon = SpawnWatermelon(15f, watermelonSpawnInterval, 2);
                    spawnFrenchFries = SpawnFrenchFries(15f, frenchFriesSpawnInterval, 2);

                    StartCoroutine(spawnWatermelon);
                    StartCoroutine(spawnFrenchFries);
                }
                break;
            // 7����
            case 7:
                // ������ �ð� ����
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(2f);
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(5f);

                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 8);
                spawnWatermelon = SpawnWatermelon(10f, watermelonSpawnInterval, 6);
                spawnSalad = SpawnSalad(30f, saladSpawnInterval, 2);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnWatermelon);
                StartCoroutine(spawnSalad);

                // ���̵� ����� �̻��� �� �����Ǵ� ���� ����
                if (difficulty >= 2)
                {
                    takoyakiSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                    eggSpawnInterval = ActivateItemRelatedSpawnInterval(8f);

                    spawnTakoyaki = SpawnTakoyaki(1.0f, takoyakiSpawnInterval, 1);
                    spawnEgg = SpawnEgg(5.0f, eggSpawnInterval, 2);

                    StartCoroutine(spawnTakoyaki);
                    StartCoroutine(spawnEgg);
                }
                break;
            // 8����
            case 8:
                // ������ �ð� ����
                daepeSpawnInterval = ActivateItemRelatedSpawnInterval(8f);
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(4f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(1.5f);

                spawnDaepe = SpawnDaepe(1.0f, daepeSpawnInterval, 3);
                spawnSalad = SpawnSalad(1.0f, saladSpawnInterval, 2);
                spawnSandwich = SpawnSandwich(5.0f, sandwichSpawnInterval, 4);

                StartCoroutine(spawnDaepe);
                StartCoroutine(spawnSalad);
                StartCoroutine(spawnSandwich);

                // ���̵� ����� �̻��� �� �����Ǵ� ���� ����
                if (difficulty >= 2)
                {
                    strongDaepeSpawnInterval = ActivateItemRelatedSpawnInterval(8f);

                    spawnStrongDaepe = SpawnStrongDaepe(15.0f, strongDaepeSpawnInterval, 1);

                    StartCoroutine(spawnStrongDaepe);
                }
                break;
            // 9����
            case 9:
                // ������ �ð� ����
                yogurtSpawnInterval = ActivateItemRelatedSpawnInterval(1f);
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(4f);
                daepeSpawnInterval = ActivateItemRelatedSpawnInterval(8f);

                spawnYogurt = SpawnYogurt(1.0f, yogurtSpawnInterval, 6);
                spawnWatermelon = SpawnWatermelon(10.0f, watermelonSpawnInterval, 2);
                spawnDaepe = SpawnDaepe(22.0f, daepeSpawnInterval, 2);

                StartCoroutine(spawnYogurt);
                StartCoroutine(spawnWatermelon);
                StartCoroutine(spawnDaepe);

                // ���̵� ����� �̻��� �� �����Ǵ� ���� ����
                if (difficulty >= 2)
                {
                    strongYogurtSpawnInterval = ActivateItemRelatedSpawnInterval(2f);

                    spawnStrongYogurt = SpawnStrongYogurt(5.0f, strongYogurtSpawnInterval, 3);

                    StartCoroutine(spawnStrongYogurt);
                }
                break;
            // 10����
            case 10:
                // ������ �ð� ����
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(2f);
                daepeSpawnInterval = ActivateItemRelatedSpawnInterval(8f);
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(8f);
                yogurtSpawnInterval = ActivateItemRelatedSpawnInterval(2f);

                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 7);
                spawnDaepe = SpawnDaepe(5.0f, daepeSpawnInterval, 3);
                spawnWatermelon = SpawnWatermelon(20.0f, watermelonSpawnInterval, 2);
                spawnYogurt = SpawnYogurt(30.0f, yogurtSpawnInterval, 5);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnDaepe);
                StartCoroutine(spawnWatermelon);
                StartCoroutine(spawnYogurt);
                break;
            // 11����
            case 11:
                // ������ �ð� ����
                spaghettiSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(6f);
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(3f);

                spawnSpaghetti = SpawnSpaghetti(1.0f, spaghettiSpawnInterval, 3);
                spawnSandwich = SpawnSandwich(3.0f, sandwichSpawnInterval, 3);
                spawnWatermelon = SpawnWatermelon(5.0f, watermelonSpawnInterval, 3);
                spawnSalad = SpawnSalad(25.0f, saladSpawnInterval, 2);

                StartCoroutine(spawnSpaghetti);
                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnWatermelon);
                StartCoroutine(spawnSalad);

                // ���̵� ����� �̻��� �� �����Ǵ� ���� ����
                if (difficulty >= 2)
                {
                    frenchFriesSpawnInterval = ActivateItemRelatedSpawnInterval(5f);

                    spawnFrenchFries = SpawnFrenchFries(15f, frenchFriesSpawnInterval, 2);

                    StartCoroutine(spawnFrenchFries);
                }
                break;
            // 12����
            case 12:
                // ������ �ð� ����
                yogurtSpawnInterval = ActivateItemRelatedSpawnInterval(2f);
                spaghettiSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                daepeSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(3f);

                spawnYogurt = SpawnYogurt(1f, yogurtSpawnInterval, 8);
                spawnSpaghetti = SpawnSpaghetti(5f, spaghettiSpawnInterval, 1);
                spawnDaepe = SpawnDaepe(15.0f, daepeSpawnInterval, 3);
                spawnSandwich = SpawnSandwich(25.0f, sandwichSpawnInterval, 5);
                spawnWatermelon = SpawnWatermelon(30.0f, watermelonSpawnInterval, 3);

                StartCoroutine(spawnYogurt);
                StartCoroutine(spawnSpaghetti);
                StartCoroutine(spawnDaepe);
                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnWatermelon);

                // ���̵� ����� �̻��� �� �����Ǵ� ���� ����
                if (difficulty >= 2)
                {
                    takoyakiSpawnInterval = ActivateItemRelatedSpawnInterval(8f);

                    spawnTakoyaki = SpawnTakoyaki(20f, takoyakiSpawnInterval, 2);

                    StartCoroutine(spawnTakoyaki);
                }
                break;
            // 13����
            case 13:
                // ������ �ð� ����
                seaweedrollSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(8f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                daepeSpawnInterval = ActivateItemRelatedSpawnInterval(8f);
                strongDaepeSpawnInterval = ActivateItemRelatedSpawnInterval(4f);

                spawnSeaweedroll = SpawnSeaweedroll(1.0f, seaweedrollSpawnInterval, 8);
                spawnSalad = SpawnSalad(4.0f, saladSpawnInterval, 2);
                spawnSandwich = SpawnSandwich(8.0f, sandwichSpawnInterval, 4);
                spawnDaepe = SpawnDaepe(30.0f, daepeSpawnInterval, 3);
                spawnStrongDaepe = SpawnStrongDaepe(40.0f, strongDaepeSpawnInterval, 1);

                StartCoroutine(spawnSeaweedroll);
                StartCoroutine(spawnSalad);
                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnDaepe);
                StartCoroutine(spawnStrongDaepe);

                // ���̵� ����� �̻��� �� �����Ǵ� ���� ����
                if (difficulty >= 2)
                {
                    frenchFriesSpawnInterval = ActivateItemRelatedSpawnInterval(5f);

                    spawnFrenchFries = SpawnFrenchFries(10f, frenchFriesSpawnInterval, 3);

                    StartCoroutine(spawnFrenchFries);
                }
                break;
            // 14����
            case 14:
                // ������ �ð� ����
                tofuLargeSpawnInterval = ActivateItemRelatedSpawnInterval(8f);
                seaweedrollSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(4f);
                daepeSpawnInterval = ActivateItemRelatedSpawnInterval(10f);

                spawnTofuLarge = SpawnTofuLarge(1.0f, tofuLargeSpawnInterval, 3);
                spawnSeaweedroll = SpawnSeaweedroll(2.0f, seaweedrollSpawnInterval, 8);
                spawnSandwich = SpawnSandwich(15.0f, sandwichSpawnInterval, 6);
                spawnDaepe = SpawnDaepe(30.0f, daepeSpawnInterval, 3);

                StartCoroutine(spawnTofuLarge);
                StartCoroutine(spawnSeaweedroll);
                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnDaepe);
                break;
            // 15����
            case 15:
                // ������ �ð� ����
                tofuLargeSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                seaweedrollSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(10f);
                strongYogurtSpawnInterval = ActivateItemRelatedSpawnInterval(5f);

                spawnTofuLarge = SpawnTofuLarge(1.0f, tofuLargeSpawnInterval, 3);
                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 10);
                spawnSeaweedroll = SpawnSeaweedroll(1.0f, seaweedrollSpawnInterval, 5);
                spawnSalad = SpawnSalad(9.0f, saladSpawnInterval, 3);
                spawnStrongYogurt = SpawnStrongYogurt(20.0f, strongYogurtSpawnInterval, 2);

                StartCoroutine(spawnTofuLarge);
                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnSeaweedroll);
                StartCoroutine(spawnSalad);
                StartCoroutine(spawnStrongYogurt);

                // ���̵� ����� �̻��� �� �����Ǵ� ���� ����
                if (difficulty >= 2)
                {
                    eggSpawnInterval = ActivateItemRelatedSpawnInterval(5f);

                    spawnEgg = SpawnEgg(5f, eggSpawnInterval, 2);

                    StartCoroutine(spawnEgg);
                }
                break;
            // 16����
            case 16:
                // ������ �ð� ����
                seaweedrollSpawnInterval = ActivateItemRelatedSpawnInterval(4f);
                dumplingSpawnInterval = ActivateItemRelatedSpawnInterval(4f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                strongYogurtSpawnInterval = ActivateItemRelatedSpawnInterval(2f);
                strongDaepeSpawnInterval = ActivateItemRelatedSpawnInterval(4f);
                daepeSpawnInterval = ActivateItemRelatedSpawnInterval(6f);

                spawnSeaweedroll = SpawnSeaweedroll(1.0f, seaweedrollSpawnInterval, 8);
                spawnDumpling = SpawnDumpling(1.0f, dumplingSpawnInterval, 1);
                spawnSandwich = SpawnSandwich(5.0f, sandwichSpawnInterval, 5);
                spawnStrongYogurt = SpawnStrongYogurt(29.0f, strongYogurtSpawnInterval, 5);
                spawnStrongDaepe = SpawnStrongDaepe(35.0f, strongDaepeSpawnInterval, 1);
                spawnDaepe = SpawnDaepe(40.0f, daepeSpawnInterval, 3);

                StartCoroutine(spawnSeaweedroll);
                StartCoroutine(spawnDumpling);
                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnStrongYogurt);
                StartCoroutine(spawnStrongDaepe);
                StartCoroutine(spawnDaepe);
                break;
            // 17����
            case 17:
                // ������ �ð� ����
                spaghettiSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                tofuLargeSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                seaweedrollSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                strongYogurtSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(2f);

                spawnSpaghetti = SpawnSpaghetti(1.0f, spaghettiSpawnInterval, 6);
                spawnTofuLarge = SpawnTofuLarge(10.0f, tofuLargeSpawnInterval, 2);
                spawnSeaweedroll = SpawnSeaweedroll(25.0f, seaweedrollSpawnInterval, 4);
                spawnStrongYogurt = SpawnStrongYogurt(30.0f, strongYogurtSpawnInterval, 5);
                spawnSandwich = SpawnSandwich(40.0f, sandwichSpawnInterval, 3);

                StartCoroutine(spawnSpaghetti);
                StartCoroutine(spawnTofuLarge);
                StartCoroutine(spawnSeaweedroll);
                StartCoroutine(spawnStrongYogurt);
                StartCoroutine(spawnSandwich);

                // ���̵� ����� �̻��� �� �����Ǵ� ���� ����
                if (difficulty >= 2)
                {
                    
                }
                break;
            // 18����
            case 18:
                // ������ �ð� ����
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                dumplingSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                seaweedrollSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                tofuLargeSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(3f);

                spawnWatermelon = SpawnWatermelon(1.0f, watermelonSpawnInterval, 10);
                spawnDumpling = SpawnDumpling(1.0f, dumplingSpawnInterval, 2);
                spawnSeaweedroll = SpawnSeaweedroll(4.0f, seaweedrollSpawnInterval, 5);
                spawnTofuLarge = SpawnTofuLarge(25.0f, tofuLargeSpawnInterval, 3);
                spawnSalad = SpawnSalad(45.0f, saladSpawnInterval, 3);

                StartCoroutine(spawnWatermelon);
                StartCoroutine(spawnDumpling);
                StartCoroutine(spawnSeaweedroll);
                StartCoroutine(spawnTofuLarge);
                StartCoroutine(spawnSalad);

                // ���̵� ����� �̻��� �� �����Ǵ� ���� ����
                if (difficulty >= 2)
                {
                    takoyakiSpawnInterval = ActivateItemRelatedSpawnInterval(5f);

                    spawnTakoyaki = SpawnTakoyaki(10f, takoyakiSpawnInterval, 2);

                    StartCoroutine(spawnTakoyaki);
                }
                break;
            // 19����
            case 19:
                // ������ �ð� ����
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(1f);
                seaweedrollSpawnInterval = ActivateItemRelatedSpawnInterval(2f);
                dumplingSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                strongDaepeSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                spaghettiSpawnInterval = ActivateItemRelatedSpawnInterval(5f);

                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 6);
                spawnDumpling = SpawnDumpling(10f, dumplingSpawnInterval, 2);
                spawnSeaweedroll = SpawnSeaweedroll(5.0f, seaweedrollSpawnInterval, 5);
                spawnStrongDaepe = SpawnStrongDaepe(15.0f, strongDaepeSpawnInterval, 3);
                spawnSpaghetti = SpawnSpaghetti(35.0f, spaghettiSpawnInterval, 3);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnDumpling);
                StartCoroutine(spawnSeaweedroll);
                StartCoroutine(spawnStrongDaepe);
                StartCoroutine(spawnSpaghetti);
                break;
            case 20:
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                yogurtSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(10f);
                daepeSpawnInterval = ActivateItemRelatedSpawnInterval(10f);
                spaghettiSpawnInterval = ActivateItemRelatedSpawnInterval(5f);

                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 10);
                spawnYogurt = SpawnYogurt(15f, yogurtSpawnInterval, 4);
                spawnWatermelon = SpawnWatermelon(25f, watermelonSpawnInterval, 3);
                spawnSalad = SpawnSalad(20f, saladSpawnInterval, 1);
                spawnDaepe = SpawnDaepe(45f, daepeSpawnInterval, 3);
                spawnSpaghetti = SpawnSpaghetti(55f, spaghettiSpawnInterval, 3);


                // ���� ����
                spawnSalmonSushi = SpawnSalmonSushi(1.0f, 999f, 1);
                StartCoroutine(spawnSalmonSushi);
                break;
            default:
                break;
        }

        yield return null;
    }

    // ���� ���� �� ��� ���� �ڷ�ƾ�� �����Ѵ�.
    private IEnumerator StopSpawn()
    {
        if (spawnPotato != null)
        {
            StopCoroutine(spawnPotato);
            spawnPotato = null;
        }
        if (spawnSandwich != null)
        {
            StopCoroutine(spawnSandwich);
            spawnSandwich = null;
        }
        if (spawnYogurt != null)
        {
            StopCoroutine(spawnYogurt);
            spawnYogurt = null;
        }
        if (spawnStrongYogurt != null)
        {
            StopCoroutine(spawnStrongYogurt);
            spawnStrongYogurt = null;
        }
        if (spawnWatermelon != null)
        {
            StopCoroutine(spawnWatermelon);
            spawnWatermelon = null;
        }
        if (spawnSalad != null)
        {
            StopCoroutine(spawnSalad);
            spawnSalad = null;
        }
        if (spawnDaepe != null)
        {
            StopCoroutine(spawnDaepe);
            spawnDaepe = null;
        }
        if (spawnStrongDaepe != null)
        {
            StopCoroutine(spawnStrongDaepe);
            spawnStrongDaepe = null;
        }
        if (spawnSpaghetti != null)
        {
            StopCoroutine(spawnSpaghetti);
            spawnSpaghetti = null;
        }
        if (spawnSeaweedroll != null)
        {
            StopCoroutine(spawnSeaweedroll);
            spawnSeaweedroll = null;
        }
        if (spawnTofuLarge != null)
        {
            StopCoroutine(spawnTofuLarge);
            spawnTofuLarge = null;
        }
        if (spawnFrenchFries != null)
        {
            StopCoroutine(spawnFrenchFries);
            spawnFrenchFries = null;
        }
        if (spawnTakoyaki != null)
        {
            StopCoroutine(spawnTakoyaki);
            spawnTakoyaki = null;
        }
        if (spawnDumpling != null)
        {
            StopCoroutine(spawnDumpling);
            spawnDumpling = null;
        }
        if (spawnEgg != null)
        {
            StopCoroutine(spawnEgg);
            spawnEgg = null;
        }

        yield return null;
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

        // ���� ��� ���� �ʿ�
        // �÷��̾��� ��ġ �������� �������� �ʵ���

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
            case "Potatoes":
                monsterInfo.type = "Potato";
                monsterInfo.SetMonsterHP(10f + 5f * currentRound);
                monsterInfo.SetMonsterDamage(0);
                monsterInfo.SetMonsterMovementSpeed(0);
                monsterInfo.SetMonsterWaffleDropCount(3);
                monsterInfo.SetMonsterConsumableDropRate(1f);
                monsterInfo.SetMonsterLootDropRate(0.2f);
                break;
            case "Sandwich":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(3f + 2f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 0.6f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(Random.Range(3.1f, 4.6f));
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.01f);
                monsterInfo.SetMonsterLootDropRate(0.01f);
                break;
            case "Yogurt":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(1f + 1f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 0.6f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(5.3f);
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.02f);
                monsterInfo.SetMonsterLootDropRate(0.03f);
                break;
            case "StrongYogurt":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(12f + 2f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 1.0f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(5.6f);
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.02f);
                monsterInfo.SetMonsterLootDropRate(0.03f);
                break;
            case "Watermelon":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(4f + 2.5f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 0.85f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(5.6f);
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.01f);
                monsterInfo.SetMonsterLootDropRate(0.01f);
                break;
            case "Salad":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(8f + 1f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 0.6f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(2.7f);
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.03f);
                monsterInfo.SetMonsterLootDropRate(0.1f);
                break;
            case "Daepe":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(20f + 11f * currentRound);
                monsterInfo.SetMonsterDamage(2f + 0.85f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(4.2f);
                monsterInfo.SetMonsterWaffleDropCount(3);
                monsterInfo.SetMonsterConsumableDropRate(0.03f);
                monsterInfo.SetMonsterLootDropRate(0.03f);
                break;
            case "StrongDaepe":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(30f + 22f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 1.15f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(4.2f);
                monsterInfo.SetMonsterWaffleDropCount(3);
                monsterInfo.SetMonsterConsumableDropRate(0.03f);
                monsterInfo.SetMonsterLootDropRate(0.03f);
                break;
            case "Spaghetti":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(10f + 24f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 1.2f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(2.1f);
                monsterInfo.SetMonsterWaffleDropCount(3);
                monsterInfo.SetMonsterConsumableDropRate(0.03f);
                monsterInfo.SetMonsterLootDropRate(0.03f);
                break;
            case "Seaweedroll":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(8f + 3f * currentRound);
                monsterInfo.SetMonsterDamage(1 + 1.0f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(Random.Range(3.2f, 3.9f));
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.01f);
                monsterInfo.SetMonsterLootDropRate(0.01f);
                break;
            case "TofuLarge":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(10f + 1f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 0.85f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(1.7f);
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.01f);
                monsterInfo.SetMonsterLootDropRate(0.01f);
                break;
            case "TofuSmall":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(15f + 5f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 1.0f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(4.9f);
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.01f);
                monsterInfo.SetMonsterLootDropRate(0.01f);
                break;
            case "FrenchFries":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(15f + 4f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 0.85f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(Random.Range(4.55f, 5.25f));
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.01f);
                monsterInfo.SetMonsterLootDropRate(0.01f);
                break;
            case "Takoyaki":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(10f + 8f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 0.85f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(5.6f);
                monsterInfo.SetMonsterWaffleDropCount(2);
                monsterInfo.SetMonsterConsumableDropRate(0.03f);
                monsterInfo.SetMonsterLootDropRate(0.03f);
                break;
            case "Dumpling":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(20f + 3f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 0.6f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(2.1f);
                monsterInfo.SetMonsterWaffleDropCount(2);
                monsterInfo.SetMonsterConsumableDropRate(0.01f);
                monsterInfo.SetMonsterLootDropRate(0.01f);
                break;
            case "Egg":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(5f + 3f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 0.6f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(0f);
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0f);
                monsterInfo.SetMonsterLootDropRate(0f);
                break;
            case "EggFry":
                monsterInfo.type = "General";
                monsterInfo.SetMonsterHP(50f + 25f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 1.15f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(Random.Range(3.5f, 4.2f)); ;
                monsterInfo.SetMonsterWaffleDropCount(3);
                monsterInfo.SetMonsterConsumableDropRate(0.01f);
                monsterInfo.SetMonsterLootDropRate(0.01f);
                break;
            // Boss
            case "SalmonSushi":
                monsterInfo.type = "Boss";
                monsterInfo.SetMonsterHP(29250);
                monsterInfo.SetMonsterDamage(30f);
                monsterInfo.SetMonsterMovementSpeed(4.2f);
                monsterInfo.SetMonsterWaffleDropCount(0);
                monsterInfo.SetMonsterConsumableDropRate(1f);
                monsterInfo.SetMonsterLootDropRate(1f);
                break;
            default:
                break;
        }
    }

    // ���͸� ���� ���� ���Ŀ� �����ϴ� �ڷ�ƾ
    private IEnumerator SpawnMonster(GameObject monster, Vector2 spawnPos)
    {
        // �ش� ��ġ�� ���� ������ ���� 1�� �Ŀ� ���� ����
        GameObject sign;
        // ���� ������ �ƴϸ� ������ ���� ����, �ƴϸ� �ʷϻ� ���� ����
        if (monster.name != "Potatoes")
            sign = Instantiate(warningSign, spawnPos, monster.transform.rotation);
        else
            sign = Instantiate(greenSign, spawnPos, monster.transform.rotation);

        yield return new WaitForSeconds(1.0f);
        // ���� ���� ��ġ �ٷ� ���� �÷��̾ �ִٸ� ���� ��ġ�� �����Ѵ�

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

    // ���� ����
    private IEnumerator SpawnPotatoes()
    {
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            yield return new WaitForSeconds(10f);
            GameObject potatoes = Resources.Load<GameObject>("Prefabs/Monsters/Potatoes");

            float random = Random.Range(0, 100f);
            // �� 10�ʸ��� 82.5% Ȯ���� �����ȴ�
            if (random <= 82.5f)
            {
                Vector2 spawnLocation = SetSpawnPos();

                // ���� ����
                spawnMonster = SpawnMonster(potatoes, spawnLocation);
                StartCoroutine(spawnMonster);
            }
            yield return null;
        }
    }

    private IEnumerator SpawnSandwich(float waitToStartTime, float spawnInterval, int monsterCount)
    {
        yield return new WaitForSeconds(waitToStartTime);

        // ���� ������� �ݺ�
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = ChooseSpawnType();
            
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
                    float posX = Random.Range(-3.0f, 3.0f);
                    float posY = Random.Range(-3.0f, 3.0f);
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

    private IEnumerator SpawnYogurt(float waitToStartTime, float spawnInterval, int monsterCount)
    {
        yield return new WaitForSeconds(waitToStartTime);

        // ���� ������� �ݺ�
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = ChooseSpawnType();

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
                    float posX = Random.Range(-3.0f, 3.0f);
                    float posY = Random.Range(-3.0f, 3.0f);
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

    private IEnumerator SpawnStrongYogurt(float waitToStartTime, float spawnInterval, int monsterCount)
    {
        yield return new WaitForSeconds(waitToStartTime);

        // ���� ������� �ݺ�
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = ChooseSpawnType();

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
            // ��ħ ����
            if (!spawnType)
            {
                // �����Ǵ� ��Ҹ� Ư���� ��
                Vector2 spawnLocation = SetSpawnPos();
                for (int i = 0; i < monsterCount; i++)
                {
                    // �� ��ü�� �� ���� �������� �ʵ��� �������ش�
                    float posX = Random.Range(-3.0f, 3.0f);
                    float posY = Random.Range(-3.0f, 3.0f);
                    spawnLocation.x += posX; spawnLocation.y += posY;

                    spawnMonster = SpawnMonster(monsterPrefabs[2], spawnLocation);
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

            // �갳 ����
            if (spawnType)
            {
                // ���� ������ ��ŭ ���� ��ҿ� ����
                for (int i = 0; i < monsterCount; i++)
                {
                    Vector2 spawnLocation = SetSpawnPos();

                    // ���� ����
                    spawnMonster = SpawnMonster(monsterPrefabs[3], spawnLocation);
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
                    float posX = Random.Range(-3.0f, 3.0f);
                    float posY = Random.Range(-3.0f, 3.0f);
                    spawnLocation.x += posX; spawnLocation.y += posY;

                    spawnMonster = SpawnMonster(monsterPrefabs[3], spawnLocation);
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
                    spawnMonster = SpawnMonster(monsterPrefabs[4], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    private IEnumerator SpawnDaepe(float waitToStartTime, float spawnInterval, int monsterCount)
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
                    spawnMonster = SpawnMonster(monsterPrefabs[5], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    private IEnumerator SpawnStrongDaepe(float waitToStartTime, float spawnInterval, int monsterCount)
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
                    spawnMonster = SpawnMonster(monsterPrefabs[6], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    private IEnumerator SpawnSpaghetti(float waitToStartTime, float spawnInterval, int monsterCount)
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
                    spawnMonster = SpawnMonster(monsterPrefabs[7], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    private IEnumerator SpawnSeaweedroll(float waitToStartTime, float spawnInterval, int monsterCount)
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
                    spawnMonster = SpawnMonster(monsterPrefabs[8], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    private IEnumerator SpawnTofuLarge(float waitToStartTime, float spawnInterval, int monsterCount)
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
                    spawnMonster = SpawnMonster(monsterPrefabs[9], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    public IEnumerator StartSpawnTofuSmall(Vector2 position)
    {
        yield return StartCoroutine(SpawnTofuSmall(position));
    }

    private IEnumerator SpawnTofuSmall(Vector2 position)
    {
        GameObject tofuSmall = Resources.Load<GameObject>("Prefabs/Monsters/TofuSmall");

        // ���� ������ ��ŭ ���� ��ҿ� ����
        for (int i = 0; i < 3; i++)
        {
            Vector2 errorPos = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            Vector2 spawnLocation = position + errorPos;

            // ���� ����
            spawnMonster = SpawnMonster(tofuSmall, spawnLocation);
            StartCoroutine(spawnMonster);
            yield return null;
        }
    }

    private IEnumerator SpawnFrenchFries(float waitToStartTime, float spawnInterval, int monsterCount)
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
                    spawnMonster = SpawnMonster(monsterPrefabs[10], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    private IEnumerator SpawnTakoyaki(float waitToStartTime, float spawnInterval, int monsterCount)
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
                    spawnMonster = SpawnMonster(monsterPrefabs[11], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    private IEnumerator SpawnDumpling(float waitToStartTime, float spawnInterval, int monsterCount)
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
                    spawnMonster = SpawnMonster(monsterPrefabs[12], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }


    private IEnumerator SpawnEgg(float waitToStartTime, float spawnInterval, int monsterCount)
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
                    spawnMonster = SpawnMonster(monsterPrefabs[13], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    public IEnumerator StartSpawnEggFry(Vector2 position)
    {
        yield return StartCoroutine(SpawnEggFry(position));
    }

    private IEnumerator SpawnEggFry(Vector2 position)
    {
        GameObject eggFry = Resources.Load<GameObject>("Prefabs/Monsters/EggFry");

        // ���� ������ ��ŭ ���� ��ҿ� ����
        for (int i = 0; i < 1; i++)
        {
            Vector2 errorPos = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            Vector2 spawnLocation = position + errorPos;

            // ���� ����
            spawnMonster = SpawnMonster(eggFry, spawnLocation);
            StartCoroutine(spawnMonster);
            yield return null;
        }
    }

    private IEnumerator SpawnSalmonSushi(float waitToStartTime, float spawnInterval, int monsterCount)
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
                    spawnMonster = SpawnMonster(bossMonsterPrefabs[0], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    private float ActivateItemRelatedSpawnInterval(float spawnInterval)
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
