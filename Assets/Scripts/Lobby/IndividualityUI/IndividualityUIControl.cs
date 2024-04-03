using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualityUIControl : MonoBehaviour
{
    // singleton
    private static IndividualityUIControl instance;
    public static IndividualityUIControl Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        Cancel();
    }

    // ȭ�� �÷��� �� Esc �Է� �� UI ��Ȱ��ȭ
    private void Cancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetActive(false);
        }
    }

    public void SetActive(bool ret)
    {
        // true�� �� UI Ȱ��ȭ �� ȭ�� �߾����� �̵�
        if (ret)
        {
            this.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        }
        // false�� �� UI ��Ȱ��ȭ �� ȭ�� �ٱ����� �̵�
        else
        {
            this.transform.position = new Vector2(-Screen.width, Screen.height);
        }

        this.gameObject.SetActive(ret);
    }
}