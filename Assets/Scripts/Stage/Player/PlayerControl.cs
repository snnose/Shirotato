using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
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
        if (!GameRoot.Instance.GetIsRoundClear() && currState != state.DEAD)
            Moving();
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
}