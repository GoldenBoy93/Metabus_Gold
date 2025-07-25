using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // Animator �Ķ���� �̸��� �̸� �ؽ÷� ��ȯ�� ĳ�� (���� ����ȭ)
    // �ؽö� ���ڿ� �Ź� �ݺ� ������� �ʰ� int ���������� ��ȯ�Ͽ� ���ϴ� ������ ����ȭ
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    private static readonly int IsDeath = Animator.StringToHash("IsDeath");

    protected Animator animator;

    protected virtual void Awake()
    {
        // �ִϸ����� ������Ʈ�� �ڽĿ��� ������
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
        // ���� �ִϸ��̼�
        animator.SetTrigger(IsAttack);
    }

    public void Damage()
    {
        // �ǰ� �ִϸ��̼� ���� ����
        animator.SetBool(IsDamage, true);
    }

    public void Death()
    {
        // ��� �ִϸ��̼� ���� ����
        animator.SetBool(IsDeath, true);
    }

    public void InvincibilityEnd()
    {
        // ���� �ð� ���� �� ȣ��
        // �ʿ������ ���� ����
        animator.SetBool(IsDamage, false);
    }
}
