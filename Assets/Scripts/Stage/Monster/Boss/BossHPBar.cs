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
        HPBar = this.transform.GetChild(0).GetComponent<Image>();
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
    }

    // 현재 체력 비율에 따라 체력 막대 조정
    private void ChangeHPGageAmount(float amount)
    {
        HPBar.fillAmount = amount;

        if (HPBar.fillAmount <= 0f)
            HPBar.fillAmount = 0f;

        if (HPBar.fillAmount >= 1f)
            HPBar.fillAmount = 1f;
    }
}
