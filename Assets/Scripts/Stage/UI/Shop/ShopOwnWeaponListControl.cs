using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopOwnWeaponListControl : MonoBehaviour
{
    private GameObject ownWeaponListContent;

    public IEnumerator renewOwnWeaponList;

    private void Awake()
    {
        ownWeaponListContent = this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        renewOwnWeaponList = RenewOwnWeaponList();
    }

    // Update is called once per frame
    void Update()
    {
        if (renewOwnWeaponList != null)
        {
            StartCoroutine(renewOwnWeaponList);
        }
    }

    public IEnumerator RenewOwnWeaponList()
    {
        // ���� ���� ���� ����� �����´�.
        List<GameObject> currWeaponList = WeaponManager.Instance.GetCurrentWeaponList();

        // UI ����
        int num = currWeaponList.Count;
        for (int i = 0; i < 6; i++)
        {
            int r = i / 3;
            int c = i % 3;

            GameObject weaponRoom =
                    ownWeaponListContent.transform.GetChild(r).GetChild(c).gameObject;
            // i�� ���� ���� ������ �۴ٸ� ���� ���� �Է� �� ����
            if (i < num)
            {
                // ���� ĭ�� ��� �̹���(���)�� �����Ѵ�.
                weaponRoom.transform.GetChild(0).gameObject.GetComponent<Image>().color =
                    DecideGradeColor(currWeaponList[i].GetComponent<WeaponInfo>().GetWeaponGrade());
                // ���� ĭ�� �̹���(����)�� �����Ѵ�.
                weaponRoom.transform.GetChild(1).gameObject.GetComponent<Image>().sprite =
                    currWeaponList[i].GetComponent<SpriteRenderer>().sprite;
                
                // ���� ĭ�� �ش� ������ ������ ������ �Ѵ�.
                if (!weaponRoom.TryGetComponent<WeaponInfo>(out WeaponInfo weaponInfo))
                {
                    weaponRoom.AddComponent<WeaponInfo>();
                    weaponRoom.GetComponent<WeaponInfo>().SetWeaponStatus(currWeaponList[i]);
                }
            }
            // �� �ܴ� ��ĭ���� ó���Ѵ�.
            else
            {
                // ���� ĭ�� ��� �̹����� ������� �����Ѵ�.
                weaponRoom.transform.GetChild(0).gameObject.GetComponent<Image>().color =
                    DecideGradeColor(0);
                // ���� ĭ�� �̹����� ����.
                weaponRoom.transform.GetChild(1).gameObject.GetComponent<Image>().sprite =
                    null;
                // ������ ������ �����ִٸ� ����
                if (weaponRoom.TryGetComponent<WeaponInfo>(out WeaponInfo weaponInfo))
                {
                    Destroy(weaponRoom.GetComponent<WeaponInfo>());
                }
            }
        }
        
        yield return null;
    }

    // rank�� ���� ��ũ ������ ��ȯ�ϴ� �Լ� (��, ��, ��, ��)
    private Color DecideGradeColor(int grade)
    {
        Color color = Color.white;
        switch (grade)
        {
            case 0:
                color = Color.white;
                break;
            case 1:
                color = new Color(120, 166, 214);
                ColorUtility.TryParseHtmlString("#78A6D6", out color);
                break;
            case 2:
                color = new Color(161, 120, 214);
                break;
            case 3:
                color = new Color(233, 137, 76);
                break;
            default:
                color = Color.black;
                break;
        }

        return color;
    }
}
