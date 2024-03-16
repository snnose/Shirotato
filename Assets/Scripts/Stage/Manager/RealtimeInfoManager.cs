using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ���� ���� �� ������ ����Ǵ� �÷��̾��� ����
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

    // ���� ����
    private float DMGPercent = 0f;
    private float ATKSpeed = 0f;
    private float FixedDMG = 0f;
    private float Critical = 0f;
    private float Range = 0f;

    // ��� ����
    private float HP = 0f;
    private int Recovery = 0;
    private float HPDrain = 0f;
    private int Armor = 0;
    private int Evasion = 0;

    // ��ƿ ����
    private float MovementSpeed = 0f;
    private float MovementSpeedPercent = 0f;
    private float RootingRange = 0f;
    private float Luck = 0f;
    private float Harvest = 0f;
    private float expGain = 0f;

    // ���� �� ������ ����
    private float currentHP = 1f;

    // �ڷ�ƾ
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

    // �÷��̾ Recovery �ɷ�ġ�� ����� ȸ���Ѵ�
    public IEnumerator HPRecovery()
    {
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            float coolDown = 10f;

            if (this.Recovery == 0f)
                yield return new WaitForSeconds(coolDown);

            // ȸ�� �ɷ�ġ�� 0���� Ŭ �� (0�̸� ȸ������ �ʴ´�)
            if (this.Recovery > 0f)
            {
                // (10 / ȸ��)�� ���� HP 1�� ȸ���Ѵ�
                coolDown = 10f / Recovery;
                yield return new WaitForSeconds(coolDown);

                // �ִ� ü�� �̻����� ȸ���� �� ����
                if (this.currentHP < this.HP)
                {
                    this.currentHP += 1;
                    // HP ȸ�� �ؽ�Ʈ ���
                    PrintText(1);
                }
                yield return null;
            }

            // ȸ�� �ɷ�ġ�� -5���϶��
            if (this.Recovery <= -5f)
            {
                // 2�ʸ��� HP�� 1�� �Ҹ�ȴ�
                coolDown = 10f / 5f;
                yield return new WaitForSeconds(coolDown);

                this.currentHP -= 1;
                // HP ���� �ؽ�Ʈ ���
                PrintText(-1);
                if (this.currentHP <= 0)
                    this.currentHP = 0;

                yield return null;
            }
        }
    }

    void PrintText(int num)
    {
        // ���� ����� �ؽ�Ʈ ���
        GameObject damageText = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro damagePro = damageText.GetComponent<TextMeshPro>();

        Color color = Color.white;

        // �ؽ�Ʈ �� ���� ����
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
