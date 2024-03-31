using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FloatStatusDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private StatusDetailControl statusDetailControl;

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        statusDetailControl = StatusDetailControl.Instance;

        // StatusDetailUI Ȱ��ȭ
        StatusDetailControl.Instance.gameObject.SetActive(true);
        // ���� �̸��� �����´�
        string statName = this.gameObject.transform.parent.name;
        // StatusDetailUI�� �̹��� �� �ؽ�Ʈ ���� ����
        statusDetailControl.SetStatusDetail(statName);
        // StatusDetailUI�� ��ġ ����
        Vector2 pos = CalDetailUIPos();
        statusDetailControl.SetDetailUIPosition(pos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // StatusDetailUI�� ��ġ�� ȭ�� ������ �̵�
        statusDetailControl.SetDetailUIPosition(new Vector2(-2466f, 350f));
        // StatusDetailUI ��Ȱ��ȭ
        statusDetailControl.gameObject.SetActive(false);

        statusDetailControl = null;
    }

    private Vector2 CalDetailUIPos()
    {
        // �ؽ�Ʈ�� Size�� �����´�.
        Vector2 proSize = this.gameObject.GetComponent<RectTransform>().rect.size;

        // UI�� Size�� �����´�.
        RectTransform UIRectTransform = statusDetailControl.transform.GetChild(0).GetComponent<RectTransform>();
        Vector2 UISize = UIRectTransform.rect.size;

        // �̵��� x ��ǥ���� UI ũ�� + �ؽ�Ʈ ũ��
        float x = (UISize.x + 350f) / 2;
        // �̵��� y ��ǥ���� UI ũ�� + �ؽ�Ʈ ũ�⸦ 2�� ���� ��
        float y = (UISize.y + proSize.y) / 2;

        // �ؽ�Ʈ ��ġ�� �̵��� ��ǥ�� ��ŭ ���� �� ��ȯ
        Vector2 tmp = new Vector2(this.gameObject.transform.position.x - x, this.gameObject.transform.position.y);
        return tmp;
    }
}
