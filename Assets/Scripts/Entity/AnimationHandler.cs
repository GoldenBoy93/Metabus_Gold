using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // Animator 파라미터 이름을 미리 해시로 변환해 캐싱 (성능 최적화)
    // 해시란 문자열 매번 반복 계산하지 않고 int 정수값으로 변환하여 비교하는 과정을 간단화
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
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
        if (obj.x != 0)
        {
            animator.SetBool(IsMoving, true);
            animator.SetFloat("hAxisRaw", 0);
            animator.SetFloat("vAxisRaw", 0);
            animator.SetFloat("hAxisRaw", obj.x);
            animator.SetFloat("vAxisRaw_A", 0);
            animator.SetFloat("hAxisRaw_A", obj.x);
        }
        else if (obj.y != 0)
        {
            animator.SetBool(IsMoving, true);
            animator.SetFloat("vAxisRaw", 0);
            animator.SetFloat("hAxisRaw", 0);
            animator.SetFloat("vAxisRaw", obj.y);
            animator.SetFloat("hAxisRaw_A", 0);
            animator.SetFloat("vAxisRaw_A", obj.y);
        }
        else
        {
            animator.SetBool (IsMoving, false);
        }
    }

    public void Attack()
    {
        // 공격 애니메이션
        animator.SetTrigger(IsAttack);
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
