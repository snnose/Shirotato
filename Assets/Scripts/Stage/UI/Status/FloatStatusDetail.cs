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

        // StatusDetailUI 활성화
        StatusDetailControl.Instance.gameObject.SetActive(true);
        // 스탯 이름을 가져온다
        string statName = this.gameObject.transform.parent.name;
        // StatusDetailUI의 이미지 및 텍스트 정보 갱신
        statusDetailControl.SetStatusDetail(statName);
        // StatusDetailUI의 위치 설정
        Vector2 pos = CalDetailUIPos();
        statusDetailControl.SetDetailUIPosition(pos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // StatusDetailUI의 위치를 화면 밖으로 이동
        statusDetailControl.SetDetailUIPosition(new Vector2(-2466f, 350f));
        // StatusDetailUI 비활성화
        statusDetailControl.gameObject.SetActive(false);

        statusDetailControl = null;
    }

    private Vector2 CalDetailUIPos()
    {
        // 텍스트의 Size를 가져온다.
        Vector2 proSize = this.gameObject.GetComponent<RectTransform>().rect.size;

        // UI의 Size를 가져온다.
        RectTransform UIRectTransform = statusDetailControl.transform.GetChild(0).GetComponent<RectTransform>();
        Vector2 UISize = UIRectTransform.rect.size;

        // 이동할 x 좌표값은 UI 크기 + 텍스트 크기
        float x = (UISize.x + 350f) / 2;
        // 이동할 y 좌표값은 UI 크기 + 텍스트 크기를 2로 나눈 값
        float y = (UISize.y + proSize.y) / 2;

        // 텍스트 위치에 이동할 좌표값 만큼 더한 후 반환
        Vector2 tmp = new Vector2(this.gameObject.transform.position.x - x, this.gameObject.transform.position.y);
        return tmp;
    }
}
