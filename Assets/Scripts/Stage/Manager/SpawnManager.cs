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

    // 0 = 샌드위치, 1 = 수박
    public GameObject[] monsterPrefabs;
    private GameObject warningSign;
    private GameObject greenSign;
    private GameObject copy;

    public List<GameObject> currentMonsters = new();

    private int currentNumber = 1;

    public IEnumerator startSpawn;
    private IEnumerator spawnMonster;

    // 코루틴 함수
    IEnumerator spawnPotato;        // 브로테이토 나무 같은 포지션
    IEnumerator spawnSandwich;      // 1 ~
    IEnumerator spawnWatermelon;    // 2 ~
    IEnumerator spawnBlueberry;     // 3 ~
    IEnumerator spawnSalad;         // 4 ~
    IEnumerator spawnCheese;
    IEnumerator spawnStrongCheese;
    IEnumerator spawnSpaghetti;
    IEnumerator spawnPancakes;
    IEnumerator spawnTofuLarge;

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
            StopSpawn();
        }
    }

    // 라운드 시작 시 해당 라운드의 몬스터 테이블을 불러와 스폰한다
    public IEnumerator StartSpawn(int currentRound)
    {
        // 모든 라운드에서 감자 스폰
        spawnPotato = SpawnPotatoes();
        StartCoroutine(spawnPotato);

        float sandwichSpawnInterval;
        float watermelonSpawnInterval;
        float saladSpawnInterval;
        float blueberrySpawnInterval;
        float cheeseSpawnInterval;
        float strongCheeseSpawnInterval;
        float spaghettiSpawnInterval;
        float pancakesSpawnInterval;
        float tofuLargeSpawnInterval;

        switch (currentRound)
        {
            // 1라운드
            case 1:
                // 리스폰 시간 설정
                sandwichSpawnInterval = 3.0f;
                
                // 리스폰 관련 아이템 적용
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(sandwichSpawnInterval);
                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 4);
                StartCoroutine(spawnSandwich);
                
                break;
            // 2라운드
            case 2:
                // 리스폰 시간 설정
                sandwichSpawnInterval = 6.0f;
                watermelonSpawnInterval = 3.0f;

                // 리스폰 관련 아이템 적용
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(sandwichSpawnInterval);
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(watermelonSpawnInterval);

                spawnSandwich = SpawnSandwich(9.0f, sandwichSpawnInterval, 4);
                spawnWatermelon = SpawnWatermelon(1.0f, watermelonSpawnInterval, 3);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnWatermelon);
                break;
            // 3라운드
            case 3:
                // 리스폰 시간 설정
                sandwichSpawnInterval = 4.0f;
                watermelonSpawnInterval = 7.0f;

                // 리스폰 관련 아이템 적용
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(sandwichSpawnInterval);
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(watermelonSpawnInterval);

                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 5);
                spawnWatermelon = SpawnWatermelon(9.0f, watermelonSpawnInterval, 3);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnWatermelon);
                break;
            // 4라운드
            case 4:
                // 리스폰 시간 설정
                saladSpawnInterval = 5.0f;
                sandwichSpawnInterval = 3f;

                // 리스폰 관련 아이템 적용
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(saladSpawnInterval);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(sandwichSpawnInterval);

                spawnSalad = SpawnSalad(1.0f, saladSpawnInterval, 2);
                spawnSandwich = SpawnSandwich(4.0f, sandwichSpawnInterval, 6);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnSalad);
                break;
            // 5라운드
            case 5:
                // 리스폰 시간 설정
                sandwichSpawnInterval = 4f;
                watermelonSpawnInterval = 5f;
                saladSpawnInterval = 6.0f;

                // 리스폰 관련 아이템 적용
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(sandwichSpawnInterval);
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(watermelonSpawnInterval);
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(saladSpawnInterval);

                spawnSandwich = SpawnSandwich(4.0f, sandwichSpawnInterval, 5);
                spawnWatermelon = SpawnWatermelon(3.0f, watermelonSpawnInterval, 3);
                spawnSalad = SpawnSalad(25.0f, saladSpawnInterval, 2);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnWatermelon);
                StartCoroutine(spawnSalad);
                break;
            // 6라운드
            case 6:
                // 리스폰 시간 설정
                blueberrySpawnInterval = 5f;
                watermelonSpawnInterval = 3f;
                sandwichSpawnInterval = 3f;

                // 리스폰 관련 아이템 적용
                blueberrySpawnInterval = ActivateItemRelatedSpawnInterval(blueberrySpawnInterval);
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(watermelonSpawnInterval);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(sandwichSpawnInterval);

                spawnBlueberry = SpawnBlueberry(1.0f, blueberrySpawnInterval, 4);
                spawnWatermelon = SpawnWatermelon(3.0f, watermelonSpawnInterval, 3);
                spawnSandwich = SpawnSandwich(25.0f, sandwichSpawnInterval, 5);

                StartCoroutine(spawnBlueberry);
                StartCoroutine(spawnWatermelon);
                StartCoroutine(spawnSandwich);
                break;
            // 7라운드
            case 7:
                // 리스폰 시간 설정
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(2f);
                blueberrySpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(5f);

                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 8);
                spawnBlueberry = SpawnBlueberry(10f, blueberrySpawnInterval, 6);
                spawnSalad = SpawnSalad(30f, saladSpawnInterval, 2);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnBlueberry);
                StartCoroutine(spawnSalad);
                break;
            // 8라운드
            case 8:
                // 리스폰 시간 설정
                cheeseSpawnInterval = ActivateItemRelatedSpawnInterval(8f);
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(4f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(1.5f);

                spawnCheese = SpawnCheese(1.0f, cheeseSpawnInterval, 3);
                spawnSalad = SpawnSalad(1.0f, saladSpawnInterval, 2);
                spawnSandwich = SpawnSandwich(5.0f, sandwichSpawnInterval, 4);

                StartCoroutine(spawnCheese);
                StartCoroutine(spawnSalad);
                StartCoroutine(spawnSandwich);
                break;
            // 9라운드
            case 9:
                // 리스폰 시간 설정
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(1f);
                blueberrySpawnInterval = ActivateItemRelatedSpawnInterval(4f);
                cheeseSpawnInterval = ActivateItemRelatedSpawnInterval(8f);

                spawnWatermelon = SpawnWatermelon(1.0f, watermelonSpawnInterval, 6);
                spawnBlueberry = SpawnBlueberry(10.0f, blueberrySpawnInterval, 2);
                spawnCheese = SpawnCheese(22.0f, cheeseSpawnInterval, 2);

                StartCoroutine(spawnWatermelon);
                StartCoroutine(spawnBlueberry);
                StartCoroutine(spawnCheese);
                break;
            // 10라운드
            case 10:
                // 리스폰 시간 설정
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(2f);
                cheeseSpawnInterval = ActivateItemRelatedSpawnInterval(8f);
                blueberrySpawnInterval = ActivateItemRelatedSpawnInterval(8f);
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(2f);

                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 7);
                spawnCheese = SpawnCheese(5.0f, cheeseSpawnInterval, 3);
                spawnBlueberry = SpawnBlueberry(20.0f, blueberrySpawnInterval, 2);
                spawnWatermelon = SpawnWatermelon(30.0f, watermelonSpawnInterval, 5);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnCheese);
                StartCoroutine(spawnBlueberry);
                StartCoroutine(spawnWatermelon);
                break;
            // 11라운드
            case 11:
                // 리스폰 시간 설정
                spaghettiSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(6f);
                blueberrySpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(3f);

                spawnSpaghetti = SpawnSpaghetti(1.0f, spaghettiSpawnInterval, 3);
                spawnSandwich = SpawnSandwich(3.0f, sandwichSpawnInterval, 3);
                spawnBlueberry = SpawnBlueberry(5.0f, blueberrySpawnInterval, 3);
                spawnSalad = SpawnSalad(25.0f, saladSpawnInterval, 2);

                StartCoroutine(spawnSpaghetti);
                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnBlueberry);
                StartCoroutine(spawnSalad);
                break;
            // 12라운드
            case 12:
                // 리스폰 시간 설정
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(2f);
                spaghettiSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                cheeseSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                blueberrySpawnInterval = ActivateItemRelatedSpawnInterval(3f);

                spawnWatermelon = SpawnWatermelon(1f, watermelonSpawnInterval, 8);
                spawnSpaghetti = SpawnSpaghetti(5f, spaghettiSpawnInterval, 1);
                spawnCheese = SpawnCheese(15.0f, cheeseSpawnInterval, 3);
                spawnSandwich = SpawnSandwich(25.0f, sandwichSpawnInterval, 5);
                spawnBlueberry = SpawnBlueberry(30.0f, blueberrySpawnInterval, 3);

                StartCoroutine(spawnWatermelon);
                StartCoroutine(spawnSpaghetti);
                StartCoroutine(spawnCheese);
                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnBlueberry);
                break;
            // 13라운드
            case 13:
                // 리스폰 시간 설정
                pancakesSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(8f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                cheeseSpawnInterval = ActivateItemRelatedSpawnInterval(8f);
                strongCheeseSpawnInterval = ActivateItemRelatedSpawnInterval(4f);

                spawnPancakes = SpawnPancakes(1.0f, pancakesSpawnInterval, 8);
                spawnSalad = SpawnSalad(4.0f, saladSpawnInterval, 2);
                spawnSandwich = SpawnSandwich(8.0f, sandwichSpawnInterval, 4);
                spawnCheese = SpawnCheese(30.0f, cheeseSpawnInterval, 3);
                spawnStrongCheese = SpawnStrongCheese(40.0f, strongCheeseSpawnInterval, 1);

                StartCoroutine(spawnPancakes);
                StartCoroutine(spawnSalad);
                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnCheese);
                StartCoroutine(spawnStrongCheese);
                break;
            // 14라운드
            case 14:
                // 리스폰 시간 설정
                tofuLargeSpawnInterval = ActivateItemRelatedSpawnInterval(8f);
                pancakesSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(4f);
                cheeseSpawnInterval = ActivateItemRelatedSpawnInterval(10f);

                spawnTofuLarge = SpawnTofuLarge(1.0f, tofuLargeSpawnInterval, 3);
                spawnPancakes = SpawnPancakes(2.0f, pancakesSpawnInterval, 8);
                spawnSandwich = SpawnSandwich(15.0f, sandwichSpawnInterval, 6);
                spawnCheese = SpawnCheese(30.0f, cheeseSpawnInterval, 3);

                StartCoroutine(spawnTofuLarge);
                StartCoroutine(spawnPancakes);
                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnCheese);
                break;
            // 15라운드
            case 15:
                // 리스폰 시간 설정
                tofuLargeSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                pancakesSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(10f);
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(5f);

                spawnTofuLarge = SpawnTofuLarge(1.0f, tofuLargeSpawnInterval, 3);
                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 10);
                spawnPancakes = SpawnPancakes(1.0f, pancakesSpawnInterval, 5);
                spawnSalad = SpawnSalad(9.0f, saladSpawnInterval, 3);
                spawnWatermelon = SpawnWatermelon(20.0f, watermelonSpawnInterval, 2);

                StartCoroutine(spawnTofuLarge);
                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnPancakes);
                StartCoroutine(spawnSalad);
                StartCoroutine(spawnWatermelon);
                break;
            // 16라운드
            case 16:
                // 리스폰 시간 설정
                pancakesSpawnInterval = ActivateItemRelatedSpawnInterval(4f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(2f);
                strongCheeseSpawnInterval = ActivateItemRelatedSpawnInterval(4f);
                cheeseSpawnInterval = ActivateItemRelatedSpawnInterval(6f);

                spawnPancakes = SpawnPancakes(1.0f, pancakesSpawnInterval, 9);
                spawnSandwich = SpawnSandwich(5.0f, sandwichSpawnInterval, 7);
                spawnWatermelon = SpawnWatermelon(29.0f, watermelonSpawnInterval, 5);
                spawnStrongCheese = SpawnStrongCheese(35.0f, strongCheeseSpawnInterval, 1);
                spawnCheese = SpawnCheese(40.0f, cheeseSpawnInterval, 3);

                StartCoroutine(spawnPancakes);
                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnWatermelon);
                StartCoroutine(spawnStrongCheese);
                StartCoroutine(spawnCheese);
                break;
            // 17라운드
            case 17:
                // 리스폰 시간 설정
                spaghettiSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                tofuLargeSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                pancakesSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                watermelonSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(2f);

                spawnSpaghetti = SpawnSpaghetti(1.0f, spaghettiSpawnInterval, 6);
                spawnTofuLarge = SpawnTofuLarge(10.0f, tofuLargeSpawnInterval, 2);
                spawnPancakes = SpawnPancakes(25.0f, pancakesSpawnInterval, 4);
                spawnWatermelon = SpawnWatermelon(30.0f, watermelonSpawnInterval, 5);
                spawnSandwich = SpawnSandwich(40.0f, sandwichSpawnInterval, 3);

                StartCoroutine(spawnSpaghetti);
                StartCoroutine(spawnTofuLarge);
                StartCoroutine(spawnPancakes);
                StartCoroutine(spawnWatermelon);
                StartCoroutine(spawnSandwich);
                break;
            // 18라운드
            case 18:
                // 리스폰 시간 설정
                blueberrySpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                pancakesSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                tofuLargeSpawnInterval = ActivateItemRelatedSpawnInterval(3f);
                saladSpawnInterval = ActivateItemRelatedSpawnInterval(3f);

                spawnBlueberry = SpawnBlueberry(1.0f, blueberrySpawnInterval, 10);
                spawnPancakes = SpawnPancakes(4.0f, pancakesSpawnInterval, 5);
                spawnTofuLarge = SpawnTofuLarge(25.0f, tofuLargeSpawnInterval, 3);
                spawnSalad = SpawnSalad(45.0f, saladSpawnInterval, 3);

                StartCoroutine(spawnBlueberry);
                StartCoroutine(spawnPancakes);
                StartCoroutine(spawnTofuLarge);
                StartCoroutine(spawnSalad);
                break;
            // 19라운드
            case 19:
                // 리스폰 시간 설정
                sandwichSpawnInterval = ActivateItemRelatedSpawnInterval(1f);
                pancakesSpawnInterval = ActivateItemRelatedSpawnInterval(2f);
                strongCheeseSpawnInterval = ActivateItemRelatedSpawnInterval(5f);
                spaghettiSpawnInterval = ActivateItemRelatedSpawnInterval(5f);

                spawnSandwich = SpawnSandwich(1.0f, sandwichSpawnInterval, 6);
                spawnPancakes = SpawnPancakes(5.0f, pancakesSpawnInterval, 5);
                spawnStrongCheese = SpawnStrongCheese(15.0f, strongCheeseSpawnInterval, 3);
                spawnSpaghetti = SpawnSpaghetti(35.0f, spaghettiSpawnInterval, 3);

                StartCoroutine(spawnSandwich);
                StartCoroutine(spawnPancakes);
                StartCoroutine(spawnStrongCheese);
                StartCoroutine(spawnSpaghetti);
                break;
            case 20:

                break;
            default:
                break;
        }

        yield return null;
    }

    // 라운드 종료 시 모든 스폰 코루틴을 종료한다.
    private void StopSpawn()
    {
        Debug.Log("몬스터 스폰 중지");
        if (spawnPotato != null)
            StopCoroutine(spawnPotato);
        if (spawnSandwich != null)
            StopCoroutine(spawnSandwich);
        if (spawnWatermelon != null)
            StopCoroutine(spawnWatermelon);
        if (spawnBlueberry != null)
            StopCoroutine(spawnBlueberry);
        if (spawnSalad != null)
            StopCoroutine(spawnSalad);
        if (spawnCheese != null)
            StopCoroutine(spawnCheese);
        if (spawnStrongCheese != null)
            StopCoroutine(spawnStrongCheese);
        if (spawnSpaghetti != null)
            StopCoroutine(spawnSpaghetti);
        if (spawnPancakes != null)
            StopCoroutine(spawnPancakes);
        if (spawnTofuLarge != null)
            StopCoroutine(spawnTofuLarge);
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
        int currentRound = GameRoot.Instance.GetCurrentRound();

        switch (monsterName)
        {
            case "Potatoes":
                monsterInfo.SetMonsterHP(10f + 5f * currentRound);
                monsterInfo.SetMonsterDamage(0);
                monsterInfo.SetMonsterMovementSpeed(0);
                monsterInfo.SetMonsterWaffleDropCount(3);
                monsterInfo.SetMonsterConsumableDropRate(1f);
                monsterInfo.SetMonsterLootDropRate(0.2f);
                break;
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
            case "Blueberry":
                monsterInfo.SetMonsterHP(4f + 2.5f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 0.85f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(8f);
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.01f);
                monsterInfo.SetMonsterLootDropRate(0.01f);
                break;
            case "Salad":
                monsterInfo.SetMonsterHP(8f + 1f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 0.6f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(3.8f);
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.03f);
                monsterInfo.SetMonsterLootDropRate(0.1f);
                break;
            case "Cheese":
                monsterInfo.SetMonsterHP(20f + 11f * currentRound);
                monsterInfo.SetMonsterDamage(2f + 0.85f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(6f);
                monsterInfo.SetMonsterWaffleDropCount(3);
                monsterInfo.SetMonsterConsumableDropRate(0.03f);
                monsterInfo.SetMonsterLootDropRate(0.03f);
                break;
            case "StrongCheese":
                monsterInfo.SetMonsterHP(30f + 22f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 1.15f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(6f);
                monsterInfo.SetMonsterWaffleDropCount(3);
                monsterInfo.SetMonsterConsumableDropRate(0.03f);
                monsterInfo.SetMonsterLootDropRate(0.03f);
                break;
            case "Spaghetti":
                monsterInfo.SetMonsterHP(10f + 24f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 1.2f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(3f);
                monsterInfo.SetMonsterWaffleDropCount(3);
                monsterInfo.SetMonsterConsumableDropRate(0.03f);
                monsterInfo.SetMonsterLootDropRate(0.03f);
                break;
            case "Pancakes":
                monsterInfo.SetMonsterHP(8f + 3f * currentRound);
                monsterInfo.SetMonsterDamage(1 + 1.0f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(Random.Range(4.5f, 5.5f));
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.01f);
                monsterInfo.SetMonsterLootDropRate(0.01f);
                break;
            case "TofuLarge":
                monsterInfo.SetMonsterHP(10f + 1f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 0.85f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(2.4f);
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.01f);
                monsterInfo.SetMonsterLootDropRate(0.01f);
                break;
            case "TofuSmall":
                monsterInfo.SetMonsterHP(15f + 5f * currentRound);
                monsterInfo.SetMonsterDamage(1f + 1.0f * currentRound);
                monsterInfo.SetMonsterMovementSpeed(7f);
                monsterInfo.SetMonsterWaffleDropCount(1);
                monsterInfo.SetMonsterConsumableDropRate(0.01f);
                monsterInfo.SetMonsterLootDropRate(0.01f);
                break;
            default:
                break;
        }
    }

    // 몬스터를 워닝 사인 이후에 스폰하는 코루틴
    private IEnumerator SpawnMonster(GameObject monster, Vector2 spawnPos)
    {
        // 해당 위치에 워닝 사인을 띄우고 1초 후에 몬스터 스폰
        GameObject sign;
        // 감자 스폰이 아니면 빨간색 워닝 사인, 아니면 초록색 워닝 사인
        if (monster.name != "Potatoes")
            sign = Instantiate(warningSign, spawnPos, monster.transform.rotation);
        else
            sign = Instantiate(greenSign, spawnPos, monster.transform.rotation);

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

    // 감자 스폰
    private IEnumerator SpawnPotatoes()
    {
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            yield return new WaitForSeconds(10f);
            GameObject potatoes = Resources.Load<GameObject>("Prefabs/Monsters/Potatoes");

            float random = Random.Range(0, 100f);
            // 매 10초마다 82.5% 확률로 스폰된다
            if (random <= 82.5f)
            {
                Vector2 spawnLocation = SetSpawnPos();

                // 감자 스폰
                spawnMonster = SpawnMonster(potatoes, spawnLocation);
                StartCoroutine(spawnMonster);
            }
            yield return null;
        }
    }

    private IEnumerator SpawnSandwich(float waitToStartTime, float spawnInterval, int monsterCount)
    {
        yield return new WaitForSeconds(waitToStartTime);

        // 라운드 종료까지 반복
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = ChooseSpawnType();
            
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

        // 라운드 종료까지 반복
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = ChooseSpawnType();

            // 산개 스폰
            if (spawnType)
            {
                // 몬스터 마릿수 만큼 랜덤 장소에 스폰
                for (int i = 0; i < monsterCount; i++)
                {
                    Vector2 spawnLocation = SetSpawnPos();

                    // 몬스터 스폰
                    spawnMonster = SpawnMonster(monsterPrefabs[1], spawnLocation);
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

    private IEnumerator SpawnBlueberry(float waitToStartTime, float spawnInterval, int monsterCount)
    {
        yield return new WaitForSeconds(waitToStartTime);

        // 라운드 종료까지 반복
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = ChooseSpawnType();

            // 산개 스폰
            if (spawnType)
            {
                // 몬스터 마릿수 만큼 랜덤 장소에 스폰
                for (int i = 0; i < monsterCount; i++)
                {
                    Vector2 spawnLocation = SetSpawnPos();

                    // 몬스터 스폰
                    spawnMonster = SpawnMonster(monsterPrefabs[3], spawnLocation);
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
                    float posX = Random.Range(-2.0f, 2.0f);
                    float posY = Random.Range(-2.0f, 2.0f);
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

        // 라운드 종료까지 반복
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = true;

            // 산개 스폰
            if (spawnType)
            {
                // 몬스터 마릿수 만큼 랜덤 장소에 스폰
                for (int i = 0; i < monsterCount; i++)
                {
                    Vector2 spawnLocation = SetSpawnPos();

                    // 몬스터 스폰
                    spawnMonster = SpawnMonster(monsterPrefabs[2], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    private IEnumerator SpawnCheese(float waitToStartTime, float spawnInterval, int monsterCount)
    {
        yield return new WaitForSeconds(waitToStartTime);

        // 라운드 종료까지 반복
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = true;

            // 산개 스폰
            if (spawnType)
            {
                // 몬스터 마릿수 만큼 랜덤 장소에 스폰
                for (int i = 0; i < monsterCount; i++)
                {
                    Vector2 spawnLocation = SetSpawnPos();

                    // 몬스터 스폰
                    spawnMonster = SpawnMonster(monsterPrefabs[4], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    private IEnumerator SpawnStrongCheese(float waitToStartTime, float spawnInterval, int monsterCount)
    {
        yield return new WaitForSeconds(waitToStartTime);

        // 라운드 종료까지 반복
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = true;

            // 산개 스폰
            if (spawnType)
            {
                // 몬스터 마릿수 만큼 랜덤 장소에 스폰
                for (int i = 0; i < monsterCount; i++)
                {
                    Vector2 spawnLocation = SetSpawnPos();

                    // 몬스터 스폰
                    spawnMonster = SpawnMonster(monsterPrefabs[4], spawnLocation);
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

        // 라운드 종료까지 반복
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = true;

            // 산개 스폰
            if (spawnType)
            {
                // 몬스터 마릿수 만큼 랜덤 장소에 스폰
                for (int i = 0; i < monsterCount; i++)
                {
                    Vector2 spawnLocation = SetSpawnPos();

                    // 몬스터 스폰
                    spawnMonster = SpawnMonster(monsterPrefabs[5], spawnLocation);
                    StartCoroutine(spawnMonster);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return null;
    }

    private IEnumerator SpawnPancakes(float waitToStartTime, float spawnInterval, int monsterCount)
    {
        yield return new WaitForSeconds(waitToStartTime);

        // 라운드 종료까지 반복
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = true;

            // 산개 스폰
            if (spawnType)
            {
                // 몬스터 마릿수 만큼 랜덤 장소에 스폰
                for (int i = 0; i < monsterCount; i++)
                {
                    Vector2 spawnLocation = SetSpawnPos();

                    // 몬스터 스폰
                    spawnMonster = SpawnMonster(monsterPrefabs[6], spawnLocation);
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

        // 라운드 종료까지 반복
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            bool spawnType = true;

            // 산개 스폰
            if (spawnType)
            {
                // 몬스터 마릿수 만큼 랜덤 장소에 스폰
                for (int i = 0; i < monsterCount; i++)
                {
                    Vector2 spawnLocation = SetSpawnPos();

                    // 몬스터 스폰
                    spawnMonster = SpawnMonster(monsterPrefabs[7], spawnLocation);
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

        // 몬스터 마릿수 만큼 랜덤 장소에 스폰
        for (int i = 0; i < 3; i++)
        {
            Vector2 errorPos = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            Vector2 spawnLocation = position + errorPos;

            Debug.Log("Spawn");

            // 몬스터 스폰
            spawnMonster = SpawnMonster(tofuSmall, spawnLocation);
            StartCoroutine(spawnMonster);
            yield return null;
        }
    }

    private float ActivateItemRelatedSpawnInterval(float spawnInterval)
    {
        float tmp = spawnInterval;
        tmp = ActivateNormalItem40(tmp);
        tmp = ActivateRareItem37(tmp);
        tmp = ActivateEpicItem28(tmp);

        return tmp;
    }

    // NormalItem40 보유 시 적 출현 빈도 +5%
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

    // RareItem37 보유 시 적 출현 빈도 -5%
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

    // EpicItem28 보유 시 적 출현 빈도 +10%
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
