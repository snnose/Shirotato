using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IllustGuideWeaponButton : MonoBehaviour, IPointerEnterHandler
{
    Button weaponButton;
    public int rarity = 0;

    private void Awake()
    {
        weaponButton = this.GetComponent<Button>();
        weaponButton.onClick.AddListener(OnClickWeaponButton);
    }

    void Start()
    {
        rarity = DecideRarity(this.transform.parent.parent.parent.parent.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ������ ���� ���� ���
        ButtonSoundManager.Instance.PlayOnPointerEnterSound2();
    }

    void OnClickWeaponButton()
    {
        // ��ư Ŭ�� ���� ���
        ButtonSoundManager.Instance.PlayOnClickButtonSound2();

        // ���õ� �������� ���, �̹���, �̸�, �������� ��ü
        IllustGuideSelectedDetail.Instance.selectedImage.transform.GetChild(0).GetComponent<Image>().color =
            this.transform.GetChild(0).GetComponent<Image>().color;

        IllustGuideSelectedDetail.Instance.selectedImage.transform.GetChild(1).GetComponent<Image>().sprite =
            this.transform.GetChild(1).GetComponent<Image>().sprite;

        IllustGuideSelectedDetail.Instance.selectedNameText.text = this.transform.GetChild(1).GetComponent<Image>().sprite.name;

        IllustGuideSelectedDetail.Instance.selectedDetailText.text = 
        SetWeaponDetailText(this.transform.GetChild(1).GetComponent<Image>().sprite.name, rarity);

        string comment = GetWeaponComment(this.transform.GetChild(1).GetComponent<Image>().sprite.name);

        IllustGuideComment.Instance.SetCommentText(comment);
    }

    // ���� �̸��� ����� ���� ��µǴ� ���� ���� ����
    string SetWeaponDetailText(string weaponName, int rarity)
    {
        string finalText = "";

        switch (weaponName)
        {
            // ����
            case "����":
                switch (rarity)
                {
                    case 0:
                        finalText += "����� : 12 (+100%)\n" +
                                     "���ݼӵ� : 0.83/s\n" +
                                     "�˹� : 15\n" +
                                     "���� : 7";
                        
                        //this.price = 10;
                        break;
                    case 1:
                        finalText += "����� : 20 (+100%)\n" +
                                     "���ݼӵ� : 0.89/s\n" +
                                     "�˹� : 15\n" +
                                     "���� : 7";
                        
                        //this.price = 22;
                        break;
                    case 2:
                        finalText += "����� : 30 (+100%)\n" +
                                     "���ݼӵ� : 0.97/s\n" +
                                     "�˹� : 15\n" +
                                     "���� : 7";

                        //this.price = 45;
                        break;
                    case 3:
                        finalText += "����� : 50 (+100%)\n" +
                                     "���ݼӵ� : 1.14/s\n" +
                                     "�˹� : 15\n" +
                                     "���� : 7";

                        //this.price = 91;
                        break;
                    default:
                        break;
                }

                break;
            // ������
            case "������":
                switch (rarity)
                {
                    case 0:
                        finalText += "����� : 15 (+100%)\n" +
                                     "���ݼӵ� : 2.32/s\n" +
                                     "�˹� : 15\n" +
                                     "���� : 7.7\n" +
                                     "6�� ��� �� 2.15�� �� ������";
                        
                        //this.price = 20;
                        break;
                    case 1:
                        finalText += "����� : 20 (+130%)\n" +
                                     "���ݼӵ� : 2.38/s\n" +
                                     "�˹� : 15\n" +
                                     "���� : 7.7\n" +
                                     "6�� ��� �� 2.1�� �� ������";
                        
                        //this.price = 34;
                        break;
                    case 2:
                        finalText += "����� : 25 (+165%)\n" +
                                     "���ݼӵ� : 2.5/s\n" +
                                     "�˹� : 15\n" +
                                     "���� : 7.7\n" +
                                     "6�� ��� �� 2�� �� ������";
                        
                        //this.price = 70;
                        break;
                    case 3:
                        finalText += "����� : 40 (+200%)\n" +
                                     "���ݼӵ� : 2.63/s\n" +
                                     "�˹� : 15\n" +
                                     "���� : 7.7\n" +
                                     "6�� ��� �� 1.9�� �� ������";
                        
                        //this.price = 130;
                        break;
                    default:
                        break;
                }

                break;

            case "����ӽŰ�":
                switch (rarity)
                {
                    case 0:
                        finalText += "����� : 3 (+50%)\n" +
                                     "���ݼӵ� : 5.88/s\n" +
                                     "�˹� : 0\n" +
                                     "���� : 7\n";
                        
                        //this.price = 20;
                        break;
                    case 1:
                        finalText += "����� : 4 (+60%)\n" +
                                     "���ݼӵ� : 5.88/s\n" +
                                     "�˹� : 0\n" +
                                     "���� : 7\n";
                        
                        //this.price = 39;
                        break;
                    case 2:
                        finalText += "����� : 5 (+70%)\n" +
                                     "���ݼӵ� : 5.88/s\n" +
                                     "�˹� : 0\n" +
                                     "���� : 7\n";
                        
                        //this.price = 74;
                        break;
                    case 3:
                        finalText += "����� : 8 (+80%)\n" +
                                     "���ݼӵ� : 6.66/s\n" +
                                     "�˹� : 0\n" +
                                     "���� : 7\n";
                        
                        //this.price = 149;
                        break;
                }
                break;

            case "��ź��":
                switch (rarity)
                {
                    case 0:
                        finalText += "����� : 3 (+80%)\n" +
                                     "���ݼӵ� : 0.72/s\n" +
                                     "�˹� : 8\n" +
                                     "���� : 6.2\n" +
                                     "�ѹ��� 4�� �߻�";
                        
                        //this.price = 20;
                        break;
                    case 1:
                        finalText += "����� : 6 (+85%)\n" +
                                     "���ݼӵ� : 0.78/s\n" +
                                     "�˹� : 8\n" +
                                     "���� : 6.2\n" +
                                     "�ѹ��� 4�� �߻�";
                        
                        //this.price = 39;
                        break;
                    case 2:
                        finalText += "����� : 9 (+90%)\n" +
                                     "���ݼӵ� : 0.83/s\n" +
                                     "�˹� : 8\n" +
                                     "���� : 6.2\n" +
                                     "�ѹ��� 4�� �߻�";
                        
                        //this.price = 74;
                        break;
                    case 3:
                        finalText += "����� : 9 (+100%)\n" +
                                     "���ݼӵ� : 0.83/s\n" +
                                     "�˹� : 8\n" +
                                     "���� : 6.2\n" +
                                     "�ѹ��� 6�� �߻�";
                        
                        //this.price = 149;
                        break;
                }
                break;

            case "Ȱ":
                switch (rarity)
                {
                    case 0:
                        finalText += "����� : 10 (+80%)\n" +
                                     "���ݼӵ� : 0.81/s\n" +
                                     "�˹� : 5\n" +
                                     "���� : 6\n" +
                                     "ȭ���� 1ȸ ƨ��ϴ�";
                        
                        //this.price = 15;
                        break;
                    case 1:
                        finalText += "����� : 13 (+80%)\n" +
                                     "���ݼӵ� : 0.85/s\n" +
                                     "�˹� : 5\n" +
                                     "���� : 6\n" +
                                     "ȭ���� 2ȸ ƨ��ϴ�";
                        
                        //this.price = 31;
                        break;
                    case 2:
                        finalText += "����� : 16 (+80%)\n" +
                                     "���ݼӵ� : 0.88/s\n" +
                                     "�˹� : 5\n" +
                                     "���� : 6\n" +
                                     "ȭ���� 3ȸ ƨ��ϴ�";
                        
                        //this.price = 61;
                        break;
                    case 3:
                        finalText += "����� : 20 (+80%)\n" +
                                     "���ݼӵ� : 0.90/s\n" +
                                     "�˹� : 5\n" +
                                     "���� : 6\n" +
                                     "ȭ���� 4ȸ ƨ��ϴ�";
                        
                        //this.price = 122;
                        break;
                }
                break;
            case "������":
                switch (rarity)
                {
                    case 0:
                        finalText += "����� : 7 (+59%)\n" +
                                     "���ݼӵ� : 1.14/s\n" +
                                     "�˹� : 0\n" +
                                     "���� : 7\n" +
                                     "ġ��Ÿ �߻� �� 1ȸ ƨ��ϴ�";

                        //this.price = 12;
                        break;
                    case 1:
                        finalText += "����� : 9 (+81%)\n" +
                                     "���ݼӵ� : 1.20/s\n" +
                                     "�˹� : 0\n" +
                                     "���� : 7\n" +
                                     "ġ��Ÿ �߻� �� 2ȸ ƨ��ϴ�";

                        //this.price = 26;
                        break;
                    case 2:
                        finalText += "����� : 12 (+100%)\n" +
                                     "���ݼӵ� : 1.25/s\n" +
                                     "�˹� : 0\n" +
                                     "���� : 7\n" +
                                     "ġ��Ÿ �߻� �� 3ȸ ƨ��ϴ�";

                        //this.price = 52;
                        break;
                    case 3:
                        finalText += "����� : 18 (+122%)\n" +
                                     "���ݼӵ� : 1.42/s\n" +
                                     "�˹� : 0\n" +
                                     "���� : 7\n" +
                                     "ġ��Ÿ �߻� �� 4ȸ ƨ��ϴ�";

                        //this.price = 105;
                        break;
                }
                break;

            // ���Ÿ� Ư��
            case "�ű׳�":
                switch (rarity)
                {
                    case 2:
                        finalText += "����� : 116 (+450%)\n" +
                                     "���ݼӵ� : 0.66/s\n" +
                                     "�˹� : 10\n" +
                                     "���� : 10\n" +
                                     "������ ����";
                        break;
                    case 3:
                        finalText += "����� : 162 (+670%)\n" +
                                     "���ݼӵ� : 0.66/s\n" +
                                     "�˹� : 10\n" +
                                     "���� : 10\n" +
                                     "������ ����";
                        break;
                    default:
                        break;
                }
                break;

            // Melee Weapon
            case "����̼�":
                switch (rarity)
                {

                    case 0:
                        finalText += "����� : 8 (+200%)\n" +
                                     "���ݼӵ� : 1.28/s\n" +
                                     "�˹� : 15\n" +
                                     "���� : 3.8\n";
                        
                        //this.price = 10;
                        break;
                    case 1:
                        finalText += "����� : 16 (+200%)\n" +
                                     "���ݼӵ� : 1.37/s\n" +
                                     "�˹� : 15\n" +
                                     "���� : 3.8\n";
                        
                        //this.price = 22;
                        break;
                    case 2:
                        finalText += "����� : 32 (+200%)\n" +
                                     "���ݼӵ� : 1.45/s\n" +
                                     "�˹� : 15\n" +
                                     "���� : 3.8\n";
                        
                        //this.price = 45;
                        break;
                    case 3:
                        finalText += "����� : 64 (+200%)\n" +
                                     "���ݼӵ� : 1.69/s\n" +
                                     "�˹� : 15\n" +
                                     "���� : 3.8\n";
                        
                        //this.price = 91;
                        break;
                    default:
                        break;
                }
                break;

            case "��ġ":
                switch (rarity)
                {
                    case 0:
                        finalText += "����� : 15 (+250%)\n" +
                                     "���ݼӵ� : 0.57/s\n" +
                                     "�˹� : 20\n" +
                                     "���� : 4.2\n";
                        
                        //this.price = 25;
                        break;
                    case 1:
                        finalText += "����� : 30 (+300%)\n" +
                                     "���ݼӵ� : 0.60/s\n" +
                                     "�˹� : 25\n" +
                                     "���� : 4.2\n";
                        
                        //this.price = 51;
                        break;
                    case 2:
                        finalText += "����� : 60 (+350%)\n" +
                                     "���ݼӵ� : 0.62/s\n" +
                                     "�˹� : 35\n" +
                                     "���� : 4.2\n";
                        
                        //this.price = 95;
                        break;
                    case 3:
                        finalText += "����� : 100 (+400%)\n" +
                                     "���ݼӵ� : 0.66/s\n" +
                                     "�˹� : 40\n" +
                                     "���� : 4.2\n";
                        
                        //this.price = 190;
                        break;
                    default:
                        break;
                }
                break;

            case "��":
                switch (rarity)
                {
                    case 0:
                        finalText += "����� : 1 (+100%)\n" +
                                     "���ݼӵ� : 0.99/s\n" +
                                     "�˹� : 30\n" +
                                     "���� : 3\n" +
                                     "�� ���� �� 1% Ȯ���� ���� ���";

                        //this.price = 10;
                        break;
                    case 1:
                        finalText += "����� : 1 (+100%)\n" +
                                     "���ݼӵ� : 1.07/s\n" +
                                     "�˹� : 30\n" +
                                     "���� : 3\n" +
                                     "�� ���� �� 2% Ȯ���� ���� ���";

                        //this.price = 22;
                        break;
                    case 2:
                        finalText += "����� : 1 (+100%)\n" +
                                     "���ݼӵ� : 1.16/s\n" +
                                     "�˹� : 30\n" +
                                     "���� : 3\n" +
                                     "�� ���� �� 3% Ȯ���� ���� ���";

                        //this.price = 45;
                        break;
                    case 3:
                        finalText += "����� : 1 (+100%)\n" +
                                     "���ݼӵ� : 1.40/s\n" +
                                     "�˹� : 30\n" +
                                     "���� : 3\n" +
                                     "�� ���� �� 5% Ȯ���� ���� ���";

                        //this.price = 91;
                        break;
                    default:
                        break;
                }
                break;

            case "�����":
                switch (rarity)
                {
                    case 0:
                        finalText += "����� : 10 (+100%)\n" +
                                     "���ݼӵ� : 0.70/s\n" +
                                     "�˹� : 10\n" +
                                     "���� : 3.5\n" +
                                     "������ 75%��ŭ �߰� �����";

                        //this.price = 17;
                        break;
                    case 1:
                        finalText += "����� : 15 (+130%)\n" +
                                     "���ݼӵ� : 0.8/s\n" +
                                     "�˹� : 10\n" +
                                     "���� : 3.5\n" +
                                     "������ 85%��ŭ �߰� �����";

                        //this.price = 34;
                        break;
                    case 2:
                        finalText += "����� : 20 (+170%)\n" +
                                     "���ݼӵ� : 0.91/s\n" +
                                     "�˹� : 10\n" +
                                     "���� : 3.5\n" +
                                     "������ 100%��ŭ �߰� �����";

                        //this.price = 66;
                        break;
                    case 3:
                        finalText += "����� : 30 (+200%)\n" +
                                     "���ݼӵ� : 1.08/s\n" +
                                     "�˹� : 10\n" +
                                     "���� : 3.5\n" +
                                     "������ 125%��ŭ �߰� �����";

                        //this.price = 130;
                        break;
                    default:
                        break;
                }
                break;

            case "����Į":
                switch (rarity)
                {
                    case 0:
                        finalText += "����� : 8 (+200%)\n" +
                                     "���ݼӵ� : 0.80/s\n" +
                                     "�˹� : 0\n" +
                                     "���� : 3.5\n" +
                                     "���� Ƚ�� �����ϸ� �η����ϴ�";

                        //this.price = 10;
                        break;
                    case 1:
                        finalText += "����� : 16 (+225%)\n" +
                                     "���ݼӵ� : 0.85/s\n" +
                                     "�˹� : 0\n" +
                                     "���� : 3.5\n" +
                                     "���� Ƚ�� �����ϸ� �η����ϴ�";

                        //this.price = 22;
                        break;
                    case 2:
                        finalText += "����� : 24 (+250%)\n" +
                                     "���ݼӵ� : 0.90/s\n" +
                                     "�˹� : 0\n" +
                                     "���� : 3.5\n" +
                                     "���� Ƚ�� �����ϸ� �η����ϴ�";

                        //this.price = 45;
                        break;
                    case 3:
                        finalText += "����� : 40 (+300%)\n" +
                                     "���ݼӵ� : 0.97/s\n" +
                                     "�˹� : 0\n" +
                                     "���� : 3.5\n" +
                                     "���� Ƚ�� �����ϸ� �η����ϴ�";

                        //this.price = 91;
                        break;
                    default:
                        break;
                }
                break;

            case "ȭ���Ϲ���":
                switch (rarity)
                {
                    case 0:
                        finalText += "����� : 20 (+200%)\n" +
                                     "���ݼӵ� : 0.69/s\n" +
                                     "�˹� : 5\n" +
                                     "���� : 4\n" +
                                     "�ֵθ���� ��⸦ �ݺ��մϴ�";

                        //this.price = 25;
                        break;
                    case 1:
                        finalText += "����� : 25 (+200%)\n" +
                                     "���ݼӵ� : 0.78/s\n" +
                                     "�˹� : 5\n" +
                                     "���� : 4\n" +
                                     "�ֵθ���� ��⸦ �ݺ��մϴ�";

                        //this.price = 51;
                        break;
                    case 2:
                        finalText += "����� : 40 (+200%)\n" +
                                     "���ݼӵ� : 0.88/s\n" +
                                     "�˹� : 5\n" +
                                     "���� : 4\n" +
                                     "�ֵθ���� ��⸦ �ݺ��մϴ�";

                        //this.price = 95;
                        break;
                    case 3:
                        finalText += "����� : 60 (+200%)\n" +
                                     "���ݼӵ� : 1.02/s\n" +
                                     "�˹� : 5\n" +
                                     "���� : 4\n" +
                                     "�ֵθ���� ��⸦ �ݺ��մϴ�";

                        //this.price = 190;
                        break;
                    default:
                        break;
                }
                break;

            // �ٰŸ� Ư��
            case "ö���":
                switch (rarity)
                {
                    case 2:
                        finalText += "����� : 30 (+240%)\n" +
                                     "���ݼӵ� : 0.69/s\n" +
                                     "�˹� : 0\n" +
                                     "���� : 4\n" +
                                     "�ٶ��� ��ó!";
                        break;
                    case 3:
                        finalText += "����� : 55 (+270%)\n" +
                                     "���ݼӵ� : 0.76/s\n" +
                                     "�˹� : 0\n" +
                                     "���� : 4\n" +
                                     "�ٶ��� ��ó!";
                        break;
                    default:
                        break;
                }
                break;

            default:
                break;
        }

        return finalText;
    }

    // rank�� ���� ��ũ ������ ��ȯ�ϴ� �Լ� (��, ��, ��, ��)
    private int DecideRarity(string listName)
    {
        // �⺻ ���
        int rarity = 0;

        switch (listName)
        {
            case "RareWeaponList":
                rarity = 1;
                break;
            case "EpicWeaponList":
                rarity = 2;
                break;
            case "LegendWeaponList":
                rarity = 3;
                break;
            default:
                rarity = 0;
                break;
        }

        return rarity;
    }

    string GetWeaponComment(string WeaponName)
    {
        string comment = "";

        switch (WeaponName)
        {
            case "����̼�":
                comment = "\n\nǫ��ǫ�� �ɳ���ġ";
                break;

            case "����":
                comment = "";
                break;

            case "������":
                comment = "";
                break;

            case "��ġ":
                comment = "\n\n�ȸ������� �����ϴ�";
                break;

            case "�����":
                comment = "\n\n��������~";
                break;

            case "����Į":
                comment = "\n\n";
                break;

            case "��ź��":
                comment = "\n\n������";
                break;

            case "����ӽŰ�":
                comment = "";
                break;

            case "��":
                comment = "��Ÿ\n\n" +
                          "??? : ���� �÷ο�";
                break;

            case "������":
                comment = "";
                break;

            case "ȭ���Ϲ���":
                comment = "\n\n'��õ����'";
                break;

            case "Ȱ":
                comment = "\n\n�÷��� ��������";
                break;

            case "�ű׳�":
                comment = "\n\n�ſ� ������";
                break;

            case "ö���":
                comment = "\n\n���ù����մϴ�";
                break;

            default:
                break;
        }

        return comment;
    }
}
