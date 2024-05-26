using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameResultUIControl : MonoBehaviour
{
    // singleton
    private static GameResultUIControl instance;
    public static GameResultUIControl Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private TextMeshProUGUI title;
    private ShopOwnItemListControl shopOwnItemListControl;
    private ShopOwnWeaponListControl shopOwnWeaponListControl;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        title = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        shopOwnWeaponListControl = this.transform.GetChild(4).GetComponent<ShopOwnWeaponListControl>();
        shopOwnItemListControl = this.transform.GetChild(5).GetComponent<ShopOwnItemListControl>();

        SetActive(false);
    }

    void Start()
    {
        
    }

    public void SetActive(bool ret)
    {
        if (ret)
        {
            this.gameObject.SetActive(ret);
            this.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

            // ���� ����, ������ ����Ʈ ����
            StartCoroutine(shopOwnItemListControl.RenewOwnItemList());
            shopOwnWeaponListControl.renewOwnWeaponList = shopOwnWeaponListControl.RenewOwnWeaponList();

            // ���� �¸�, �й迡 ���� ��µǴ� �ؽ�Ʈ�� ����ȴ�
            SetTitleText(GameRoot.Instance.GetIsGameOver());
        }
        else
        {
            this.transform.position = new Vector2(Screen.width, 0);
            this.gameObject.SetActive(ret);
        }
    }

    private void SetTitleText(bool isGameOver)
    {
        string difficultyName = "";

        switch (RoundSetting.Instance.GetDifficulty())
        {
            case 0:
                difficultyName = "����";
                break;
            case 1:
                difficultyName = "����";
                break;
            case 2:
                difficultyName = "�����";
                break;
            case 3:
                difficultyName = "�ſ� �����";
                break;
            case 4:
                difficultyName = "����";
                break;
            default:
                break;
        }

        // ���� �й� ��
        if (isGameOver)
            title.text = "���� �й� (" + difficultyName + ", " + GameRoot.Instance.GetCurrentRound() + ")";
        // ���� �¸� ��
        else
            title.text = "���� �¸� (" + difficultyName + ")";
    }
}
