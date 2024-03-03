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
            // ���� ����ġ�� �䱸 ����ġ ��ŭ �����Ѵ�
            currentExp -= demandExp;
            // ���� ������ + 1
            currentLevel++;
            // �䱸 ����ġ�� �����Ѵ�
            demandExp = calDemandExp(currentLevel);
            // ���׷��̵� ���� Ƚ�� + 1
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
