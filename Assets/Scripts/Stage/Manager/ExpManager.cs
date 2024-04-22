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
            // ���� �� ���� ���
            roundSoundManager.PlayLevelUpSound();
            // ���� �� UI Ȱ��ȭ
            LevelUpUIControl.Instance.SetActive(true);

            // ���� ����ġ�� �䱸 ����ġ ��ŭ �����Ѵ�
            currentExp -= demandExp;
            // ���� ������ + 1
            currentLevel++;
            // �÷��̾��� ü���� 1 ��½�Ų��.
            PlayerInfo.Instance.SetHP(PlayerInfo.Instance.GetHP() + 1);
            RealtimeInfoManager.Instance.SetCurrentHP(RealtimeInfoManager.Instance.GetCurrentHP() + 1);
            RealtimeInfoManager.Instance.SetHP(RealtimeInfoManager.Instance.GetHP() + 1);
            // �䱸 ����ġ�� �����Ѵ�
            demandExp = calDemandExp(currentLevel);
            // ���׷��̵� ���� Ƚ�� + 1
            int levelUpCount = GameRoot.Instance.GetLevelUpCount();
            GameRoot.Instance.SetLevelUpCount(++levelUpCount);

            // �� ���� ���� ����ġ�� ���� ��츦 ����� �ڷ�ƾ ����
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
