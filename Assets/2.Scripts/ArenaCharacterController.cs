using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArenaCharacterController : NetworkBehaviour
{

    private Vector2 move;
    public float mSpeed = 5.0f;

    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigid;
    public Animator animator;

    // SyncVar 속성을 부여, 서버에서 값이 바뀌면 FlipSprite() 호출 
    [SyncVar(hook = "FlipSprite")]
    bool flip;

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

        // 리모트 플레이어는 Command를 이용해서 서버에서 이동, 서버의 값을 동기화 
        if (isLocalPlayer)
        {
            CmdMoving(move);
        }
    }

    [Command]
    public void CmdMoving(float _move)
    {
        rigid.velocity = new Vector2(_move * mSpeed, rigid.velocity.y);

        if (_move > 0 && spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
            flip = false;
        }
        else if (_move < 0 && spriteRenderer.flipX == false)
        {
            spriteRenderer.flipX = true;
            flip = true;
        }

        animator.SetBool("grounded", true);
        animator.SetFloat("velocityX", Mathf.Abs(_move));
    }

    [ClientCallback]
    void FlipSprite(bool newValue)
    {
        spriteRenderer.flipX = newValue;
    }
}
