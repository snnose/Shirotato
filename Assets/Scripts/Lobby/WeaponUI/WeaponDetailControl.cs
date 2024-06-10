using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponDetailControl : MonoBehaviour
{
    private Image weaponImage;
    private TextMeshProUGUI weaponNamePro;
    private TextMeshProUGUI weaponDetailPro;

    private void Awake()
    {
        weaponImage = this.transform.GetChild(1).GetChild(1).GetComponent<Image>();
        weaponNamePro = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        weaponDetailPro = this.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RenewWeaponInfo(string weaponName)
    {
        switch (weaponName)
        {
            case "����":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/����");
                weaponNamePro.text = "����";
                weaponDetailPro.text = "����� : " + 12 + " (+100%)\n" +
                                       "���ݼӵ� : " + 0.83 + "/s \n" +
                                       "�˹� : " + 15 + '\n' +
                                       "���� : " + 7;
                break;
            case "������":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/������");
                weaponNamePro.text = "������";
                weaponDetailPro.text = "����� : " + 15 + " (+100%)\n" +
                                       "���ݼӵ� : " + 2.32 + "/s \n" +
                                       "�˹� : " + 15 + '\n' +
                                       "���� : " + 7.7 + '\n' +
                                       "6�� ��� �� 2.15�� ���� ������";
                break;
            case "����ӽŰ�":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/����ӽŰ�");
                weaponNamePro.text = "����ӽŰ�";
                weaponDetailPro.text = "����� : " + 3 + " (+50%)\n" +
                                       "���ݼӵ� : " + 5.88 + "/s \n" +
                                       "�˹� : " + 0 + '\n' +
                                       "���� : " + 7 + '\n';
                break;
            case "��ź��":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/��ź��");
                weaponNamePro.text = "��ź��";
                weaponDetailPro.text = "����� : " + 3 + " (+80%)\n" +
                                       "���ݼӵ� : " + 0.73 + "/s \n" +
                                       "�˹� : " + 8 + '\n' +
                                       "���� : " + 6.2 + '\n' +
                                       "�ѹ��� 4�� �߻�";
                break;

            case "Ȱ":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Ȱ");
                weaponNamePro.text = "Ȱ";
                weaponDetailPro.text = "����� : " + 10 + " (+80%)\n" +
                                       "���ݼӵ� : " + 0.82 + "/s \n" +
                                       "�˹� : " + 5 + '\n' +
                                       "���� : " + 6 + '\n' +
                                       "ȭ���� 1ȸ ƨ��ϴ�";
                break;
            case "������":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/������");
                weaponNamePro.text = "������";
                weaponDetailPro.text = "����� : " + 7 + " (+58%)\n" +
                                       "���ݼӵ� : " + 1.14 + "/s \n" +
                                       "�˹� : " + 0 + '\n' +
                                       "���� : " + 7 + '\n' +
                                       "ġ��Ÿ �߻� �� 1ȸ ƨ��ϴ�";
                break;
            // Melee Weapon
            case "����̼�":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/����̼�");
                weaponNamePro.text = "����̼�";
                weaponDetailPro.text = "����� : " + 8 + " (+200%)\n" +
                                       "���ݼӵ� : " + 1.28 + "/s \n" +
                                       "�˹� : " + 15 + '\n' +
                                       "���� : " + 3.8 + '\n';
                break;
            case "��ġ":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/��ġ");
                weaponNamePro.text = "��ġ";
                weaponDetailPro.text = "����� : " + 15 + " (+250%)\n" +
                                       "���ݼӵ� : " + 0.57 + "/s \n" +
                                       "�˹� : " + 20 + '\n' +
                                       "���� : " + 4.2 + '\n';
                break;
            case "��":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/��");
                weaponNamePro.text = "��";
                weaponDetailPro.text = "����� : " + 1 + " (+100%)\n" +
                                       "���ݼӵ� : " + 0.99 + "/s \n" +
                                       "�˹� : " + 30 + '\n' +
                                       "���� : " + 3 + '\n' +
                                       "�� ���� �� 3% Ȯ���� ���� ���";
                break;
            case "�����":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/�����");
                weaponNamePro.text = "�����";
                weaponDetailPro.text = "����� : " + 10 + " (+100%)\n" +
                                       "���ݼӵ� : " + 0.70 + "/s \n" +
                                       "�˹� : " + 10 + '\n' +
                                       "���� : " + 3.5 + '\n' +
                                       "������ 75%��ŭ �߰� �����";
                break;
            case "����Į":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/����Į");
                weaponNamePro.text = "����Į";
                weaponDetailPro.text = "����� : " + 8 + " (+200%)\n" +
                                       "���ݼӵ� : " + 0.8 + "/s \n" +
                                       "�˹� : " + 0 + '\n' +
                                       "���� : " + 3.5 + '\n' +
                                       "���� Ƚ�� �����ϸ� �η����ϴ�";
                break;
            case "ȭ���Ϲ���":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/ȭ���Ϲ���");
                weaponNamePro.text = "ȭ���Ϲ���";
                weaponDetailPro.text = "����� : " + 20 + " (+200%)\n" +
                                       "���ݼӵ� : " + 0.69 + "/s \n" +
                                       "�˹� : " + 0 + '\n' +
                                       "���� : " + 4 + '\n' +
                                       "�ֵθ���� ��⸦ �ݺ��մϴ�";
                break;
            default:
                break;
        }
    }
}
