using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArenaCharacterController : NetworkBehaviour
{
    private Vector2 move;
    public float mSpeed = 5.0f;
    public float jumpForce = 7.0f;


    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigid;
    public Animator animator;

    // SyncVar 속성을 부여, 서버에서 값이 바뀌면 FlipSprite() 호출 
    [SyncVar(hook = "FlipSprite")]
    bool flip;

    // 단순 점프를 구현하기 위한 변수들
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    // 점프 키를 계속 누르고 있을 때 더 높이 점프 (ex 할로우나이트)
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    [ClientCallback]
    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isLocalPlayer)
        {

            if (Input.GetButtonDown("Jump") && isGrounded == true)
            {
                CmdJump();
                isJumping = true;
                jumpTimeCounter = jumpTime;
            }

            if (Input.GetButton("Jump") && isJumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    CmdJump();
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }

            CmdMoving(move);
        }
    }

    [Command]
    public void CmdMoving(float _move)
    {
        rigid.velocity = new Vector2(_move * mSpeed, rigid.velocity.y);

        if(_move > 0 && spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
            flip = false;
        }
        else if(_move < 0 && spriteRenderer.flipX == false)
        {
            spriteRenderer.flipX = true;
            flip = true;
        }

        animator.SetBool("grounded", true);
        animator.SetFloat("velocityX", Mathf.Abs(_move));
    }

    [Command]
    public void CmdJump()
    {
        rigid.velocity = Vector2.up * jumpForce;
    }


    [ClientCallback]
    void FlipSprite(bool newValue)
    {
        spriteRenderer.flipX = newValue;
    }
}
