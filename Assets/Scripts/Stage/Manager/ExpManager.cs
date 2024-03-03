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
            // 현재 경험치를 요구 경험치 만큼 차감한다
            currentExp -= demandExp;
            // 현재 레벨을 + 1
            currentLevel++;
            // 요구 경험치를 갱신한다
            demandExp = calDemandExp(currentLevel);
            // 업그레이드 가능 횟수 + 1
        }
        yield return null;
    }

    private float calDemandExp(int currentLevel)
    {
        return 1.54f * (currentLevel + 4) * (currentLevel + 4); 
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
