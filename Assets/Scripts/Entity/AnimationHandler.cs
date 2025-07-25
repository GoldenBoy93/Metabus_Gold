using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // Animator �Ķ���� �̸��� �̸� �ؽ÷� ��ȯ�� ĳ�� (���� ����ȭ)
    // �ؽö� ���ڿ� �Ź� �ݺ� ������� �ʰ� int ���������� ��ȯ�Ͽ� ���ϴ� ������ ����ȭ
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

        // �̵� ���� ������ ũ�⸦ �̿��� �����̴� ������ �Ǵ�
        //animator.SetBool(IsMoving, obj.magnitude > .5f);
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
