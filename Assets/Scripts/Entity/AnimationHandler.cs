using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // Animator 파라미터 이름을 미리 해시로 변환해 캐싱 (성능 최적화)
    // 해시란 문자열 매번 반복 계산하지 않고 int 정수값으로 변환하여 비교하는 과정을 간단화
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    private static readonly int IsDeath = Animator.StringToHash("IsDeath");

    protected Animator animator;

    protected virtual void Awake()
    {
        // 애니메이터 컴포넌트를 자식에서 가져옴
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, true);

        if (obj.x != 0)
        {
            //animator.SetBool(IsMoving, true);
            animator.SetInteger("vAxisRaw", 0);
            if (animator.GetInteger("hAxisRaw") != obj.x)
            {
                Debug.Log("dd");
                animator.SetInteger("hAxisRaw", (int)obj.x);
            }
        }

        else if (obj.y != 0)
        {
            //animator.SetBool(IsMoving, true);
            animator.SetInteger("hAxisRaw", 0);
            if (animator.GetInteger("vAxisRaw") != obj.y)
            {
                animator.SetInteger("vAxisRaw", (int)obj.y);
            }
        }

        else
        {
            animator.SetInteger("hAxisRaw", 0);
            animator.SetInteger("vAxisRaw", 0);
            animator.SetBool (IsMoving, false);
        }

        // 이동 방향 벡터의 크기를 이용해 움직이는 중인지 판단
        //animator.SetBool(IsMoving, obj.magnitude > .5f);
    }
    
    public void Damage()
    {
        // 피격 애니메이션 상태 진입
        animator.SetBool(IsDamage, true);
    }

    public void Death()
    {
        // 사망 애니메이션 상태 진입
        animator.SetBool(IsDeath, true);
    }

    public void InvincibilityEnd()
    {
        // 무적 시간 종료 시 호출
        // 필요없으면 삭제 예정
        animator.SetBool(IsDamage, false);
    }
}
