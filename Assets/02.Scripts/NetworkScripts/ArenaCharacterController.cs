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

    [SyncVar(hook = "FlipSprite")]
    bool flip;

    // 단순 점프를 구현하기 위한 변수들
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    // 점프 키를 계속 누르고 있을 때 더 높이 점프를 위한 변수들 (ex 할로우나이트)
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Invoke("StartSet", 2.0f);
    }

    private void StartSet()
    {
        rigid.gravityScale = 5;
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

            // 점프 중일 때 Jump 키를 계속 누르고 있을 경우 jumpTime만큼 더 높이 올라 간다.
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


    // CmdMoving() 함수에서 flip값이 변경되면 Syncvar Hook으로 인해 호출되어 클라이언트에도 적용
    [ClientCallback]
    void FlipSprite(bool newValue)
    {
        spriteRenderer.flipX = newValue;
    }
}
