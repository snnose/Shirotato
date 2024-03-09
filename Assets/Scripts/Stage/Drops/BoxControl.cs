using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 맞닿으면 박스 획득
        if (collision.gameObject == PlayerControl.Instance.gameObject)
        {
            // 현재 라운드에 얻은 박스 개수 추가
            int boxCount = GameRoot.Instance.GetBoxCount();
            GameRoot.Instance.SetBoxCount(++boxCount);

            // 화면 우상단에 UI 플로팅

            Destroy(this.gameObject);
        }
    }
}
