using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);


    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D> ();
    }

    void Start () 
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
        contactFilter.useLayerMask = true;
    }
    
    void Update () 
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity (); 
    }

    // 가상 함수, 자식에서 정의
    protected virtual void ComputeVelocity()
    {
    
    }
    
    void FixedUpdate()
    {
        // physics2D.gravity를 이용한 중력 적용  
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;

        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2 (groundNormal.y, -groundNormal.x);


        // 왜 두개로 나눠서 하지??  
        // x축 이동 
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement (move, false);

        // x축과 y축 이동 
        move = Vector2.up * deltaPosition.y;

        Movement (move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance) 
        {
            // 움직이는 방향(move)으로 distacne + shellRedius 거리 내 충돌한 것들 중 contractFilter로 한번 Filtering 후 결과를 모두 hitBuffer에 저장 
            int count = rb2d.Cast (move, contactFilter, hitBuffer, distance + shellRadius);

            // hitBufferList 에  hitBuffer내용을 대입, 결과를 유지시켜 다른곳에서도 쓸수 있게함 
            hitBufferList.Clear ();
            for (int i = 0; i < count; i++) {
                hitBufferList.Add (hitBuffer [i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++) 
            {
                // 충돌한 것의 y값이 0.65보다 크다면, 플레이어가 땅에있음
                Vector2 currentNormal = hitBufferList [i].normal;
                if (currentNormal.y > minGroundNormalY) 
                {
                    grounded = true;
                    if (yMovement) 
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                // Dot 함수는 두 인자가 같은 방향이라면 1을반환, 반대방향이라면 -1을, 둘 다 아닐경우(수직인경우 등) 0을 반환    
                float projection = Vector2.Dot (velocity, currentNormal);
                if (projection < 0) 
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList [i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        // GetAxis("Horizontal") 의 값을 특정 처리를 한후 rigidbody2D.position에 대입
        rb2d.position = rb2d.position + move.normalized * distance;
    }

}
