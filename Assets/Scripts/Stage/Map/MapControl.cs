using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    GameObject mapSprite;

    private void Awake()
    {
        mapSprite = this.transform.GetChild(0).gameObject;
        //SetMapColor();
    }

    void Start()
    {
        // 설정된 맵 환경에 따라 낮, 밤으로 변경
        SetMapColor();
    }

    void SetMapColor()
    {
        int mapMode = RoundSetting.Instance.GetMapMode();

        Color color = Color.white;

        // 값이 1일 때 밤
        if (mapMode == 1)
        {
            ColorUtility.TryParseHtmlString("#9096D9", out color);
        }

        mapSprite.GetComponent<SpriteRenderer>().color = color;
    }
}
