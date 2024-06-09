using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IllustGuideItemButton : MonoBehaviour, IPointerEnterHandler
{
    Button itemButton;

    private void Awake()
    {
        itemButton = this.GetComponent<Button>();
        itemButton.onClick.AddListener(OnClickItemButton);
    }

    void Start()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ������ ���� ���� ���
        ButtonSoundManager.Instance.PlayOnPointerEnterSound2();
    }

    void OnClickItemButton()
    {
        // ��ư Ŭ�� ���� ���
        ButtonSoundManager.Instance.PlayOnClickButtonSound2();

        // ���õ� �������� ���, �̹���, �̸�, �������� ��ü
        IllustGuideSelectedDetail.Instance.selectedImage.transform.GetChild(0).GetComponent<Image>().color =
            this.transform.GetChild(0).GetComponent<Image>().color;

        IllustGuideSelectedDetail.Instance.selectedImage.transform.GetChild(1).GetComponent<Image>().sprite =
            this.transform.GetChild(1).GetComponent<Image>().sprite;

        IllustGuideSelectedDetail.Instance.selectedNameText.text = this.GetComponent<ItemInfo>().itemName;

        IllustGuideSelectedDetail.Instance.selectedDetailText.text = SetItemDetailText();

        string comment = GetItemComment(this.GetComponent<ItemInfo>().itemName);

        IllustGuideComment.Instance.SetCommentText(comment);
    }

    string GetItemComment(string itemName)
    {
        string comment = "";

        switch (itemName)
        {
            // �븻
            case "����":
                comment = "24.04.20 / ���ǻ�\n\n" +
                          "������ ��õ ����";
                break;

            case "�����":
                comment = "\n\n�÷ξ�~ �� �Ծ��?";
                break;

            case "�Ϳ����๰":
                comment = "24.01.15\n\n" +
                          "�ñ��ϸ� 1:41:30";
                break;

            case "������":
                comment = "\n\n??? : �̾� ����";
                break;

            case "�ܹ�":
                comment = "��Ű��� / 4:07:15\n\n" +
                          "�� ��� �ִ� �ž�~";
                break;

            case "�븥��":
                comment = "23.11.24 / ������" +
                          "\n\n�ٺ� ������";
                break;

            case "������":
                comment = "\n\n���ž��� ����� �˴ϴ�";
                break;

            case "����":
                comment = "";
                break;

            case "������":
                comment = "\n\n������ö��";
                break;

            case "���콺":
                comment = "\n\n��XX ����";
                break;

            case "����ũ":
                comment = "\n\n��Ʈ������ �ʼ�ǰ";
                break;

            case "�Ӹ���":
                comment = "\n\n����Ʈ �� �ϳ�";
                break;

            case "�޸���":
                comment = "\n\n����Ϳ� �ٿ����� �����ϴ�";
                break;

            case "��":
                comment = "\n\n�� �� ��";
                break;

            case "��":
                comment = "\n\n����";
                break;

            case "�ٿ��":
                comment = "24.04.25\n\n" +
                          "�߾��� ����";
                break;

            case "��â��":
                comment = "\n\n�� �� ���� �� �����ϴ�";
                break;

            case "���":
                comment = "23.12.12\n\n" +
                          "����� ���?";
                break;

            case "����":
                comment = "24.04.19\n\n" +
                          "�ֺ���";
                break;

            case "���ڱ�":
                comment = "\n\n�� ���εȰž�";
                break;

            case "��纣��":
                comment = "\n\n�׸����Ʈ�� ��������";
                break;

            case "��������":
                comment = "\n\n1����";
                break;

            case "����":
                comment = "\n\n���� ���� ���� ��";
                break;

            case "����":
                comment = "\n\nǲǲ�մϴ�";
                break;

            case "��Ǫ":
                comment = "\n\n";
                break;

            case "�ð�":
                comment = "23.10.04 ~\n\n" +
                          "�ֹ���";
                break;

            case "������":
                comment = "\n\n�з°�� 2��";
                break;

            case "��":
                comment = "\n\nF";
                break;

            case "��¡��":
                comment = "23.09.20\n\n" +
                          "�̸�����~~";
                break;

            case "�����":
                comment = "\n\n�÷��� ���� �� �ϳ�";
                break;

            case "������":
                comment = "\n\n�±Ⱑ �ʿ����ݾ�";
                break;

            case "�ڼ�":
                comment = "\n\n������ ���ϱ��?";
                break;

            case "�ۻ�":
                comment = "���̺� �� ���̹�\n\n" +
                          "40�۽�!";
                break;

            case "������":
                comment = "23.12.12\n\n" +
                          "��� ���⿡ �����ϴ�";
                break;

            case "����":
                comment = "23.11.13 / ���ڼ���\n\n" +
                          "??? : ���� ���� ��� ����?";
                break;

            case "��":
                comment = "\n\n����̿��� �ְ��� ����";
                break;

            case "�򸮴�":
                comment = "24.01.03\n\n" +
                          "������ٴ� ���۸��� ������";
                break;

            case "ħ��":
                comment = "\n\n������ �ʼ����";
                break;

            case "īƮ":
                comment = "24.04.07 / īƮ����\n\n" +
                          "���Ǹ� �Ǿ���մϴ�";
                break;

            case "Ŀ��":
                comment = "\n\n���ϰ� ���ô� ��";
                break;

            case "ź��":
                comment = "\n\n����";
                break;

            case "����":
                comment = "\n\n�����ߵ�";
                break;

            case "Ƽ��Ǭ":
                comment = "\n\n";
                break;

            case "���������":
                comment = "\n\n������ �Ծ���մϴ�";
                break;

            case "��":
                comment = "23.11.10 / ���ڼ���\n\n" +
                          "���� (~ 23.12.24)";
                break;

            // ����
            case "������":
                comment = "24.01.01 / ���ڼ���" +
                    "\n\n??? : ��.. �׷� ����?";
                break;

            case "���":
                comment = "���ڼ���\n\n" +
                          "������մϴ�";
                break;

            case "����":
                comment = "\n\n�÷δ� �ư���..";
                break;

            case "���˴�":
                comment = "���ڼ���\n\n" +
                          "����������";
                break;

            case "��Ÿ��":
                comment = "�ǻ�\n\n" +
                          "���� ��Ÿ��";
                break;

            case "���ϻ�":
                comment = "23.10.25 / ���ϻ�\n\n" +
                          "I believe I can fly~";
                break;

            case "����":
                comment = "��X��X��\n\n" +
                          "������";
                break;

            case "������":
                comment = "23.10.12 / ���ָ��\n\n" +
                          "�����Ƥ�������";
                break;

            case "��¤����":
                comment = "\n\n����Ͷ�";
                break;

            case "�ٵϵ�":
                comment = "23.08.15 / ����ġ\n\n" +
                          "���� 10�� ��";
                break;

            case "�Ҵ߼ҽ�":
                comment = "\n\n���ְ� �ʽ��ϴ�";
                break;

            case "���":
                comment = "23.12.04 / AGF�ı�\n\n" +
                          "�Ϳ����ϴ�";
                break;

            case "�߿��θ���":
                comment = "23.10.31 / �ҷ���\n\n" +
                          "��?�����ϴ�";
                break;

            case "��Ÿ����":
                comment = "23.12.25 / ũ��������\n\n" +
                          "��Ÿ�� �ֽ��ϴ�";
                break;

            case "���۶�":
                comment = "24.04.20 / ���ǻ�\n\n" +
                          "������������";
                break;

            case "��ĸ":
                comment = "\n\n3����";
                break;

            case "����":
                comment = "24.01.06 / ����\n\n" +
                          "�÷��� ��������";
                break;

            case "���丶��":
                comment = "23.12.22 / �Ŀ�\n\n" +
                          "������ �Ϳ����ϴ�";
                break;

            case "�͵�":
                comment = "23.12.21 / ��\n\n" +
                          "�͵��� ô!(�����̸�)";
                break;

            case "�丮�����":
                comment = "\n\n����ī�� ����";
                break;

            case "������":
                comment = "��X��X��\n\n" +
                          "��ٴ��� ����ϼ���?";
                break;

            case "���":
                comment = "23.07.02\n\n" +
                          "���� ���";
                break;

            case "�����":
                comment = "23.11.24 / ������\n\n" +
                          "�ָ� �������ϴ�";
                break;

            case "������":
                comment = "\n\n300�� ������";
                break;

            case "û���":
                comment = "\n\n�÷ΰ� �����մϴ�";
                break;

            case "�ʷϺ��":
                comment = "\n\n6����";
                break;

            case "��Ŀ":
                comment = "24.04.20 / ���ǻ�\n\n" +
                          "�����ϰ� ���� ���ϴ�";
                break;

            case "ġ��":
                comment = "\n\n�� ġ ��";
                break;

            case "�䳢����":
                comment = "24.04.20 / ���ǻ�\n\n" +
                          "������ ����";
                break;

            case "�����ҵ�":
                comment = "23.11.24 / �����ҵ�\n\n" +
                          "������ ����";
                break;

            case "�׾Ƹ�":
                comment = "24.04.23 / �׾Ƹ�\n\n" +
                          "���μ��� �ͽ��ϴ�";
                break;

            case "���ĸ�":
                comment = "\n\n�ų� �� ���Դϴ�";
                break;

            case "����":
                comment = "24.02.19 / ��Ÿ\n\n" +
                          "������? ��Ʃ��";
                break;

            case "��":
                comment = "23.11.10 / ���ڼ���\n\n" +
                          "��";
                break;

            case "�������":
                comment = "\n\n��� �����ϴ�";
                break;

            // ����
            case "�Ͱ���":
                comment = "23.05.03 / ���ǻ�\n\n" +
                          "�������̵�� �ʾ����ϴ�";
                break;

            case "��":
                comment = "��X��Ʈ\n\n" +
                          "�׷��� ���";
                break;

            case "���":
                comment = "\n\nȮ���� �÷��� ȸ��";
                break;

            case "�ϸ�":
                comment = "���ڼ���\n\n" +
                          "�ϸ��";
                break;

            case "�Ͼ��":
                comment = "23.11.16 / ���ڼ���\n\n" +
                          "���ٳ��ٴϾ��";
                break;

            case "���̾Ƹ��":
                comment = "\n\n�׳�?";
                break;

            case "����":
                comment = "���ڼ���\n\n" +
                          "����";
                break;

            case "�絹��":
                comment = "23.12.24 / ���ڼ���\n\n" +
                          "������ ����(�̰���)";
                break;

            case "��������":
                comment = "23.10.21\n\n" +
                          "�ϴ��� �Ӹ���";
                break;

            case "������":
                comment = "23.10.03 / ù�ַ�\n\n" +
                          "������ �ŷ��ؿ�~";
                break;

            case "��Ʈ����":
                comment = "\n\n�� ���ְ�� �ȸԴ�";
                break;

            case "��":
                comment = "23.11.10 / ���ڼ���\n\n" +
                          "����";
                break;

            case "�ٳĳ�":
                comment = "24.03.06\n\n" +
                          "�ٳĳ�~";
                break;

            case "����":
                comment = "24.04.20 / �ý�����\n\n" +
                          "�ڸ� ���� �ʾҴٸ�..";
                break;

            case "��簻":
                comment = "24.03.16\n\n" +
                          "���� �Դ� ���Դϴ�";
                break;

            case "���":
                comment = "��X��Ʈ" +
                          "\n\n��ǥ���� ��";
                break;

            case "�뷱����":
                comment = "\n\n��� �����ϴ�";
                break;

            case "��ư":
                comment = "\n\n�����ư(��¥����)";
                break;

            case "�غ���":
                comment = "24.04.14 / �����Ƽī\n\n" +
                          "�ø�";
                break;

            case "���":
                comment = "23.12.29 ~\n\n" +
                          "���߱� ��Ҹ�";
                break;

            case "������":
                comment = "24.03.28 ~\n\n" +
                          "�������� �游�����ϴ�";
                break;

            case "�����":
                comment = "24.04.15 ~\n\n" +
                          "��������";
                break;

            case "���":
                comment = "\n\n���Ƹ�~ û��Ƹ�~";
                break;

            case "��õ":
                comment = "23.11.07 / ���ڼ���\n\n" +
                          "�� ��������ϴ�";
                break;

            case "����":
                comment = "\n\n��X��2";
                break;

            case "��������":
                comment = "\n\n�� ����?";
                break;

            case "��":
                comment = "23.03.01 / �����\n\n" +
                          "���� ��";
                break;

            case "����ũ":
                comment = "23.12.25 / ũ��������\n\n" +
                          "�кҿ� �ҿ��� ���";
                break;

            case "��":
                comment = "";
                break;

            case "ůů��":
                comment = "23.12.19 / ���ڼ���\n\n" +
                          "ůů";
                break;

            case "�Ķ�����":
                comment = "\n\n9����";
                break;

            case "���ڲ�":
                comment = "24.02.16 / ��Ÿ\n\n" +
                          "���ŵ��� ���輺";
                break;

            case "���Ϻ�":
                comment = "24.04.21 / ���Ϻ�\n\n" +
                          "�ùظ�";
                break;

            case "ǻ��":
                comment = "\n\n���׿��� �ǳ� ����";
                break;

            case "���":
                comment = "\n\n�ѹ��߿� ī���� ���";
                break;

            // ����
            case "Maro-15":
                comment = "24.02.22 / �÷λ���\n\n" +
                          "������ ����";
                break;

            case "��Ÿ":
                comment = "\n\n�뷡�� ġƮŰ";
                break;

            case "����":
                comment = "\n\n����~";
                break;

            case "������":
                comment = "24.02.08 ~ / �ǻ�\n\n" +
                          "������ ���� ��";
                break;

            case "����":
                comment = "23.11.05\n\n" +
                          "������?";
                break;

            case "����":
                comment = "23.06.10 ~\n\n" +
                          "�÷��� ����";
                break;

            case "����������":
                comment = "23.10.02 / �Ѿ־�\n\n" +
                          "�ʹ� �����";
                break;

            case "����":
                comment = "23.06.10 / ����\n\n" +
                          "������ ����";
                break;

            case "�ξ���":
                comment = "23.10.21 / Pale\n\n" +
                          "���� �������ϴ�";
                break;

            case "��������":
                comment = "23.09.21 / �ǻ�\n\n" +
                          "������ ���� �� 2";
                break;

            case "�Ͻϱ�ġ":
                comment = "23.12.04 ~\n\n" +
                          "�Ͻϱ�ġ~";
                break;

            case "�Ȱ�":
                comment = "23.06.10 ~ / �ǻ�\n\n" +
                          "����";
                break;

            case "�հ�":
                comment = "23.09.17 / 100��\n\n" +
                          "���ο��� ������";
                break;

            case "���":
                comment = "���ϱ���\n\n" +
                          "�پ �ǿ뼺";
                break;

            case "���ֺ����":
                comment = "23.10.19 & ���ֺ����\n\n" +
                          "����� ��� ����";
                break;

            case "���ּ�":
                comment = "\n\n�÷��� �ڰ���";
                break;

            case "�ڵ���":
                comment = "23.06.08 / ������\n\n" +
                          "����! ������";
                break;

            case "��������":
                comment = "23.06.10 ~ / �ǻ�\n\n" +
                          "���� �Ϳ� ���� ���ϴ�";
                break;

            case "������Ʈ":
                comment = "23.06.10 ~ / �ǻ�\n\n" +
                          "������ ���� ��";
                break;

            case "���������Ƿ�":
                comment = "24.04.09 ~\n\n" +
                          "��������������";
                break;

            case "������":
                comment = "\n\n���� 1��";
                break;

            case "ġ�Ŀ�":
                comment = "24.02.18 / �Է���\n\n" +
                          "�԰԰Էΰ԰԰Է�";
                break;

            case "ī���":
                comment = "23.09.05 / ���̵�\n\n" +
                          "Ÿ���� ���̵�(����)";
                break;

            case "��Ż":
                comment = "24.03.24 / ��Ż2\n\n" +
                          "Ŭ���������� ��ϸ���";
                break;

            case "ȭ����":
                comment = "24.03.13 / �ֵ�\n\n" +
                          "���� �� ��ü";
                break;

            default:
                comment = "";
                break;
        }

        return comment;
    }

    string SetItemDetailText()
    {
        ItemInfo itemInfo = this.GetComponent<ItemInfo>();

        // ������ ���� ������ ��� ��´�
        string tmpText = "";
        int plusCount = 0;
        int minusCount = 0;

        // ���� ����
        if (itemInfo.DMGPercent > 0)
        {
            tmpText += "����� +" + itemInfo.DMGPercent + "%\n";
            plusCount++;
        }
        if (itemInfo.ATKSpeed > 0)
        {
            tmpText += "���ݼӵ� +" + itemInfo.ATKSpeed + "%\n";
            plusCount++;
        }
        if (itemInfo.FixedDMG > 0)
        {
            tmpText += "�߰� ����� +" + itemInfo.FixedDMG + '\n';
            plusCount++;
        }
        if (itemInfo.Critical > 0)
        {
            tmpText += "ġ��Ÿ Ȯ�� +" + itemInfo.Critical + "%\n";
            plusCount++;
        }
        if (itemInfo.Range > 0)
        {
            tmpText += "���� +" + itemInfo.Range + "%\n";
            plusCount++;
        }

        // ��� ����
        if (itemInfo.HP > 0)
        {
            tmpText += "�ִ� ü�� +" + itemInfo.HP + '\n';
            plusCount++;
        }
        if (itemInfo.Recovery > 0)
        {
            tmpText += "ȸ���� +" + itemInfo.Recovery + '\n';
            plusCount++;
        }
        if (itemInfo.HPDrain > 0)
        {
            tmpText += "����� ��� +" + itemInfo.HPDrain + "%\n";
            plusCount++;
        }
        if (itemInfo.Armor > 0)
        {
            tmpText += "���� +" + itemInfo.Armor + '\n';
            plusCount++;
        }
        if (itemInfo.Evasion > 0)
        {
            tmpText += "ȸ�� Ȯ�� +" + itemInfo.Evasion + "%\n";
            plusCount++;
        }

        // ��ƿ ����
        if (itemInfo.MovementSpeedPercent > 0)
        {
            tmpText += "�̵��ӵ� +" + itemInfo.MovementSpeedPercent + "%\n";
            plusCount++;
        }
        if (itemInfo.RootingRange > 0)
        {
            tmpText += "ȹ�� ���� +" + itemInfo.RootingRange + "%\n";
            plusCount++;
        }
        if (itemInfo.Luck > 0)
        {
            tmpText += "��� +" + itemInfo.Luck + '\n';
            plusCount++;
        }
        if (itemInfo.Harvest > 0)
        {
            tmpText += "��Ȯ +" + itemInfo.Harvest + '\n';
            plusCount++;
        }
        if (itemInfo.ExpGain > 0)
        {
            tmpText += "����ġ ȹ�� +" + itemInfo.ExpGain + "%\n";
            plusCount++;
        }

        // ������ Ư�� ȿ��
        if (itemInfo.positiveSpecial != "")
        {
            tmpText += itemInfo.positiveSpecial + "\n";
            plusCount++;
        }

        // ���� ����
        if (itemInfo.DMGPercent < 0)
        {
            tmpText += "����� " + itemInfo.DMGPercent + "%\n";
            minusCount++;
        }
        if (itemInfo.ATKSpeed < 0)
        {
            tmpText += "���ݼӵ� " + itemInfo.ATKSpeed + "%\n";
            minusCount++;
        }
        if (itemInfo.FixedDMG < 0)
        {
            tmpText += "�߰� ����� " + itemInfo.FixedDMG + '\n';
            minusCount++;
        }
        if (itemInfo.Critical < 0)
        {
            tmpText += "ġ��Ÿ Ȯ��" + itemInfo.Critical + "%\n";
            minusCount++;
        }
        if (itemInfo.Range < 0)
        {
            tmpText += "���� " + itemInfo.Range + "%\n";
            minusCount++;
        }

        // ��� ����
        if (itemInfo.HP < 0)
        {
            tmpText += "�ִ� ü�� " + itemInfo.HP + '\n';
            minusCount++;
        }
        if (itemInfo.Recovery < 0)
        {
            tmpText += "ȸ���� " + itemInfo.Recovery + '\n';
            minusCount++;
        }
        if (itemInfo.HPDrain < 0)
        {
            tmpText += "����� ��� " + itemInfo.HPDrain + "%\n";
            minusCount++;
        }
        if (itemInfo.Armor < 0)
        {
            tmpText += "���� " + itemInfo.Armor + '\n';
            minusCount++;
        }
        if (itemInfo.Evasion < 0)
        {
            tmpText += "ȸ�� Ȯ�� " + itemInfo.Evasion + "%\n";
            minusCount++;
        }

        // ��ƿ ����
        if (itemInfo.MovementSpeedPercent < 0)
        {
            tmpText += "�̵��ӵ� " + itemInfo.MovementSpeedPercent + "%\n";
            minusCount++;
        }
        if (itemInfo.RootingRange < 0)
        {
            tmpText += "ȹ�� ���� " + itemInfo.RootingRange + "%\n";
            minusCount++;
        }
        if (itemInfo.Luck < 0)
        {
            tmpText += "��� " + itemInfo.Luck + '\n';
            minusCount++;
        }
        if (itemInfo.Harvest < 0)
        {
            tmpText += "��Ȯ " + itemInfo.Harvest + '\n';
            minusCount++;
        }
        if (itemInfo.ExpGain < 0)
        {
            tmpText += "����ġ ȹ�� " + itemInfo.ExpGain + "%\n";
            minusCount++;
        }

        // ������ Ư�� ȿ��
        if (itemInfo.negativeSpecial != "")
        {
            tmpText += itemInfo.negativeSpecial + "\n";
            minusCount++;
        }

        // �ؽ�Ʈ�� �� �������� ������
        string[] lines = tmpText.Split('\n');
        string finalText = ""; // ���� �ؽ�Ʈ

        // �ɷ�ġ�� ����ϸ� �ؽ�Ʈ�� �ʷϻ����� ����
        for (int j = 0; j < plusCount; j++)
        {
            string coloredLine = "";
            // +�� ���ڸ� ���� �����Ѵ�
            for (int k = 0; k < lines[j].Length; k++)
            {
                // #1FDE38 << ���� �ʷϻ�
                if (lines[j][k] == '+' || lines[j][k] == '%' || lines[j][k] == '.')
                    coloredLine += $"<color=#1FDE38>{lines[j][k]}</color>";
                else if (lines[j][k] > 47 && lines[j][k] < 58)
                    coloredLine += $"<color=#1FDE38>{lines[j][k]}</color>";
                else
                    coloredLine += lines[j][k];
            }

            // ���� �ؽ�Ʈ�� �߰�
            finalText += coloredLine;
            finalText += "\n";
        }

        // �ɷ�ġ�� �϶��ϸ� �ؽ�Ʈ�� ���������� ����
        for (int j = plusCount; j < plusCount + minusCount; j++)
        {
            string coloredLine = "";

            // -�� ���ڸ� ���� �����Ѵ�
            for (int k = 0; k < lines[j].Length; k++)
            {
                if (lines[j][k] == '-' || lines[j][k] == '%' || lines[j][k] == '.')
                    coloredLine += $"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{lines[j][k]}</color>";
                else if (lines[j][k] > 47 && lines[j][k] < 58)
                    coloredLine += $"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{lines[j][k]}</color>";
                else
                    coloredLine += lines[j][k];
            }

            // ���� �ؽ�Ʈ�� �߰�
            finalText += coloredLine;
            finalText += "\n";
        }

        return finalText;
    }
}
