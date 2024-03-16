using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 라운드 진행 중 실제로 적용되는 플레이어의 스탯
public class RealtimeInfoManager : MonoBehaviour
{
    // singleton
    private static RealtimeInfoManager instance;
    public static RealtimeInfoManager Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    // 공격 관련
    private float DMGPercent = 0f;
    private float ATKSpeed = 0f;
    private float FixedDMG = 0f;
    private float Critical = 0f;
    private float Range = 0f;

    // 방어 관련
    private float HP = 0f;
    private int Recovery = 0;
    private float HPDrain = 0f;
    private int Armor = 0;
    private int Evasion = 0;

    // 유틸 관련
    private float MovementSpeed = 0f;
    private float MovementSpeedPercent = 0f;
    private float RootingRange = 0f;
    private float Luck = 0f;
    private float Harvest = 0f;
    private float expGain = 0f;

    // 라운드 중 관리할 스탯
    private float currentHP = 1f;

    // 코루틴
    public IEnumerator hpRecovery;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        SetAllStatus(PlayerInfo.Instance);
        currentHP = HP;
        //Harvest = 100f;
        StartCoroutine(HPRecovery());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameRoot.Instance.GetIsRoundClear())
        {
            StopCoroutine(HPRecovery());
        }
    }

    // 플레이어가 Recovery 능력치에 비례해 회복한다
    public IEnumerator HPRecovery()
    {
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            float coolDown = 10f;

            if (this.Recovery == 0f)
                yield return new WaitForSeconds(coolDown);

            // 회복 능력치가 0보다 클 때 (0이면 회복되지 않는다)
            if (this.Recovery > 0f)
            {
                // (10 / 회복)초 마다 HP 1씩 회복한다
                coolDown = 10f / Recovery;
                yield return new WaitForSeconds(coolDown);

                // 최대 체력 이상으로 회복할 수 없다
                if (this.currentHP < this.HP)
                {
                    this.currentHP += 1;
                    // HP 회복 텍스트 출력
                    PrintText(1);
                }
                yield return null;
            }

            // 회복 능력치가 -5이하라면
            if (this.Recovery <= -5f)
            {
                // 2초마다 HP가 1씩 소모된다
                coolDown = 10f / 5f;
                yield return new WaitForSeconds(coolDown);

                this.currentHP -= 1;
                // HP 감소 텍스트 출력
                PrintText(-1);
                if (this.currentHP <= 0)
                    this.currentHP = 0;

                yield return null;
            }
        }
    }

    void PrintText(int num)
    {
        // 받은 대미지 텍스트 출력
        GameObject damageText = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro damagePro = damageText.GetComponent<TextMeshPro>();

        Color color = Color.white;

        // 텍스트 및 색상 결정
        if (num > 0)
        {
            damagePro.text = "+" + num;
            ColorUtility.TryParseHtmlString("#1FDE38", out color);
        }
        else
        {
            damagePro.text = num.ToString();
            color = Color.red;
        }

        damagePro.color = color;
        GameObject copy = Instantiate(damageText);
        Vector3 randomPos = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = PlayerControl.Instance.gameObject.transform.position + randomPos;
    }

    public void SetCurrentHP(float currentHP)
    {
        this.currentHP = currentHP;
    }

    public void SetAllStatus(PlayerInfo playerInfo)
    {
        this.DMGPercent = playerInfo.GetDMGPercent();
        this.ATKSpeed = playerInfo.GetATKSpeed();
        this.FixedDMG = playerInfo.GetFixedDMG();
        this.Critical = playerInfo.GetCritical();
        this.Range = playerInfo.GetRange();

        this.HP = playerInfo.GetHP();
        this.Recovery = playerInfo.GetRecovery();
        this.Armor = playerInfo.GetArmor();
        this.Evasion = playerInfo.GetEvasion();

        this.MovementSpeed = playerInfo.GetMovementSpeed();
        this.MovementSpeedPercent = playerInfo.GetMovementSpeedPercent();
        this.RootingRange = playerInfo.GetRootingRange();
        this.Luck = playerInfo.GetLuck();
        this.expGain = playerInfo.GetExpGain();

        this.currentHP = this.HP;
    }

    public void SetDMGPercent(float dmgPercent)
    {
        this.DMGPercent = dmgPercent;
    }

    public void SetATKSpeed(float ATKSpeed)
    {
        this.ATKSpeed = ATKSpeed;
    }

    public void SetFixedDMG(float fixedDamage)
    {
        this.FixedDMG = fixedDamage;
    }

    public void SetCritical(float critical)
    {
        this.Critical = critical;
    }

    public void SetRange(float range)
    {
        this.Range = range;
    }

    public void SetHP(float HP)
    {
        this.HP = HP;
    }

    public void SetRecovery(int recovery)
    {
        this.Recovery = recovery;
    }

    public void SetHPDrain(float hpDrain)
    {
        this.HPDrain = hpDrain;
    }

    public void SetArmor(int armor)
    {
        this.Armor = armor;
    }

    public void SetEvasion(int evasion)
    {
        this.Evasion = evasion;
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        this.MovementSpeed = movementSpeed;
    }

    public void SetMovementSpeedPercent(float movementSpeedPercent)
    {
        this.MovementSpeedPercent = movementSpeedPercent;
    }

    public void SetRootingRange(float rootingRange)
    {
        this.RootingRange = rootingRange;
    }

    public void SetLuck(float luck)
    {
        this.Luck = luck;
    }

    public void SetHarvest(float harvest)
    {
        this.Harvest = harvest;
    }

    public void SetExpGain(float expGain)
    {
        this.expGain = expGain;
    }

    public float GetCurrentHP()
    {
        return this.currentHP;
    }

    public float GetDMGPercent()
    {
        return this.DMGPercent;
    }

    public float GetATKSpeed()
    {
        return this.ATKSpeed;
    }

    public float GetFixedDMG()
    {
        return this.FixedDMG;
    }

    public float GetRange()
    {
        return this.Range;
    }

    public float GetCritical()
    {
        return this.Critical;
    }

    public float GetHP()
    {
        return this.HP;
    }

    public int GetRecovery()
    {
        return this.Recovery;
    }

    public float GetHPDrain()
    {
        return this.HPDrain;
    }

    public int GetArmor()
    {
        return this.Armor;
    }

    public int GetEvasion()
    {
        return this.Evasion;
    }

    public float GetMovementSpeed()
    {
        return this.MovementSpeed;
    }

    public float GetMovementSpeedPercent()
    {
        return this.MovementSpeedPercent;
    }

    public float GetRootingRange()
    {
        return this.RootingRange;
    }

    public float GetLuck()
    {
        return this.Luck;
    }

    public float GetHarvest()
    {
        return this.Harvest;
    }

    public float GetExpGain()
    {
        return this.expGain;
    }
}
