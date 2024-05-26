using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    private Image HPBar;
    private MonsterControl monsterControl;

    private float maxHP;

    private void Awake()
    {
        HPBar = this.transform.GetChild(1).GetComponent<Image>();
        monsterControl = this.transform.parent.parent.GetComponent<MonsterControl>();
    }

    void Start()
    {
        maxHP = monsterControl.GetMonsterCurrentHP();
    }

    void Update()
    {
        Vector2 bossHPBarPos =
            Camera.main.WorldToScreenPoint(
                new Vector2(monsterControl.gameObject.transform.position.x, monsterControl.gameObject.transform.position.y + 1.0f));

        this.transform.position = bossHPBarPos;

        ChangeHPGageAmount(monsterControl.GetMonsterCurrentHP() / maxHP);

        if (GameRoot.Instance.GetIsRoundClear() || GameRoot.Instance.GetIsGameOver())
            Destroy(this.gameObject);
    }

    // 현재 체력 비율에 따라 체력 막대 조정
    private void ChangeHPGageAmount(float amount)
    {
        if (amount <= 0f)
            amount = 0f;
        else if (amount >= 1f)
            amount = 1f;

        HPBar.fillAmount = amount;
    }
}
