using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private static PlayerControl instance;
    public static PlayerControl Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }
    private GameObject playerBox;

    private GameObject player;
    private Rigidbody2D playerRb2D;
    private Animator playerAnimator;
    private PlayerInfo playerInfo;

    public enum state
    {
        IDLE = 0,
        MOVE,
        DEAD,
    };

    public state currState = state.IDLE;

    // Field
    private float movementSpeed = 10f;

    public IEnumerator activateEpicItem20;
    public IEnumerator activateEpicItem22;
    public IEnumerator activateEpicItem33;

    private IEnumerator inActivateEpicItem20;
    private IEnumerator inActivateEpicItem22;
    private IEnumerator inActivateEpicItem33;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        playerBox = GameObject.FindGameObjectWithTag("GameController");

        player = GameObject.FindGameObjectWithTag("Player");
        playerRb2D = playerBox.GetComponent<Rigidbody2D>();
        playerAnimator = player.GetComponent<Animator>();
        playerInfo = player.GetComponent<PlayerInfo>();
    }
    void Start()
    {
        activateEpicItem20 = ActivateEpicItem20();
        activateEpicItem22 = ActivateEpicItem22();
        activateEpicItem33 = ActivateEpicItem33();
    }

    // Update is called once per frame
    void Update()
    {
        // ���尡 ������ �ʰ�, �÷��̾ ���� �ʾ��� ���
        if (!GameRoot.Instance.GetIsRoundClear() && currState != state.DEAD)
            Moving();

        // ���尡 �����ٸ� ������ ��� ���·� ��ȯ
        if (GameRoot.Instance.GetIsRoundClear())
            Stop();
    }

    // �÷��̾ �̵� ���·� ��ȯ
    private void Moving()
    {
        Vector2 movement;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        movementSpeed = RealtimeInfoManager.Instance.GetMovementSpeed() * 
                            (1 + RealtimeInfoManager.Instance.GetMovementSpeedPercent() / 100);
        playerRb2D.velocity = movement * movementSpeed;

        // �������� �ʴ� ���
        if (playerRb2D.velocity.x == 0f && playerRb2D.velocity.y == 0f)
        {
            playerAnimator.SetBool("IsMove", false);
            currState = state.IDLE;
            playerAnimator.Play("Idle");
        }

        // y�����θ� �����̴� ���
        if (playerRb2D.velocity.y != 0f)
        {
            currState = state.MOVE;
            playerAnimator.SetBool("IsMove", true);
        }
        // ���������� �̵��� ��
        if (playerRb2D.velocity.x > 0)
        {
            player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            playerAnimator.bodyRotation = Quaternion.Euler(0f, 180f, 0f);
            currState = state.MOVE;
            playerAnimator.SetBool("IsMove", true);
        }
        // �������� �̵��� ��
        else if (playerRb2D.velocity.x < 0)
        {
            player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            playerAnimator.bodyRotation = Quaternion.Euler(0f, 0f, 0f);
            currState = state.MOVE;
            playerAnimator.SetBool("IsMove", true);
        }

        // ������ ȿ�� ó��
        // �� ���� ��
        // �� +8
        if (currState == state.IDLE && activateEpicItem20 != null)
            StartCoroutine(activateEpicItem20);
        // ȸ�� +20%
        if (currState == state.IDLE && activateEpicItem22 != null)
            StartCoroutine(activateEpicItem22);
        // ���ݼӵ� +40%
        if (currState == state.IDLE && activateEpicItem33 != null)
            StartCoroutine(activateEpicItem33);
        // ������ ��
        // �� -8 (�� ���� �� ���� �ɷ�ġ��ŭ ����)
        if (currState == state.MOVE && inActivateEpicItem20 != null)
            StartCoroutine(inActivateEpicItem20);
        // ȸ�� -20% (�� ���� �� ���� �ɷ�ġ��ŭ ����)
        if (currState == state.MOVE && inActivateEpicItem22 != null)
            StartCoroutine(inActivateEpicItem22);
        // ���ݼӵ� -40% (�� ���� �� ���� �ɷ�ġ��ŭ ����)
        if (currState == state.MOVE && inActivateEpicItem33 != null)
            StartCoroutine(inActivateEpicItem33);
    }

    // �÷��̾ ��� ���·� ��ȯ
    private void Stop()
    {
        Vector2 movement = new Vector2(0f, 0f);

        this.playerRb2D.velocity = movement;

        playerAnimator.SetBool("IsMove", false);
        currState = state.IDLE;
        playerAnimator.Play("Idle");
    }

    public GameObject GetPlayer()
    {
        return this.gameObject;
    }

    // EpicItem20 ���� ��, �� ���� �� �� +8
    public IEnumerator ActivateEpicItem20()
    {
        int count = ItemManager.Instance.GetOwnEpicItemList()[20];

        if (count > 0)
        {
            int armor = RealtimeInfoManager.Instance.GetArmor() + (8 * count);
            RealtimeInfoManager.Instance.SetArmor(armor);
        }

        inActivateEpicItem20 = InActivateEpicItem20();

        yield return null;
    }

    public IEnumerator ActivateEpicItem22()
    {
        int count = ItemManager.Instance.GetOwnEpicItemList()[22];

        if (count > 0)
        {
            int evasion = RealtimeInfoManager.Instance.GetEvasion() + (20 * count);
            RealtimeInfoManager.Instance.SetEvasion(evasion);
        }

        inActivateEpicItem22 = InActivateEpicItem22();

        yield return null;
    }

    public IEnumerator ActivateEpicItem33()
    {
        int count = ItemManager.Instance.GetOwnEpicItemList()[33];

        if (count > 0)
        {
            float ATKSpeed = RealtimeInfoManager.Instance.GetATKSpeed() + (40 * count);
            RealtimeInfoManager.Instance.SetATKSpeed(ATKSpeed);
        }

        inActivateEpicItem33 = InActivateEpicItem33();

        yield return null;
    }

    IEnumerator InActivateEpicItem20()
    {
        int count = ItemManager.Instance.GetOwnEpicItemList()[20];

        if (count > 0)
        {
            int armor = RealtimeInfoManager.Instance.GetArmor() - (8 * count);
            RealtimeInfoManager.Instance.SetArmor(armor);
        }

        activateEpicItem20 = ActivateEpicItem20();

        yield return null;
    }

    IEnumerator InActivateEpicItem22()
    {
        int count = ItemManager.Instance.GetOwnEpicItemList()[22];

        if (count > 0)
        {
            int evasion = RealtimeInfoManager.Instance.GetEvasion() - (20 * count);
            RealtimeInfoManager.Instance.SetEvasion(evasion);
        }

        activateEpicItem22 = ActivateEpicItem22();

        yield return null;
    }

    IEnumerator InActivateEpicItem33()
    {
        int count = ItemManager.Instance.GetOwnEpicItemList()[33];

        if (count > 0)
        {
            float ATKSpeed = RealtimeInfoManager.Instance.GetATKSpeed() - (40 * count);
            RealtimeInfoManager.Instance.SetATKSpeed(ATKSpeed);
        }

        activateEpicItem33 = ActivateEpicItem33();

        yield return null;
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }
}