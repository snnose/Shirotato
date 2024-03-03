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
    public float movementSpeed = 10f;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        // 라운드가 끝나지 않고, 플레이어가 죽지 않았을 경우
        if (!GameRoot.Instance.GetIsRoundClear() && currState != state.DEAD)
            Moving();

        // 라운드가 끝났다면 강제로 대기 상태로 전환
        if (GameRoot.Instance.GetIsRoundClear())
            Stop();
    }

    private void Moving()
    {
        Vector2 movement;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        playerRb2D.velocity = movement * movementSpeed;

        // 움직이지 않는 경우
        if (playerRb2D.velocity.x == 0f && playerRb2D.velocity.y == 0f)
        {
            playerAnimator.SetBool("IsMove", false);
            currState = state.IDLE;
            playerAnimator.Play("Idle");
        }

        // y축으로만 움직이는 경우
        if (playerRb2D.velocity.y != 0f)
        {
            currState = state.MOVE;
            playerAnimator.SetBool("IsMove", true);
        }
        // 오른쪽으로 이동할 때
        if (playerRb2D.velocity.x > 0)
        {
            player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            playerAnimator.bodyRotation = Quaternion.Euler(0f, 180f, 0f);
            currState = state.MOVE;
            playerAnimator.SetBool("IsMove", true);
        }
        // 왼쪽으로 이동할 때
        else if (playerRb2D.velocity.x < 0)
        {
            player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            playerAnimator.bodyRotation = Quaternion.Euler(0f, 0f, 0f);
            currState = state.MOVE;
            playerAnimator.SetBool("IsMove", true);
        }
    }

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
}