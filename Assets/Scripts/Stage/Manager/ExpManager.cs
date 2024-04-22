using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    private static ExpManager instance;
    public static ExpManager Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private RoundSoundManager roundSoundManager;

    private int currentLevel = 0;

    private float currentExp;
    private float demandExp;

    public IEnumerator levelUp;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        roundSoundManager = GameObject.FindGameObjectWithTag("AudioManager").transform.GetChild(1).
                            GetComponent<RoundSoundManager>();

        levelUp = LevelUp();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentExp = 0;
        demandExp = calDemandExp(currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (levelUp != null)
        {
            StartCoroutine(levelUp);
        }
    }

    public IEnumerator LevelUp()
    {
        if (currentExp >= demandExp)
        {
            // 레벨 업 사운드 출력
            roundSoundManager.PlayLevelUpSound();
            // 레벨 업 UI 활성화
            LevelUpUIControl.Instance.SetActive(true);

            // 현재 경험치를 요구 경험치 만큼 차감한다
            currentExp -= demandExp;
            // 현재 레벨을 + 1
            currentLevel++;
            // 플레이어의 체력을 1 상승시킨다.
            PlayerInfo.Instance.SetHP(PlayerInfo.Instance.GetHP() + 1);
            RealtimeInfoManager.Instance.SetCurrentHP(RealtimeInfoManager.Instance.GetCurrentHP() + 1);
            RealtimeInfoManager.Instance.SetHP(RealtimeInfoManager.Instance.GetHP() + 1);
            // 요구 경험치를 갱신한다
            demandExp = calDemandExp(currentLevel);
            // 업그레이드 가능 횟수 + 1
            int levelUpCount = GameRoot.Instance.GetLevelUpCount();
            GameRoot.Instance.SetLevelUpCount(++levelUpCount);

            // 한 번에 많은 경험치를 얻을 경우를 대비해 코루틴 장전
            levelUp = LevelUp();
        }
        yield return null;
    }

    private float calDemandExp(int currentLevel)
    {
        return 1f * (currentLevel + 4) * (currentLevel + 4); 
    }

    public void SetCurrentExp(float currentExp)
    {
        this.currentExp = currentExp;
    }

    public int GetCurrentLevel()
    {
        return this.currentLevel;
    }

    public float GetCurrentExp()
    {
        return this.currentExp;
    }

    public float GetDemandExp()
    {
        return this.demandExp;
    }
}
