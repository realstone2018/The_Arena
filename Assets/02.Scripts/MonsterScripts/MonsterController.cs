using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    public float moveSpeed = 1.0f;
    
    // 몬스터마다 다른 패턴을 어떻게 구현 할까? 
    // 상속? 인터페이스?     싹다 넣어두고 ChangeMovement()함수에서 range 변위를 매개변수로 조정?
    public enum MovementFlag {Idle, Left, Right, Trace};
    public MovementFlag movementFlag = MovementFlag.Idle;

    public Animator animator;
    public bool isTracing = false;
    public GameObject traceTarget;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(ChangeMoveMent());
    }

    private void FixedUpdate()
    {
        Move();
    }

    IEnumerator ChangeMoveMent()
    {
        // 몬스터마다 다른 패턴을 가지므로 enum()이 몬스터마다 다를 때를 대비해서 
        int num = System.Enum.GetValues(typeof(MovementFlag)).Length;
        Debug.Log("MovementFlag Length : " + num);

        while (true)
        {
            if (isTracing)
                movementFlag = MovementFlag.Trace;
            else
                movementFlag = (MovementFlag)Random.Range(0, num);

            yield return new WaitForSeconds(3.0f);
        }
    }

    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        string dist = "";
        //bool jumping = false;

        if (movementFlag == MovementFlag.Trace && isTracing)
        {
            Vector3 playerPos = traceTarget.transform.position;

            if (playerPos.x < transform.position.x)
                dist = "Left";
            else if (playerPos.x >= transform.position.x)
                dist = "Right";

            // 몬스터가 땅에 있고 플레이어가 몬스터보다 위에 있는 경우 랜덤 시간마다 점프 
            //if(!jumping && playerPos.y > transform.position.y)
            

        }
        else if (movementFlag == MovementFlag.Left)
            dist = "Left";
        else if (movementFlag == MovementFlag.Right)
            dist = "Right";


        if (dist == "Left")
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Vector3 rayPos = transform.position + moveVelocity * 1.5f;
        Ray2D ray = new Ray2D(rayPos, Vector2.down);

        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 3.0f, layerMask);

        if (hit.collider == null)
        {
            Debug.Log("Nothing");
            moveVelocity *= -1;
        }
        else
            Debug.Log("start : " + rayPos + "  hit : " + hit.collider.name);

        transform.position += moveVelocity * moveSpeed * Time.deltaTime;
        SetAnimator();
    }

    private void SetAnimator()
    {
        switch(movementFlag)
        {
            case MovementFlag.Idle:
                animator.SetBool("isMoving", false);
                break;
            case MovementFlag.Left:
            case MovementFlag.Right:
            case MovementFlag.Trace:
                animator.SetBool("isMoving", true);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Collision With Player");
            isTracing = true;
            traceTarget = collision.gameObject;

            StopCoroutine(ChangeMoveMent());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTracing = false;

            StartCoroutine(ChangeMoveMent());
        }
    }
}
